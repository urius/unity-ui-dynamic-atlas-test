using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [SerializeField]
    private ItemIconsProvider _itemIconsProvider;

    public override void InstallBindings()
    {
        Debug.Log("ProjectContextInstaller::InstallBindings, ItemIconsProvider: ");

        Container.BindInstance<ItemIconsProvider>(_itemIconsProvider);
        
        Container.Bind<Model>().AsSingle();
    }
}