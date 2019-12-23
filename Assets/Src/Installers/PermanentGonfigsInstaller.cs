using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PermanentGonfigsInstaller", menuName = "Installers/PermanentGonfigsInstaller")]
public class PermanentGonfigsInstaller : ScriptableObjectInstaller<PermanentGonfigsInstaller>
{
    public ChassiConfigs chassiConfigs;
    public HeadConfigs headConfigs;

    public override void InstallBindings()
    {
        Debug.Log("PermanentGonfigsInstaller::InstallBindings");

        Container.BindInstance<ChassiConfigs>(chassiConfigs);
        Container.BindInstance<HeadConfigs>(headConfigs);
    }
}


[Serializable]
public abstract class PartConfigBase
{
    public PartType type;
    public int cost;
    public GameObject prefab;
}

[Serializable]
public class ChassiConfigs
{
    public ChassiConfig[] chassisConfigs;
}


[Serializable]
public class ChassiConfig : PartConfigBase
{
    public int speed;
    public int turnSpeed;
}

[Serializable]
public class HeadConfigs
{
    public HeadConfig[] headConfigs;
}

[Serializable]
public class HeadConfig : PartConfigBase
{
    public int radius;
    public int turnSpeed;
}

public enum PartType
{
    Chassi = 0,
    Head = 1,
}