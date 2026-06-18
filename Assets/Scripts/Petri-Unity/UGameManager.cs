using Petricite;
using System;
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

        private Dictionary<Zone, ZoneElement> zoneToVE = new();
        private Dictionary<Card, CardElement> cardToVE = new();

        [SerializeField] private UIDocument document;
        [SerializeField] private VisualTreeAsset cardTemplate;
        [SerializeField] private VisualTreeAsset buttonTemplate;
        public ListView buttonHolder;
        public Label choiceLabel;
        private List<VisualElement> spawnedButtons = new();

        public Sprite frontSprite;
        public Sprite backSprite;


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

            Permanent.OnReadyChange += OnReadyChange;

            choiceLabel = document.rootVisualElement.Q<Label>("ChoiceLabel");
            buttonHolder = document.rootVisualElement.Q<ListView>("ButtonHolder");

            playArea.aliceCard = new(playArea.playerBases[players[0]], players[0], "Albany", 0, 0, 0);
            playArea.bobCard = new(playArea.playerBases[players[1]], players[1], "Baltimore", 0, 0, 0);

            ChoiceManager.OnChoices += OnChoices;
            ChoiceManager.OnChosen += OnChosen;
            BaseChoiceCommand.OnChoiceFinished += OnMultichoiceSelectionFinish;
            BaseChoiceCommand.OnNewChoice += OnMultichoiceNewChoice;

            document.rootVisualElement.Q<Button>("TempButton").clicked += () => new Unit(playArea.playerMainDecks[players[0]], players[0]);//document.rootVisualElement.Q<DeckElement>("MainDeckZone").Add(new Card);
        }

        private void OnMultichoiceNewChoice(IChoosable chosen)
        {
            if (typeof(Card).IsAssignableFrom(chosen.GetType()))
            {
                SelectedHighlight(cardToVE[chosen as Card]);
            }
        }

        private void OnMultichoiceSelectionFinish(List<IChoosable> chosenList)
        {
            foreach (var chosen in chosenList)
            {
                if (typeof(Card).IsAssignableFrom(chosen.GetType()))
                {
                    Unhighlight(cardToVE[chosen as Card]);
                }

            }
        }

     

        private void OnChosen(IChoosable chosen)
        {
            choiceLabel.text = "No active choice";

            buttonHolder.itemsSource = null;
            buttonHolder.Rebuild();

            foreach (var button in spawnedButtons)
            {
                button.RemoveFromHierarchy();
            }

            spawnedButtons.Clear();


            foreach (var ve in cardToVE.Values)
            {
                Unhighlight(ve);
            }

        }

        private void OnReadyChange(Permanent permanent, bool ready)
        {
            if (!cardToVE.ContainsKey(permanent)) return;

            Debug.Log("CARD FOUND IN ONREADYCHANGE");

            var cardVE = cardToVE[permanent];

            //float oldRot = cardVE.resolvedStyle.rotate.angle.value;
            float newRot = ready ? 0 : 90;

            cardVE.style.rotate = new(new Angle(newRot));
        }

        private void OnChoices(IEnumerable<IChoosable> choices, string choiceTitle, Player player)
        {
            //IMPORTANT TO NOTE THAT CHOICE CAN BE NULL

            var choiceList = choices.ToList();

            foreach (var choice in choices)
            {
                if (choice == null) continue;

                if (typeof(Card).IsAssignableFrom(choice.GetType()))
                {
                    var card = choice as Card;
                    if (cardToVE.ContainsKey(card))
                    {
                        Highlight(cardToVE[card]);
                    }
                    else
                    {
                        Unhighlight(cardToVE[card]);
                    }
                }
                
            }

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

        private void Unhighlight(VisualElement visualElement)
        {
            if (visualElement.ClassListContains("Highlighted")) visualElement.RemoveFromClassList("Highlighted");
            if (visualElement.ClassListContains("Selected")) visualElement.RemoveFromClassList("Selected");
        }

        private void Highlight(VisualElement visualElement)
        {
            visualElement.AddToClassList("Highlighted");


        }

        private void SelectedHighlight(VisualElement ve)
        {
            ve.AddToClassList("Selected");
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
            CardElement newCardVE = new(frontSprite, backSprite);

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
                var player = playArea.players[playerIndex];

                var playerBoard = document.rootVisualElement.Q($"Player{playerIndex}Board");
                var playerBase = playerBoard.Q<ZoneElement>("BaseZone");

                zoneToVE.Add(playArea.playerBases[playArea.players[playerIndex]], playerBase);
                zoneToVE.Add(playArea.playerMainDecks[player], playerBoard.Q<ZoneElement>("MainDeckZone"));
                for (int i = 0; i < 2; i++)
                {
                    var location = playArea.playerBattlefields[playArea.players[playerIndex]][playArea.battlefields[i]];

                    zoneToVE.Add(location, playerBoard.Q<ZoneElement>($"Battlefield{i}Zone"));
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
