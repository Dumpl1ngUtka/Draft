using Random = UnityEngine.Random;

namespace Battle.PassiveEffects
{
    public static class PassiveEffectsCombination
    {
        public static CombinationResult Check(PassiveEffect availableEffect, PassiveEffect additionalEffect)
        {
            switch (availableEffect)
            {
                case Slippery slippery when additionalEffect is AddArmor:
                    return new CombinationResult(true, destoyAvailableEffect: false)
                    {
                        AddNewEffect = !(Random.Range(0f, 1f) < slippery.ProcChance)
                    };
                case AddArmor when additionalEffect is Slippery slippery:
                {
                    return new CombinationResult(true, addNewEffect: true)
                    {
                        DestoyAvailableEffect = !(Random.Range(0f, 1f) < slippery.ProcChance)
                    };
                }
            }

            return new CombinationResult(false);
        }
    }

    public struct CombinationResult
    {
        public bool IsCombined;
        public bool DestoyAvailableEffect;
        public bool AddNewEffect;
        public PassiveEffect CombinedEffect;

        public CombinationResult(bool isCombined, bool destoyAvailableEffect = false, bool addNewEffect = true, PassiveEffect combinedEffect = null)
        {
            IsCombined = isCombined;
            DestoyAvailableEffect = destoyAvailableEffect;
            AddNewEffect = addNewEffect;
            CombinedEffect = combinedEffect;
        }
    }
}