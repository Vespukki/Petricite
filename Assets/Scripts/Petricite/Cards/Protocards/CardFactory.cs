using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;

namespace Petricite
{
    public static class CardFactory
    {
        public static Unit CreateUnit(string id, Zone zone, Player controller)
        {
            Unit final = id switch
            {
                "ogn-001" => new Unit(zone, controller, "Blazing Scorcher", id, 0, 5, 5, (card) => 
                {
                    PlayArea.AddRule(PlayableCard.OnCardPlayed.CreateRule(card, (playableCard) => Accelerate(card, playableCard) ));
                }),
                
                _ => null
            };


            return final;
        }

        public static void Accelerate(Card original, PlayableCard playedCard)
        {
            if (original != playedCard)
            {
                Debug.Log("ACCELERATE CARD NO MATCHUP");
                return;
            }



            CommandManager.PushCommand(new AccelerateCommand(playedCard));
            /*CommandManager.PushCommand(genCom); //ok I need to make choice commands point to what to do next maybe? or like idk cuz i need to push a 
                //choice here and then have that choice potential do more stuff. i guess its kinda all happening at the same time with no room for other stuff so maybe i 
                //just modify the part that takes accelerate as an arg and make it take a task to run instead of a regular method so i can do this stuff
            */
        }
    }
}
