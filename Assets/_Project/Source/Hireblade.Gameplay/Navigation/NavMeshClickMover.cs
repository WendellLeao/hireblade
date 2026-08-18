using UnityEngine;
using UnityEngine.AI;
using WendellLeao.Pooling;

namespace Hireblade.Gameplay.Navigation
{
    internal sealed class NavMeshClickMover : MonoBehaviour, IMoveableAgent
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        [SerializeField]
        private LayerMask walkableLayerMask;

        [Header("Particle")]
        [SerializeField]
        private PoolData clickSurfaceParticlePoolData;

        private ICameraProvider _cameraProvider;
        private IParticleFactory _particleFactory;
        private RaycastHit _cachedHitInfo;

        public Vector3 Velocity => navMeshAgent.velocity;

        public void SetUp(ICameraProvider cameraProvider, IParticleFactory particleFactory)
        {
            _cameraProvider = cameraProvider;
            _particleFactory = particleFactory;
        }

        public void ResetPath()
        {
            navMeshAgent.ResetPath();
            navMeshAgent.velocity = Vector3.zero;
        }

        public void Tick(float deltaTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                HandleNavMeshAgentDestination();
            }
        }

        private void HandleNavMeshAgentDestination()
        {
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out _cachedHitInfo, maxDistance: Mathf.Infinity, walkableLayerMask))
            {
                Vector3 hitInfoPoint = _cachedHitInfo.point;

                SetDestination(hitInfoPoint);

                EmitClickOnSurfaceParticle(hitInfoPoint);
            }
        }

        private void EmitClickOnSurfaceParticle(Vector3 hitInfoPoint)
        {
            Vector3 particlePosition = hitInfoPoint + new Vector3(0f, 0.1f, 0f);

            GameObject clickSurfaceParticlePrefab = clickSurfaceParticlePoolData.Prefab;

            _particleFactory.EmitParticle(clickSurfaceParticlePoolData, particlePosition, clickSurfaceParticlePrefab.transform.rotation);
        }

        private void SetDestination(Vector3 position)
        {
            navMeshAgent.destination = position;
        }
    }
}
