using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManagerScriptableObject _gameManagerScriptableObject;
    [SerializeField] private GameObjectPool _boxPool;
    [SerializeField] private Text _text, _gameOverCanvasText;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private AudioClip _alarm;

    private float _deltaTime = 0f;
    private float _timer = 1.5f;
    private bool _shouldCheck = true;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        ChangeScoreText(_gameManagerScriptableObject._score);
        _boxPool.AddToPool(50);
    }

    private void FixedUpdate() {
         _deltaTime += Time.fixedDeltaTime;

        if(_deltaTime > _timer && _shouldCheck) 
            StartCoroutine(TimerCoroutine());
    }

    private void OnEnable() {
        _gameManagerScriptableObject._scoreChangeEvent.AddListener(ChangeScoreText);
        _gameManagerScriptableObject._fillChangeEvent.AddListener(ChangeFillAmount);
        _gameManagerScriptableObject._gameOverEvent.AddListener(GameOver);
    }

    private void OnDisable() {
        _gameManagerScriptableObject._scoreChangeEvent.RemoveListener(ChangeScoreText);
        _gameManagerScriptableObject._fillChangeEvent.RemoveListener(ChangeFillAmount);
        _gameManagerScriptableObject._gameOverEvent.RemoveListener(GameOver);
    }

    public void ChangeScoreText(int score) {
        _text.text = score + "";
    }

    public void ChangeFillAmount(float amount) {
        _healthBar.fillAmount = amount;

        if(amount > 0.6)
            _audioSource.PlayOneShot(_alarm);
    }

    private void GameOver(bool gameOver) {
        if(gameOver) {
            Time.timeScale = 0f;
            _gameOverCanvasText.text = _text.text;
            _gameOverCanvas.gameObject.SetActive(true);
        }
    }
    public void ClearDeltaTime() => _deltaTime = 0f;

    public void Restart() => SceneManager.LoadScene("GameScene");

    public void Quit() => Application.Quit();
    
    private IEnumerator TimerCoroutine() {
        _shouldCheck = false;
        _gameManagerScriptableObject.ChangeFillAmount(0.045f);
        yield return new WaitForSeconds(1f);
        _shouldCheck = true;
    }
}
