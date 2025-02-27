using UnityEngine;

namespace Battle.Units
{
    public class DuckMeshController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _duckParts;

        public void SetVisibility(bool visibility)
        {
            foreach (var duckPart in _duckParts)
                duckPart.SetActive(visibility);
        }

        public void SetClothes()
        {
            Debug.Log("D");
        }
    }
}
