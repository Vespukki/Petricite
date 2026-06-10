using Petricite;
using UnityEngine;

namespace Petrunity
{
    public class UGameManager : MonoBehaviour
    {
        public ChoiceManager choiceManager = new();
        public CommandManager commandManager = new();
        public PlayArea playArea;

        private void Awake()
        {
            commandManager.ProcessCommand();
        }

        private void Start()
        {
            playArea.potOfGreed.StandardMove();
        }
    }
}
