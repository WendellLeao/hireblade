using System.Collections.Generic;
using Hireblade.Commands;
using UnityEngine;
using Hireblade.Gameplay.Weapons;

namespace Hireblade.Gameplay.Commands
{
    internal sealed class CommandInputReader : MonoBehaviour, ICommandInvoker
    {
        [SerializeField]
        private CommandCollectionData commandCollectionData;

        private readonly Dictionary<CommandType, ICommand> _commands = new();

        public void Initialize(IWeaponHolder weaponHolder)
        {
            _commands.TryAdd(CommandType.Attack, new AttackCommand(weaponHolder));
        }

        public void Shutdown()
        { }

        public void Tick(float deltaTime)
        {
            ReadCommands();
        }

        private void ReadCommands()
        {
            foreach (KeyValuePair<CommandType, ICommand> keyValuePair in _commands)
            {
                if (HasPressedCommandKey(keyValuePair.Key))
                {
                    ICommand command = keyValuePair.Value;

                    command.Execute();
                }
            }
        }

        private bool HasPressedCommandKey(CommandType commandType)
        {
            if (!commandCollectionData.TryGetCommandDataByType(commandType, out CommandData data))
            {
                return false;
            }

            return Input.GetKeyDown(data.KeyCode);
        }
    }
}
