using UnityEngine;
using Zenject;

public class UITransitionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiCanvas;

    private DiContainer _diContainer;
    
    [Inject]
    void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void NavigateToPrefab(GameObject prefab)
    {
        _diContainer.InstantiatePrefab(prefab, _uiCanvas.transform);
    }
}
