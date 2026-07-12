using UnityEngine.UIElements;
using Petricite;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Petrunity
{
    [UxmlElement]
    public partial class PoolRouterElement : VisualElement
    {
        public Player player;
        public PoolRouterElement()
        {
        }
        public List<Resource> resources = new();

        public Dictionary<Domain, PoolElement> poolElements = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain">in almost every case the domain passed should be 2 domain flags</param>
        public void Initialize(List<Domain> domains, Player newPlayer)
        {
            if (player != null)
            {
                player.OnResourceChanged -= OnResourceChanged;
            }

            contentContainer.Clear();
            poolElements.Clear();

            this.player = newPlayer;
            newPlayer.OnResourceChanged += OnResourceChanged;

            var none = new PoolElement(Domain.None);
            contentContainer.Add(none);
            poolElements.Add(Domain.None, none);


            foreach (Domain domain in domains)
            {
                var dom = new PoolElement(domain);
                contentContainer.Add(dom);
                poolElements.Add(domain, dom);
            }

            var all = new PoolElement(Domain.All);
            contentContainer.Add(all);
            poolElements.Add(Domain.All, all);
        }

        private void OnResourceChanged(Player newPlayer, List<Resource> list)
        {
            if (player == newPlayer)
            {
                resources = list;
                ParseResources();
            }
        }

        private void ParseResources()
        {
            foreach (var domain in Enum.GetValues(typeof(Domain)).Cast<Domain>())
            {
                var valid = resources.Where((r) => r.domain == domain);

                if (poolElements.TryGetValue(domain, out var element))
                {
                    poolElements[domain].Set(valid.Count());
                }
            }
        }
    }
}
