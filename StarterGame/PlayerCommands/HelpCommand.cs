﻿using System.Collections;
using System.Collections.Generic;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class HelpCommand : Command
    {
        private CommandWords _words;

        public HelpCommand() : this(new CommandWords()){}

        // Designated Constructor
        public HelpCommand(CommandWords commands) : base()
        {
            _words = commands;
            this.Name = "help";
        }

        override
        public bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("\nI cannot help you with " + this.SecondWord);
            }
            else
            {
                player.InfoMessage("\nYou are lost. You are alone. You wander around, \n\nYour available commands are " + _words.Description(player.State));
            }
            return false;
        }
    }
}
