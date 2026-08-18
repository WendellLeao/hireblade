using System.Collections.Generic;
using UnityEngine;
using WendellLeao.Pooling;
using Hireblade.Gameplay.Particles;

namespace Hireblade.Gameplay.Spells.Manager
{
    internal sealed class SpellManager : MonoBehaviour, ISpellFactory
    {
        private readonly List<ISpell> _spells = new();

        private IPoolingService _poolingService;
        private IParticleFactory _particleFactory;

        public void SetUp(IPoolingService poolingService, IParticleFactory particleFactory)
        {
            _poolingService = poolingService;
            _particleFactory = particleFactory;
        }

        public ISpell CastSpell(SpellData data, Vector3 position, Vector3 direction)
        {
            if (!_poolingService.TryGetObjectFromPool(data.PoolData.Id, parent: null, out ISpell spell))
            {
                return null;
            }

            _spells.Add(spell);

            spell.SetUp();

            SetSpellPositionAndRotation(position, direction, spell);

            spell.OnHit += HandleSpellHit;

            if (spell is IParticleEmitter particleEmitter)
            {
                particleEmitter.SetParticleFactory(_particleFactory);
            }

            return spell;
        }

        private void OnDestroy()
        {
            for (int i = _spells.Count - 1; i >= 0; i--)
            {
                DisposeSpell(_spells[i]);
            }
        }

        private void HandleSpellHit(ISpell spell)
        {
            DisposeSpell(spell);
        }

        private void DisposeSpell(ISpell spell)
        {
            spell.OnHit -= HandleSpellHit;

            _spells.Remove(spell);

            _poolingService.ReleaseObjectToPool(spell);
        }

        private void SetSpellPositionAndRotation(Vector3 position, Vector3 direction, ISpell spell)
        {
            spell.transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction));
        }
    }
}
