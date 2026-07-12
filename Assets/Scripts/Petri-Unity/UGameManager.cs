using Petricite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
        [SerializeField] private VisualTreeAsset buttonTemplate;
        public ListView buttonHolder;
        public Label choiceLabel;
        private List<VisualElement> spawnedButtons = new();

        public Sprite frontSprite;
        public Sprite backSprite;

        public Dictionary<Player, PoolRouterElement> poolRouters = new();


        private void Awake()
        {
            List<Player> players = new();
            List<Battlefield> battlefields = new();

            players.Add(new("Alice"));
            players.Add(new("Bob"));

            battlefields.Add(new("Zaun Warrens"));
            battlefields.Add(new("The Papertree"));
            playArea = new(players, battlefields);
            ParsePlayArea();


            commandManager = new(players);

            Card.OnCardCreated += CardCreated;
            Card.OnZoneChange += CardMoved;

            IReadyable.OnReadyChange += OnReadyChange;

            choiceLabel = document.rootVisualElement.Q<Label>("ChoiceLabel");
            buttonHolder = document.rootVisualElement.Q<ListView>("ButtonHolder");

            playArea.aliceCard = new(playArea.playerBases[players[0]], players[0], "Albany", "NO_ID", 0, 0, 0, TempAliceInit);// new() { new("TEMP ABILITY", TempAlicePreTask, TempAliceTask) }

            playArea.bobCard = new(playArea.playerBases[players[1]], players[1], "Baltimore", "NO_ID", 0, 0, 0);

            foreach (var player in players)
            {
                for (int i = 0; i < 12; i++)
                {
                    playArea.playerRuneDecks[player].cards.Add(CardFactory.CreateRune("ogn-007", playArea.playerRuneDecks[player], player));
                }
            }
            
            ChoiceManager.OnChoices += OnChoices;
            ChoiceManager.OnChosen += OnChosen;
            BaseChoiceCommand.OnChoiceFinished += OnMultichoiceSelectionFinish;
            BaseChoiceCommand.OnNewChoice += OnMultichoiceNewChoice;

            document.rootVisualElement.Q<Button>("TempButton").clicked += () => CardFactory.CreateUnit("ogn-001", playArea.playerHands[players[0]], players[0]);//document.rootVisualElement.Q<DeckElement>("MainDeckZone").Add(new Card);
            document.rootVisualElement.Q<Button>("ChannelButton").clicked += () => playArea.Channel(players[0]);
            
        }

        public void TempAliceInit(Card card)// ok now its time to go back to card factory
        {
            void onPlay(PlayableCard playable)
            {
                Debug.Log($"On play rule! with {playable.Name}");

                if (playable == card)
                {
                    Debug.Log($"On play rule and its me! {playable.Name}");
                }
            }

            PlayArea.AddRule(PlayableCard.OnCardPlayed.CreateRule(card, onPlay));
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

        private void OnReadyChange(IReadyable readyable, bool ready)
        {
            if (readyable is Card permanent)
            {
                if (!cardToVE.ContainsKey(permanent)) return;

                Debug.Log("CARD FOUND IN ONREADYCHANGE");

                var cardVE = cardToVE[permanent];

                //float oldRot = cardVE.resolvedStyle.rotate.angle.value;
                float newRot = ready ? 0 : 90;

                cardVE.style.rotate = new(new Angle(newRot));
            }
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

        private void CardCreated(Card card)
        {
            if (zoneToVE[card.Zone] != null)
            {
                SpawnCard(card);
            }
        }
        private void SpawnCard(Card card)
        {
            
            CardElement newCardVE = new(frontSprite);

            zoneToVE[card.Zone].Add(newCardVE);
            
            cardToVE.Add(card, newCardVE);

            Addressables.LoadAssetAsync<Sprite>(card.id).Completed += (handle) =>
            {
                if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    newCardVE.ReloadSprite(handle.Result);
                }
            };
        }

        private void CardMoved(Card card, Zone newZone)
        {
            zoneToVE[newZone].Add(cardToVE[card]);

            Debug.Log(zoneToVE[newZone].childCount + " swag");

        }

        public void ParsePlayArea()
        {
            for (int playerIndex = 0; playerIndex < playArea.players.Count; playerIndex++)
            {
                var player = playArea.players[playerIndex];

                var router = document.rootVisualElement.Q<PoolRouterElement>($"PoolRouter{playerIndex}");
                poolRouters[player] = router;
                router.Initialize(new() { Domain.Fury, Domain.Body }, player);

                var playerBoard = document.rootVisualElement.Q($"Player{playerIndex}Board");
                var playerBase = playerBoard.Q<ZoneElement>("BaseZone");

                zoneToVE.Add(playArea.playerBases[playArea.players[playerIndex]], playerBase);
                zoneToVE.Add(playArea.playerMainDecks[player], playerBoard.Q<ZoneElement>("MainDeckZone"));
                zoneToVE.Add(playArea.playerHands[player], document.rootVisualElement.Q<HandElement>($"Hand{playerIndex}Zone"));
                zoneToVE.Add(playArea.playerRuneDecks[player], playerBoard.Q<ZoneElement>("RuneDeckZone"));
                zoneToVE.Add(playArea.playerRuneZones[player], playerBoard.Q<ZoneElement>("RuneZone"));
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
