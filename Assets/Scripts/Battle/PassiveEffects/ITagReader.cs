using System.Collections.Generic;
using Units;

namespace Battle.PassiveEffects
{
    public interface ITagReader
    {
        public PassiveEffect CheckTags(List<Tag> tags);
    }
}