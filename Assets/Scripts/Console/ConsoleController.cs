using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole()
        {
            var addMessage = " command has been added to the console";

            ConsoleController.AddCommandsToConsole(Command, this);
            ConsoleController.AddStaticMessageToConsole(Name + addMessage);
        }

        public abstract void RunCommand();
    }

    public class ConsoleController : MonoBehaviour
    {
        public static ConsoleController Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands { get; private set; }

        [Header("UI Components")] 
        public Canvas consoleCanvas;
        public ScrollRect scrollRect;
        public Text consoleText;
        public Text inputText;
        public InputField consoleInput;

        private void Awake()
        {
            if (Instance != null) return;

            Instance = this;

            Commands = new Dictionary<string, ConsoleCommand>();
        }

        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
            GameManager.Instance.consoleOpen = false;
            CreateCommands();
        }

        private void CreateCommands()
        {
            var commandQuit = CommandQuit.CreateCommand();
        }

        public static void AddCommandsToConsole(string name, ConsoleCommand command)
        {
            if (!Commands.ContainsKey(name))
            {
                Commands.Add(name, command);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                TriggerConsole();
            }

            if (!consoleCanvas.gameObject.activeInHierarchy) return;
            if (!Input.GetKeyDown(KeyCode.Return)) return;
            if (string.IsNullOrWhiteSpace(inputText.text)) return;
            AddMessageToConsole(inputText.text);
            ParseInput(inputText.text);
        }

        private void TriggerConsole()
        {
            consoleCanvas.gameObject.SetActive(!consoleCanvas.gameObject.activeInHierarchy);
            GameManager.Instance.consoleOpen = !GameManager.Instance.consoleOpen;
        }

        private void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
            scrollRect.verticalNormalizedPosition = 0f;
        }

        public static void AddStaticMessageToConsole(string msg)
        {
            ConsoleController.Instance.consoleText.text += msg + "\n";
            ConsoleController.Instance.scrollRect.verticalNormalizedPosition = 0f;
        }

        private void ParseInput(string input)
        {
            var splitInput = input.Split(null);

            if (splitInput.Length == 0)
            {
                AddMessageToConsole("Command not recognized");
                return;
            }

            if (!Commands.ContainsKey(splitInput[0]))
            {
                AddMessageToConsole("Command not recognized");
            }
            else
            {
                Commands[splitInput[0]].RunCommand();
            }
        }
    }
}