using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UITransitionsController : MonoBehaviour
{
    [SerializeField]
    private NavigationSetting[] _navigationSettings;

    private UITransitionsManager _transitionManager;

    [Inject]
    void Construct(UITransitionsManager transitionManager)
    {
        _transitionManager = transitionManager;
    }

    void OnEnable()
    {
        foreach (var setting in _navigationSettings)
        {
            setting.button.onClick.AddListener(() => NavigateToUIPrefab(setting.prefab));
        }
    }

    void OnDisable()
    {
        foreach (var setting in _navigationSettings)
        {
            setting.button.onClick.RemoveAllListeners();
        }
    }

    void NavigateToUIPrefab(GameObject prefab)
    {
        _transitionManager.NavigateToPrefab(prefab);
        Destroy(gameObject);
    }

    [Serializable]
    private class NavigationSetting
    {
        public Button button;
        public GameObject prefab;
    }
}

