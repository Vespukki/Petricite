using Petricite;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Petrunity
{
    public class PlayerBoard : MonoBehaviour
    {
        private Player player;

        public Transform baseTransform;
        public List<Transform> battlefieldTransforms = new();





        public void SetUpPlayerBoard(Player player)
        {
            this.player = player;
        }
    }
}
