using System;
using UnityEngine;
using UnityEngine.UI;

public class CreateApplyButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private Text _text;

    public event Action Click = delegate { };

    public void ToApplyMode()
    {
        _text.text = "Apply";
    }

    public void ToCreateMode()
    {
        _text.text = "Create";
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Click();
    }
}
