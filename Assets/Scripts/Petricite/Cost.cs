namespace Petricite
{
    public struct Cost
    {
        public int energy;
        public int power;

        public Cost(int energy = 0, int power = 0)
        {
            this.energy = energy;
            this.power = power;
        }

        public static Cost Zero => new(0, 0);
        public static Cost Ones => new(1, 1);
        public static Cost OneP => new(0, 1);
        public static Cost OneE => new(1, 0);
    }
}
