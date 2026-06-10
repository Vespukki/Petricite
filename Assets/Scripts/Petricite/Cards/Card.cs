namespace Petricite
{
    public class Card
    {
        protected string name;
        private bool exhausted;
        private Zone zone;
        public Card(Zone zone, string name = "UNNAMED CARD")
        {
            this.name = name;
            this.zone = zone;
        }

        public Zone Zone
        {
            get
            {
                return zone;
            }
            set
            {
                //HERE IS WHERE I WILL DO AN EVENT TO SEE IF ANYONE OBJECTS TO THE MOVE
                zone = value;
            }
        }

    }

}
