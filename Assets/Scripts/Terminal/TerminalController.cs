using System.Collections.Generic;
using Core;
using Terminal.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace Terminal
{
    public abstract class TerminalCommand
    {
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole()
        {
            var addMessage = " command has been added to the terminal";

            TerminalController.AddCommandsToTerminal(Command, this);
            TerminalController.AddStaticMessageToTerminal(Name + addMessage);
        }

        public abstract void RunCommand();
    }

    public class TerminalController : MonoBehaviour
    {
        public static TerminalController Instance { get; private set; }
        public static Dictionary<string, TerminalCommand> Commands { get; private set; }

        [Header("UI Components")] 
        public Canvas terminalCanvas;
        public ScrollRect scrollRect;
        public Text terminalText;
        public Text inputText;
        public InputField terminalInput;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != null)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Commands = new Dictionary<string, TerminalCommand>();

            terminalCanvas.gameObject.SetActive(false);
            GameManager.Instance.consoleOpen = false;
            CreateCommands();
        }

        private void CreateCommands()
        {
            var commandQuit = CommandQuit.CreateCommand();
        }

        public static void AddCommandsToTerminal(string name, TerminalCommand command)
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
                TriggerTerminal();
            }

            if (!terminalCanvas.gameObject.activeInHierarchy) return;
            if (!Input.GetKeyDown(KeyCode.Return)) return;
            if (string.IsNullOrWhiteSpace(inputText.text)) return;
            AddMessageToTerminal(inputText.text);
            ParseInput(inputText.text);
        }

        private void TriggerTerminal()
        {
            terminalCanvas.gameObject.SetActive(!terminalCanvas.gameObject.activeInHierarchy);
            GameManager.Instance.consoleOpen = !GameManager.Instance.consoleOpen;
        }

        private void AddMessageToTerminal(string msg)
        {
            terminalText.text += msg + "\n";
            scrollRect.verticalNormalizedPosition = 0f;
        }

        public static void AddStaticMessageToTerminal(string msg)
        {
            TerminalController.Instance.terminalText.text += msg + "\n";
            TerminalController.Instance.scrollRect.verticalNormalizedPosition = 0f;
        }

        private void ParseInput(string input)
        {
            var splitInput = input.Split(' ');

            if (splitInput.Length == 0)
            {
                AddMessageToTerminal("Command not recognized");
                return;
            }

            if (!Commands.ContainsKey(splitInput[0]))
            {
                AddMessageToTerminal("Command not recognized");
            }
            else
            {
                Commands[splitInput[0]].RunCommand();
            }

            terminalInput.text = "";
        }
    }
}