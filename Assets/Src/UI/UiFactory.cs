using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UiFactory
{
    private DiContainer _diContainer;

    [Inject]
    public void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public GameObject Instantiate(GameObject prefab, Transform parentTransform)
    {
        return _diContainer.InstantiatePrefab(prefab, parentTransform);
    }
}
