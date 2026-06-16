using Petricite;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Petrunity
{
    public class UGameManager : MonoBehaviour
    {
        public ChoiceManager choiceManager = new();
        public CommandManager commandManager;
        public PlayArea playArea;

        private Dictionary<Zone, VisualElement> zoneToVE = new();
        private Dictionary<Card, VisualElement> cardToVE = new();

        [SerializeField] private UIDocument document;
        [SerializeField] private VisualTreeAsset cardTemplate;
        [SerializeField] private VisualTreeAsset buttonTemplate;
        public ListView buttonHolder;
        public Label choiceLabel;
        private List<VisualElement> spawnedButtons = new();


        private void Awake()
        {
            List<Player> players = new();
            List<Battlefield> battlefields = new();

            players.Add(new("Alice", new()));
            players.Add(new("Bob", new()));

            battlefields.Add(new("Zaun Warrens"));
            battlefields.Add(new("The Papertree"));
            playArea = new(players, battlefields);
            ParsePlayArea();


            commandManager = new(players);

            Unit.OnUnitPlayed += UnitPlayed;
            Card.OnZoneChange += CardMoved;

            choiceLabel = document.rootVisualElement.Q<Label>("ChoiceLabel");
            buttonHolder = document.rootVisualElement.Q<ListView>("ButtonHolder");

            playArea.aliceCard = new(playArea.playerBases[players[0]], players[0], "Albany", 0, 0, 0);
            playArea.bobCard = new(playArea.playerBases[players[1]], players[1], "Baltimore", 0, 0, 0);

            ChoiceManager.OnChoices += PromptChoices;
            ChoiceManager.OnChosen += ClearChoices;

        }

        private void ClearChoices(IChoosable chosen)
        {
            choiceLabel.text = "No active choice";

            buttonHolder.itemsSource = null;
            buttonHolder.Rebuild();

            foreach (var button in spawnedButtons)
            {
                button.RemoveFromHierarchy();
            }

            spawnedButtons.Clear();

        }

        private void PromptChoices(IEnumerable<IChoosable> choices, string choiceTitle, Player player)
        {
            //IMPORTANT TO NOTE THAT CHOICE CAN BE NULL

            var choiceList = choices.ToList();

            choiceLabel.text = choiceTitle;



            buttonHolder.makeItem = () => 
            {
                var newButton = buttonTemplate.CloneTree();
                spawnedButtons.Add(newButton);
                return newButton; 
            };

            buttonHolder.bindItem = (element, i) =>
            {
                var button = element.Q<Button>();
                var choiceName = choiceList[i]?.Name ?? "Done";

                button.text = choiceName;
                button.clicked += () => ChoiceManager.Choose(choiceList[i]);
            };
            
            buttonHolder.itemsSource = choiceList;
            buttonHolder.Rebuild();

        }

        private void UnitPlayed(Unit unit)
        {
            if (zoneToVE[unit.Zone] != null)
            {
                SpawnCard(unit);
            }
        }
        private void SpawnCard(Card card)
        {
            VisualElement newCardVE = cardTemplate.CloneTree();

            zoneToVE[card.Zone].Add(newCardVE);
            
            cardToVE.Add(card, newCardVE);
        }

        private void CardMoved(Card card, Zone newZone)
        {
            zoneToVE[newZone].Add(cardToVE[card]);
        }

        public void ParsePlayArea()
        {
            for (int playerIndex = 0; playerIndex < playArea.players.Count; playerIndex++)
            {

                var playerBoard = document.rootVisualElement.Q($"Player{playerIndex}Board");
                var playerBase = playerBoard.Q("BaseZone");

                zoneToVE.Add(playArea.playerBases[playArea.players[playerIndex]], playerBase);

                for (int i = 0; i < 2; i++)
                {
                    var location = playArea.playerBattlefields[playArea.players[playerIndex]][playArea.battlefields[i]];

                    zoneToVE.Add(location, playerBoard.Q($"Battlefield{i}Zone"));
                }
            }

            foreach (var pair in zoneToVE)
            {
                Debug.Log(pair.Key.Name + " " + pair.Value.name);
            }
        }

        private void Start()
        {
            CommandManager.StartNextTurn();
            commandManager.ProcessCommand();

        }
    }
}
