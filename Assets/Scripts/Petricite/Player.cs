using NUnit.Framework;
using System.Collections.Generic;

namespace Petricite
{
    public class Player : IChoosable
    {
        private string name;
        public string Name => name;


        public Player(string name)
        {
            this.name = name;
        }

    }
}
