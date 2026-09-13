using Hireblade.Core.Health;
using WendellLeao.Events;

namespace Hireblade.Gameplay.Events.Health
{
    public sealed class HealthSpawnedEvent : GameEvent
    {
        public HealthSpawnedEvent(IHealth health)
        {
            Health = health;
        }

        public IHealth Health { get; private set; }
    }
}
