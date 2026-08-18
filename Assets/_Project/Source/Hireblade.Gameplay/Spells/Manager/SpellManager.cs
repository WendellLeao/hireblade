using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Spells.Manager
{
    internal sealed class SpellManager : MonoBehaviour
    {
        private SpellFactory _spellFactory;

        public ISpellFactory Factory => _spellFactory;

        public void Initialize(IPoolingService poolingService, IParticleFactory particleFactory)
        {
            _spellFactory = new SpellFactory(poolingService, particleFactory);
        }

        private void OnDestroy()
        {
            _spellFactory?.Shutdown();
        }
    }
}
