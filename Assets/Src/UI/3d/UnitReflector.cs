using UnityEngine;
using Zenject;

public class UnitReflector : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPlaceholder;

    private GameObject _reflectedGO;
    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void ReflectUnit(GameObject chassiPrefab, GameObject headPrefab)
    {
        if (_reflectedGO != null)
        {
            Destroy(_reflectedGO);
            _reflectedGO = null;
        }

        if (chassiPrefab != null)
        {
            _reflectedGO = _diContainer.InstantiatePrefab(chassiPrefab, _spawnPlaceholder);
            //_reflectedGO = Instantiate(chassiPrefab, _spawnPlaceholder);
            if (headPrefab != null)
            {
                var headPlaceholder = _reflectedGO.GetComponent<UnitFacade>().HeadPlaceholder;
                _diContainer.InstantiatePrefab(headPrefab, headPlaceholder);
            }
        }
    }

    public void Clear()
    {
        Destroy(_reflectedGO);
    }
}
