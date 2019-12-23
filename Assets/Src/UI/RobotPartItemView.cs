using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RobotPartItemView : MonoBehaviour
{
    public event Action<PartConfigBase> ButtonClicked = delegate { };

    private PartConfigBase _config;

    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private Button _setButton;

    private ItemIconsProvider _itemIconsProvider;

    [Inject]
    public void Construct(ItemIconsProvider itemIconsProvider)
    {
        _itemIconsProvider = itemIconsProvider;
    }

    public void SetConfig(PartConfigBase config)
    {
        _config = config;

        Sprite iconSprite = null;
        if (config.type == PartType.Chassi)
        {
            iconSprite = _itemIconsProvider.GetChassiIcon(config.prefab);
        }
        else if (config.type == PartType.Head)
        {
            iconSprite = _itemIconsProvider.GetHeadsIcon(config.prefab);
        }
        SetImage(iconSprite);
    }

    private void OnEnable()
    {
        _setButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _setButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked(_config);
    }

    private void SetImage(Sprite imageSprite)
    {
        _itemImage.sprite = imageSprite;
    }
}
