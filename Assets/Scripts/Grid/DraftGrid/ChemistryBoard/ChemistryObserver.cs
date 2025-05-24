using System;
using TMPro;
using Units;
using UnityEngine;

namespace Grid.DraftGrid.ChemistryBoard
{
    public class ChemistryObserver : MonoBehaviour, CustomObserver.IObserver<ChemestryInteractor>
    {
        [SerializeField] private Sprite _covenantSprite;
        [SerializeField] private ChemistryObserverCell _cellPrefab;
        [SerializeField] private TMP_Text _totalChemistryText;
        [SerializeField] private Transform _raceContainer;
        [SerializeField] private Transform _covenantTypeContainer;
        [SerializeField] private Transform _covenantContainer;
        
        private CustomObserver.IObservable<ChemestryInteractor> _observable;
        public void Init(CustomObserver.IObservable<ChemestryInteractor> observable)
        {
            _observable = observable;
            observable.AddObserver(this);
        }

        public void OnDisable()
        {
            _observable?.RemoveObserver(this);
        }

        public void UpdateObserver(ChemestryInteractor interactor)
        {
            _totalChemistryText.text = interactor.AllTeamChem.ToString();
            
            ClearContainer(_raceContainer);
            ClearContainer(_covenantContainer);
            ClearContainer(_covenantTypeContainer);

            foreach (var race in interactor.RaceCounts)
            {
                var instance = Instantiate(_cellPrefab, _raceContainer);
                instance.Init(race.Key.Icon, race.Value.ToString());
            }
            
            foreach (var type in interactor.CovenantTypeCounts)
            {
                var instance = Instantiate(_cellPrefab, _covenantTypeContainer);
                instance.Init(_covenantSprite, type.Value.ToString());
                var color = type.Key switch
                {
                    CovenantType.Blue => Color.blue,
                    CovenantType.Red => Color.red,
                    CovenantType.Yellow => Color.yellow,
                    _ => throw new ArgumentOutOfRangeException()
                };
                instance.SetImageColor(color);
            }
            
            foreach (var covenant in interactor.CovenantCounts)
            {
                var instance = Instantiate(_cellPrefab, _covenantContainer);
                instance.Init(covenant.Key.Icon, covenant.Value.ToString());
            }
        }

        private void ClearContainer(Transform container)
        {
            foreach(Transform child in container) 
                Destroy(child.gameObject);
        }
    }    
}