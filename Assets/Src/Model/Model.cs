using System;
using System.Collections.Generic;

public class Model
{
    public readonly UserSquad userSquad = new UserSquad();
}

public class UserSquad
{
    public event Action<UnitConfig> UnitAdded = delegate { };
    private readonly List<UnitConfig> _units = new List<UnitConfig>();

    public int UnitsCount => _units.Count;

    public UnitConfig GetUnitAt(int index)
    {
        if (index < _units.Count)
        {
            return _units[index];
        }
        return null;
    }

    public void AddUnit(UnitConfig unit)
    {
        _units.Add(unit);
        UnitAdded(unit);
    }
}

public class UnitConfig
{
    public ChassiConfig chassiConfig;
    public HeadConfig headConfig;
}
