using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameManagerScriptableObject", menuName = "ScriptableObjects/Game Manager")]
public class GameManagerScriptableObject : ScriptableObject
{
    public int _score = 0;
    public int _highScore = 0;

    [System.NonSerialized] public UnityEvent<int> _scoreChangeEvent;

    private void OnEnable() {
        _score = 0;

        if(_scoreChangeEvent == null)
            _scoreChangeEvent = new UnityEvent<int>();
    }

    public void ChangeScore(int value) {
        _score += value;
        _scoreChangeEvent.Invoke(_score);
    }

    public void saveScore() {
        _highScore = _score;        
    }
}
