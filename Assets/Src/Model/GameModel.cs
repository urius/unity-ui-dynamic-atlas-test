using System.Collections;
using System.Collections.Generic;

public class GameModel
{
    public readonly IList<UnitModel> Units = new List<UnitModel>();

    public void AddUnit(UnitModel unit)
    {
        Units.Add(unit);
    }
}

public class UnitModel
{
    public int Team;
    public readonly UnitConfig Config;

    public UnitModel(UnitConfig config)
    {
        Config = config;
    }
}
