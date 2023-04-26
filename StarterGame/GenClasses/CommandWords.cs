using System.Collections;
using System.Collections.Generic;
using System;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class CommandWords
    {
        private static Command[] _commandArrayInCreation = { new CharNameCommand()};
        private static Command[] _commandArrayInMenu = { new ReflectCommand(), new PlayCommand()};
        private static Command[] _commandArrayInGame = { new GoCommand(), new LookCommand(), new ReflectCommand(), new EquipCommand(), new UnequipCommand(), new LevelCommand(), new EnchantCommand(), new HitCommand(), new BackCommand(), new TakeCommand(), new MenuCommand()};
        private static Command[] _commandArrayInCombat = {};
        private static Command[] _commandArrayInDialogue = {};
        private static Command[] _commandArrayInShop = { new ShopListCommand(), new SellCommand(), new BuyCommand(),  new PlayCommand()};
        private static Command[] _commandArrayInElevator = { new GoCommand(), new AscendCommand(), new DescendCommand(), new LookCommand(), new ReflectCommand(), new EquipCommand(), new UnequipCommand(), new LevelCommand(), new EnchantCommand(), new BackCommand(), new ShopCommand(), new MenuCommand(), new SaveCommand(), new LoadCommand()};
        private static Dictionary<States, Command[]> _commandArrays = new Dictionary<States, Command[]>() { {States.CHARCREATION, _commandArrayInCreation }, {States.ELEVATOR, _commandArrayInElevator },{ States.MENU, _commandArrayInMenu }, { States.GAME, _commandArrayInGame }, { States.COMBAT, _commandArrayInCombat }, { States.DIALOGUE, _commandArrayInDialogue } , { States.SHOP, _commandArrayInShop } };
        private Dictionary<States, Dictionary<string, Command>> _commands;

        public CommandWords() : this(_commandArrays) {}

        // State machine based Designated Constructor
        public CommandWords(Dictionary<States, Command[]> commandlist)
        {
            _commands = new Dictionary<States, Dictionary<string, Command>>();
            _commands[States.MENU] = new Dictionary<string, Command>();
            _commands[States.GAME] = new Dictionary<string, Command>();
            _commands[States.COMBAT] = new Dictionary<string, Command>();
            _commands[States.DIALOGUE] = new Dictionary<string, Command>();
            _commands[States.ELEVATOR] = new Dictionary<string, Command>();
            _commands[States.CHARCREATION] = new Dictionary<string, Command>();
            _commands[States.SHOP] = new Dictionary<string, Command>();
            foreach (States state in Enum.GetValues(typeof(States))) {
                foreach (Command command in commandlist[state])
                {
                    _commands[state][command.Name] = command;
                }
                Command help = new HelpCommand(this);
                Command quit = new QuitCommand();
                _commands[state][help.Name] = help;
                _commands[state][quit.Name] = quit;
        }
        }

        public Command Get(States state, string word)
        {
            Command command = null;
            _commands[state].TryGetValue(word, out command);
            return command;
        }

        public string Description(States states)
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = _commands[states].Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
