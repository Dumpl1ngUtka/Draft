namespace Battle.DamageSystem
{
    public struct Damage
    {
        public int Value { get; private set; }
        public DamageType DamageType { get; private set; }

        public Damage(int value, DamageType damageType)
        {
            Value = value;
            DamageType = damageType;
        }
    }
}