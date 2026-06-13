using Petricite;
using System.Collections.Generic;
using UnityEngine;

namespace Petrunity
{
    public class UGameManager : MonoBehaviour
    {
        public ChoiceManager choiceManager = new();
        public CommandManager commandManager = new();
        public PlayArea playArea;

        [SerializeField] private GameObject playerboardPrefab;
        [SerializeField] private Transform gameCanvas;

        [SerializeField] private GameObject cardPrefab;
        private Dictionary<Zone, Transform> zoneToTransform = new();
        private Dictionary<Card, CardObject> cardToObject = new();

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

            Unit.OnUnitPlayed += UnitPlayed;
            Card.OnZoneChange += CardMoved;

            playArea.aliceCard = new(playArea.playerBases[players[0]], players[0], "Albany", 0, 0, 0);
            playArea.bobCard = new(playArea.playerBases[players[1]], players[1], "Baltimore", 0, 0, 0);

            commandManager.ProcessCommand();
        }

        private void UnitPlayed(Unit unit)
        {
            if (zoneToTransform[unit.Zone] != null)
            {
                SpawnCard(unit, unit.Zone);
            }
        }
        private void SpawnCard(Card card, Zone zone)
        {
            var cardObject = Instantiate(cardPrefab, zoneToTransform[zone]).GetComponent<CardObject>();
            cardObject.card = card;

            cardToObject.Add(card, cardObject);
        }

        private void CardMoved(Card card, Zone newZone)
        {
            cardToObject[card].transform.SetParent(zoneToTransform[newZone]); //OK GOOD JOB GRIFFY NEXT FIX THE MOVEMENT FILTER OPTION 
        }

        public void ParsePlayArea()
        {
            foreach (var player in playArea.players)
            {
                var playerBoard = Instantiate(playerboardPrefab, gameCanvas).GetComponent<PlayerBoard>();
                playerBoard.SetUpPlayerBoard(player);

                zoneToTransform.Add(playArea.playerBases[player], playerBoard.baseTransform);

                for (int i = 0; i < 2; i++)
                {
                    var location = playArea.playerBattlefields[player][playArea.battlefields[i]];

                    zoneToTransform.Add(location, playerBoard.battlefieldTransforms[i]);
                }
            }

            foreach (var pair in zoneToTransform)
            {
                Debug.Log(pair.Key.ChoiceName + " " + pair.Value.name);
            }
        }

        private void Start()
        {
            playArea.aliceCard.StandardMove();
        }
    }
}
