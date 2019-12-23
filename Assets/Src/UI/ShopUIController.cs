using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _scrollContent;

    [SerializeField]
    private GameObject _itemPrefab;

    [SerializeField]
    private CreateApplyButton _createApplyButton;

    [SerializeField]
    private Button _showPrevUnitButton;

    [SerializeField]
    private Button _showNextUnitButton;

    [SerializeField]
    private Text _text;

    private ItemIconsProvider _itemIconsProvider;
    private ChassiConfig[] _chassiConfigs;
    private HeadConfig[] _headConfigs;
    private UiFactory _uiFactory;
    private UnitReflector _unitReflector;
    private PartConfigBase _chassiConfig;
    private PartConfigBase _headConfig;
    private UserSquad _userSquad;
    private int _currentUnitIndex;
    private UnitConfig _currentUnit;

    private readonly List<RobotPartItemView> _scrollItems = new List<RobotPartItemView>();

    [Inject]
    public void Construct(
        ItemIconsProvider itemIconsProvider,
        ChassiConfigs chassiConfigs,
        HeadConfigs headsConfigs,
        UiFactory uiFactory,
        UnitReflector unitReflector,
        Model model)
    {
        _itemIconsProvider = itemIconsProvider;
        _chassiConfigs = chassiConfigs.chassisConfigs;
        _headConfigs = headsConfigs.headConfigs;
        _uiFactory = uiFactory;
        _unitReflector = unitReflector;
        _userSquad = model.userSquad;
    }

    public void ShowChassis()
    {
        RemoveItems();

        foreach (var config in _chassiConfigs)
        {
            CreateScrollItem(config);
        }
    }

    public void ShowHeads()
    {
        RemoveItems();

        foreach (var config in _headConfigs)
        {
            CreateScrollItem(config);
        }
    }

    private void Awake()
    {
        if (_userSquad.UnitsCount == 0)
        {
            _currentUnitIndex = -1;
            _currentUnit = new UnitConfig();
        }
        else
        {
            _currentUnitIndex = 0;
            _currentUnit = _userSquad.GetUnitAt(_currentUnitIndex);
        }
    }

    private void CorrectIndex()
    {
        if (_currentUnitIndex < 0)
        {
            _currentUnitIndex = _userSquad.UnitsCount - 1;
        }
        else if (_currentUnitIndex > _userSquad.UnitsCount - 1)
        {
            _currentUnitIndex = 0;
        }
    }

    private void OnEnable()
    {
        ShowUnit(_currentUnit);
        UpdateButtons();
        UpdateTopText();

        _createApplyButton.Click += OnCreateApplyClick;
        _showNextUnitButton.onClick.AddListener(OnNextClicked);
        _showPrevUnitButton.onClick.AddListener(OnPrevClicked);
    }

    private void OnCreateApplyClick()
    {
        if (_currentUnitIndex == -1)
        {
            if (_currentUnit.headConfig != null && _currentUnit.chassiConfig != null)
            {
                _userSquad.AddUnit(_currentUnit);
                _currentUnitIndex = _userSquad.UnitsCount - 1;
            }
        }
        else
        {
            _currentUnitIndex = -1;
            _currentUnit = new UnitConfig();
        }

        UpdateTopText();
        ShowUnit(_currentUnit);

        UpdateButtons();
    }

    private void OnPrevClicked()
    {
        _currentUnitIndex--;
        ShowCurrentUnit();
    }

    private void OnNextClicked()
    {
        _currentUnitIndex++;
        ShowCurrentUnit();
    }

    private void ShowCurrentUnit()
    {
        CorrectIndex();

        UpdateButtons();
        UpdateTopText();

        _currentUnit = _userSquad.GetUnitAt(_currentUnitIndex);
        ShowUnit(_currentUnit);
    }

    private void UpdateTopText()
    {
        if (_currentUnitIndex >= 0)
        {
            _text.text = $"{_currentUnitIndex + 1}/{_userSquad.UnitsCount}";
        }
        else
        {
            _text.text = "creating...";
        }
    }

    private void ShowUnit(UnitConfig unit)
    {
        _unitReflector.ReflectUnit(unit.chassiConfig?.prefab, unit.headConfig?.prefab);
    }

    private void OnDisable()
    {
        _createApplyButton.Click -= OnCreateApplyClick;
        _showNextUnitButton.onClick.RemoveListener(OnNextClicked);
        _showPrevUnitButton.onClick.RemoveListener(OnPrevClicked);
    }

    private void UpdateButtons()
    {
        var isNavigationButtonsVisible = _userSquad.UnitsCount != 0;
        _showPrevUnitButton.gameObject.SetActive(isNavigationButtonsVisible);
        _showNextUnitButton.gameObject.SetActive(isNavigationButtonsVisible);

        if (_currentUnitIndex == -1)
        {
            _createApplyButton.ToApplyMode();
        }
        else
        {
            _createApplyButton.ToCreateMode();
        }
    }

    private void CreateScrollItem(PartConfigBase config)
    {
        var item = _uiFactory.Instantiate(_itemPrefab, _scrollContent.transform);

        var itemView = item.GetComponent<RobotPartItemView>();
        itemView.SetConfig(config);

        itemView.ButtonClicked += OnSetPartButtonClicked;

        _scrollItems.Add(itemView);
    }

    private void OnSetPartButtonClicked(PartConfigBase config)
    {
        if (config.type == PartType.Chassi)
        {
            _currentUnit.chassiConfig = (ChassiConfig)config;
        }
        else if (config.type == PartType.Head)
        {
            _currentUnit.headConfig = (HeadConfig)config;
        }

        ShowUnit(_currentUnit);
    }

    private void RemoveItems()
    {
        foreach (var item in _scrollItems)
        {
            item.ButtonClicked -= OnSetPartButtonClicked;
            Destroy(item.gameObject);
        }
        _scrollItems.Clear();
    }
}
