using Petricite;
using UnityEngine;

namespace Petrunity
{
    public class PlayArea : MonoBehaviour
    {
        public Location playerBase = null;
        public Location battlefield1 = null;
        public Location battlefield2 = null;

        public Unit potOfGreed = null;


        private void Awake()
        {
            playerBase = new("Base", 10);
            battlefield1 = new("Battlefield 1", 1);
            battlefield2 = new("Battlefield 2", 0);

            potOfGreed = new(playerBase, "Pot of Greed", 0, 0, 0);
        }

        private void Update()
        {
            Debug.Log(potOfGreed.Zone.name);
        }

        private void Start()
        {
            //ok, now i gotta make it so that all cards inherently have a location. specifically, all cards MUST have a location, and cannot exist without one.
            //Does that make sense long term? or do i wanna be able to make cards exist before i bring them into the game? maybe I want a separate class called proto-card
            //for holding card info without bringing them into the world.
        }
    }
}
