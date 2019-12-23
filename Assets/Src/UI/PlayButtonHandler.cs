using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Button _playButton;
    
    [SerializeField]
    private String _gameSceneName;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPLayButtonClicked);
    }

    private void OnPLayButtonClicked()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPLayButtonClicked);
    }
}
