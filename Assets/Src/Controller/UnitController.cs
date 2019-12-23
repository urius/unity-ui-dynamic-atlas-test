using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;
    private GameModel _gameModel;
    private Model _model;

    private Dictionary<UnitModel, UnitFacade> _unitsByModel = new Dictionary<UnitModel, UnitFacade>();

    [Inject]
    public void Construct(
        Model model,
        GameModel gameModel)
    {
        _model = model;
        _gameModel = gameModel;
    }

    void Start()
    {
        var squad = _model.userSquad;
        var team = 0;
        for (var i = 0; i < squad.UnitsCount; i++)
        {
            var unitConfig = squad.GetUnitAt(i);
            var spawnTransform = _spawnPoints[i].transform;
            var unitView = CreateUnitView(unitConfig, spawnTransform.position, spawnTransform.rotation);
            var unitModel = CreateUnitModel(unitConfig, team);
            _gameModel.AddUnit(unitModel);

            _unitsByModel.Add(unitModel, unitView);
        }
    }

    private UnitModel CreateUnitModel(UnitConfig unitConfig, int team)
    {
        var result = new UnitModel(unitConfig);
        result.Team = team;
        return result;
    }

    private UnitFacade CreateUnitView(UnitConfig unitConfig, Vector3 position, Quaternion rotation)
    {
        var unitFacade = Instantiate(unitConfig.chassiConfig.prefab, position, rotation)
                                        .GetComponent<UnitFacade>();
        Instantiate(unitConfig.headConfig.prefab, unitFacade.HeadPlaceholder);

        return unitFacade;
    }
}
