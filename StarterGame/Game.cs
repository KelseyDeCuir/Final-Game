using System.Collections;
using System.Collections.Generic;
using System;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class Game
    {
        private Character _player;
        private Parser _parser;
        private bool _playing;

        public Game()
        {
            //TODO: EDIT GAME() TO IMPLEMENT MENUS
            _playing = false;
            _parser = new Parser(new CommandWords());
            //_player creates a new game however, if we want to have a save and load system 
            // the player should only be able to create a new game if they type create new game
            //therefore Game() will likely have to change
            _player = new Player(GameWorld.Instance);
        }

        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.

            bool finished = false;
            while (!finished)
            {
                Console.Write("\n=> ");
                Command command = _parser.ParseCommand(_player.State, Console.ReadLine().ToLower());
                if (command == null)
                {
                    _player.ErrorMessage("I don't understand...");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }


        public void Start()
        {
            _playing = true;
            _player.InfoMessage(Welcome());
        }

        public void End()
        {
            _playing = false;
            _player.InfoMessage(Goodbye());
        }

        public string Welcome()
        {
            return "You wake up alone. You seem to be in an elevator that you do not recognize.\n\nYou have no recollection of how you ended up there.\n\nType 'help' if you need help. " + _player.CurrentRoom.Description() + "\n(You must enter a name or load a save to continue)";
        }

        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }
        public string Save()
        {
            return "\nGame saved.";
        }
        public string Win()
        {
            return "\n CONGRATULATIONS! You have won!";
        }
        public string GameOver()
        {
            return "\nYou have died. GAME OVER. \n";
        }

    }
}
