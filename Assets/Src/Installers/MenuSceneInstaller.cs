using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField]
    private UITransitionsManager _uiTransitionsManager;

    [SerializeField]
    private UnitReflector _unitReflector;

    public override void Start()
    {
        Debug.Log("MenuSceneInstaller::Start, _uiTransitionsManager is " + (_uiTransitionsManager));
    }
    public override void InstallBindings()
    {
        Debug.Log("MenuSceneInstaller");
        Container.BindInstance<UITransitionsManager>(_uiTransitionsManager);
        Container.BindInstance<UnitReflector>(_unitReflector);
        Container.Bind<UiFactory>().AsTransient();
    }
}