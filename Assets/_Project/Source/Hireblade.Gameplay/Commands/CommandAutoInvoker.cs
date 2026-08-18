using System.Collections;
using UnityEngine;

namespace Hireblade.Gameplay.Commands
{
    internal sealed class CommandAutoInvoker : MonoBehaviour, ICommandInvoker
    {
        private AttackCommand _attackCommand;
        private Coroutine _executeAttackCommandRoutine;

        public void SetUp(IWeaponHolder weaponHolder)
        {
            _attackCommand = new AttackCommand(weaponHolder);

            _executeAttackCommandRoutine = StartCoroutine(ExecuteAttackCommandRoutine());
        }

        public void Dispose()
        {
            if (_executeAttackCommandRoutine != null)
            {
                StopCoroutine(_executeAttackCommandRoutine);
            }
        }

        public void Tick(float deltaTime)
        { }

        private IEnumerator ExecuteAttackCommandRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(3f, 5f));

                _attackCommand.Execute();
            }
        }
    }
}
