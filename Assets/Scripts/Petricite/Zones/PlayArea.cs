using System.Collections.Generic;
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
        public Dictionary<Player, Dictionary<Battlefield, Zone>> playerBattlefields = new();
        public PlayArea(List<Player> newPlayers, List<Battlefield> battlefields)
        {
            this.battlefields = new(battlefields);
            foreach (var player in newPlayers)
            {
                players.Add(player);

                playerBases.Add(player, new Location($"{player.Name}'s base", int.MaxValue));

                var newDict = new Dictionary<Battlefield, Zone>();
                foreach (var battlefield in battlefields)
                {
                    newDict.Add(battlefield, new Location($"{player.Name}'s {battlefield.Name}", int.MaxValue));
                }
                playerBattlefields.Add(player, newDict);
            }


        }

        


    }
}
