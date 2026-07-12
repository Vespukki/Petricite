using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Petricite
{
    public class PlayArea
    {

        public Unit aliceCard;
        public Unit bobCard;

        public List<Player> players = new();
        public List<Battlefield> battlefields = new();

        public Dictionary<Player, Zone> playerBases = new();
        public Dictionary<Player, Zone> playerMainDecks = new();
        public Dictionary<Player, Dictionary<Battlefield, Zone>> playerBattlefields = new();
        public Dictionary<Player, Hand> playerHands = new();
        public Dictionary<Player, Zone> playerRuneDecks = new();
        public Dictionary<Player, Zone> playerRuneZones = new();

        private static List<Rule> rules = new();
        private static List<Ability> abilities = new();

        public static event Action<Player> OnChannel;
        public static void AddRule(Rule rule)
        {
            rules.Add(rule);
        }

        public static void RemoveRule(Rule rule)
        {
            rules.Remove(rule);
        }

        public static void AddAbility(Ability ab)
        {
            abilities.Add(ab);
        }
        public static void RemoveAbility(Ability ab)
        {
            abilities.Remove(ab);
        }

        public PlayArea(List<Player> newPlayers, List<Battlefield> battlefields)
        {
            this.battlefields = new(battlefields);
            foreach (var player in newPlayers)
            {
                players.Add(player);

                playerBases.Add(player, new Location($"{player.Name}'s base", int.MaxValue, player));
                playerMainDecks.Add(player, new Zone($"{player.Name}'s Main Deck", int.MaxValue, player));
                playerHands.Add(player, new Hand($"{player.Name}'s Hand", int.MaxValue, player));
                playerRuneDecks.Add(player, new Zone($"{player.Name}'s Rune Deck", int.MaxValue, player));
                playerRuneZones.Add(player, new Zone($"{player.Name}'s Rune Zone", int.MaxValue, player, true));

                
                var newDict = new Dictionary<Battlefield, Zone>();
                foreach (var battlefield in battlefields)
                {
                    newDict.Add(battlefield, new Location($"{player.Name}'s {battlefield.Name}", int.MaxValue, player));
                }
                playerBattlefields.Add(player, newDict);
            }


        }


        public void Channel(Player player)
        {
            if (playerRuneDecks[player].CardCount == 0) return;
            
            OnChannel?.Invoke(player);
            playerRuneDecks[player].cards[0].Zone = playerRuneZones[player];
        }


    }
}
