using UnityEngine;
using Zenject;

public class UnitChassiInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var headInstaller = gameObject.GetComponentInChildren<UnitHeadInstaller>();
        Debug.Log("headInstaller is: " + (headInstaller != null));
    }
}