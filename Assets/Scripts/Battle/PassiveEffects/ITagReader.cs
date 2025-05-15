using System.Collections.Generic;
using Battle.Units;

namespace Battle.PassiveEffects
{
    public interface ITagReader
    {
        public PassiveEffect CheckTags(List<Tag> tags);
    }
}