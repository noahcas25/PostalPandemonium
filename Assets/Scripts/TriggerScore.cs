using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScore : MonoBehaviour
{
    [SerializeField] private string _color;
    [SerializeField] private GameManagerScriptableObject _gameManagerScriptableObject;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObjectPool _boxPool;

    private void CheckBox(GameObject box) {
        print(box.GetComponent<Box>().GetColor() + " : " + _color);
        if(box.GetComponent<Box>().GetColor().Equals(_color)) {
            _gameManagerScriptableObject.ChangeScore(1);
            _uiManager.ClearDeltaTime();
        } else _gameManagerScriptableObject.ChangeScore(-1);

        _boxPool.ReturnToPool(box);
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Box"))
            CheckBox(other.gameObject);
    }
}
