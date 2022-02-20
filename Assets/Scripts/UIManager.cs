using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManagerScriptableObject _gameManagerScriptableObject;
    [SerializeField] private Text _text;

    private void Start() {
        ChangeScoreText(_gameManagerScriptableObject._score);
    }

    private void OnEnable() {
        _gameManagerScriptableObject._scoreChangeEvent.AddListener(ChangeScoreText);
    }

    private void OnDisable() {
        _gameManagerScriptableObject._scoreChangeEvent.RemoveListener(ChangeScoreText);
    }

    public void ChangeScoreText(int score) {
        _text.text = score + "";
    }
}
