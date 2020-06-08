using UnityEngine;

namespace Terminal.Commands
{
    public class CommandQuit : TerminalCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandQuit()
        {
            Name = "Quit";
            Command = "quit";
            Description = "Quits the application";
            Help = "Use this command with no arguments to force Unity to quit!";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }

        // this must be implemented manually in all Command type scripts
        public static CommandQuit CreateCommand()
        {
            return new CommandQuit();
        }
    }
}
