using Hireblade.Core;
using WendellLeao.Events;

namespace Hireblade.Events.Health
{
    public sealed class HealthSpawnedEvent : GameEvent
    {
        public IHealth Health { get; private set; }

        public HealthSpawnedEvent(IHealth health)
        {
            Health = health;
        }
    }
}
