using System;
using Cysharp.Threading.Tasks;
using Hireblade.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using WendellLeao.ServiceLocator;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace Hireblade.Application
{
    internal sealed class GameFlowManager : MonoBehaviour, IGameFlowService
    {
        private const string MainMenuScenePath = "Assets/_Project/Scenes/LandingPage.unity";
        private const string GameplayScenePath = "Assets/_Project/Scenes/SampleScene.unity";

        private readonly MainMenuFlowManager _mainMenuFlowManager = new();
        private readonly GameplayFlowManager _gameplayFlowManager = new();

        private GameFlowState _currentState;
        private string _currentScenePath;

        public void Initialize()
        {
            EnterMainMenu();
        }

        public void EnterMainMenu()
        {
            TransitionToAsync(GameFlowState.MainMenu, MainMenuScenePath).Forget();
        }

        public void EnterGameplay()
        {
            TransitionToAsync(GameFlowState.Gameplay, GameplayScenePath).Forget();
        }

        private void Awake()
        {
            Locator.Register<IGameFlowService>(this);
        }

        private void OnDestroy()
        {
            Locator.Unregister<IGameFlowService>();
        }

        private async UniTask TransitionToAsync(GameFlowState state, string scenePath)
        {
            Debug.Log($"[GameFlowManager] {_currentState} -> {state}");

            string previousScenePath = _currentScenePath;

            _currentState = state;
            _currentScenePath = scenePath;

            Scene loadedScene = await LoadSceneAsync(scenePath);

            await GetStateManager(state).EnterAsync(loadedScene);

            if (!string.IsNullOrEmpty(previousScenePath))
            {
                _ = SceneManager.UnloadSceneAsync(previousScenePath);
            }
        }

        private IGameFlowStateManager GetStateManager(GameFlowState state)
        {
            return state switch
            {
                GameFlowState.MainMenu => _mainMenuFlowManager,
                GameFlowState.Gameplay => _gameplayFlowManager,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, message: null)
            };
        }

        private async UniTask<Scene> LoadSceneAsync(string scenePath)
        {
#if UNITY_EDITOR
            AsyncOperation asyncOperation = EditorSceneManager.LoadSceneAsyncInPlayMode(scenePath, new LoadSceneParameters(LoadSceneMode.Additive));
#else
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
#endif
            while (!asyncOperation.isDone)
            {
                await UniTask.Yield();
            }

            return SceneManager.GetSceneByPath(scenePath);
        }
    }
}
