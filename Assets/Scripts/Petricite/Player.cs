using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Petricite
{
    public class Player : IChoosable
    {
        private string name;
        public string Name => name;

        private List<Resource> resourcePool = new();

        public event Action<Player, List<Resource>> OnResourceChanged;

        public void AddResource(Resource power)
        {
            resourcePool.Add(power);
            OnResourceChanged?.Invoke(this, resourcePool);
        }

        public List<Resource> GetResourcesOfDomain(Domain domain)
        {
            return resourcePool.Where((resource) => resource.domain == domain).ToList();
        }

        public void ClearResources()
        {
            resourcePool.Clear();
        }

        public Player(string name)
        {
            this.name = name;
        }

    }
}
