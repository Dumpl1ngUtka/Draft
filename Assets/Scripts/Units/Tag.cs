using Battle.PassiveEffects;

namespace Units
{
    public struct Tag
    {
        public string Name;
        public PassiveEffect TagHolder;

        public Tag(string name, PassiveEffect tagHolder)
        {
            Name = name;
            TagHolder = tagHolder;
        }
    }
}