using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameManagerScriptableObject", menuName = "ScriptableObjects/Game Manager")]
public class GameManagerScriptableObject : ScriptableObject
{
    public int _score = 0;
    public int _highScore = 0;
    public float _fillAmount = 0f;
    public bool _gameOver = false;

    [System.NonSerialized] public UnityEvent<int> _scoreChangeEvent;
    [System.NonSerialized] public UnityEvent<float> _fillChangeEvent;
    [System.NonSerialized] public UnityEvent<bool> _gameOverEvent;

    private void OnEnable() {
        _score = 0;
        _fillAmount = 0f;
        _gameOver = false;

        if(_scoreChangeEvent == null)
            _scoreChangeEvent = new UnityEvent<int>();
        if(_fillChangeEvent == null)
            _fillChangeEvent = new UnityEvent<float>(); 
        if(_gameOverEvent == null)
            _gameOverEvent = new UnityEvent<bool>();               
    }

    public void ChangeScore(int value) {
        _score += value;
        _scoreChangeEvent.Invoke(_score);
        
        if(value > 0) 
            ChangeFillAmount(-0.045f);
    }

    public void ChangeFillAmount(float value) {
        _fillAmount += value;
        
        if(_fillAmount >= 1)
            GameOver();
        if(_fillAmount < 0)
            _fillAmount = 0f;

        _fillChangeEvent.Invoke(_fillAmount);
    }

    public void GameOver() {
        _gameOver = true;
        _gameOverEvent.Invoke(_gameOver);
    }

    public void SaveScore() {
        _highScore = _score;        
    }
}