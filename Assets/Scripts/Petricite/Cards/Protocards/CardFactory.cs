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

        public static Rune CreateRune(string id, Zone zone, Player controller)
        {
            Rune final = id switch
            {

                "ogn-007" => new Rune(zone, controller, "Fury Rune", "ogn-007", (card) =>
                {
                    PlayArea.AddAbility(new(card, "Add energy", () => { return Task.CompletedTask; }, () => ChooseToExhaustMe(card), () => 
                    {
                        if (card is IReadyable readyable)
                        {
                            return card.Zone.boardZone && readyable.Ready && card.controller == CommandManager.currentTurnPlayer;
                        }
                        return false;
                    }));
                    PlayArea.AddAbility(new(card, "Add power", () => { return Task.CompletedTask; }, () => ChooseToRecycleMe(card), () => 
                    {
                        if (card is IReadyable readyable)
                        {
                            return card.Zone.boardZone && readyable.Ready && card.controller == CommandManager.currentTurnPlayer;
                        }
                        return false;
                    }));
                }),
                _ => null
            };

            return final;
        }
        public static async Task ChooseToRecycleMe(Card card)
        {
            if (card is IReadyable readyable)
            {
                ChoiceCommand<Card> choice = new(card.controller, new() { card }, "Recycle?", true);
                await choice.Execute();

                if (choice.result != null)
                {
                    card.controller.AddResource(new(Domain.All));
                    Debug.Log(card.controller.GetResourcesOfDomain(Domain.All).Count + " is current power pool"); //YOU NEED TO ADD THE RECYCLE PART
                }
            }
        }

        public static async Task ChooseToExhaustMe(Card card)
        {
            if (card is IReadyable readyable)
            {
                ChoiceCommand<Card> choice = new(card.controller, new() { card }, "Exhaust?", true);
                await choice.Execute();

                if (choice.result != null)
                {
                    readyable.Ready = false;
                    card.controller.AddResource(new(Domain.None));
                    Debug.Log(card.controller.GetResourcesOfDomain(Domain.None).Count + " is current energy pool");
                }
            }
        }
    }
}
