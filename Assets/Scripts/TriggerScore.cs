using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScore : MonoBehaviour
{
    [SerializeField] private string _color;
    [SerializeField] private GameManagerScriptableObject _gameManagerScriptableObject;

    private void CheckBox(GameObject box) {
        print(box.GetComponent<Box>().GetColor() + " : " + _color);
        if(box.GetComponent<Box>().GetColor().Equals(_color)) {
            _gameManagerScriptableObject.ChangeScore(1);
        } else _gameManagerScriptableObject.ChangeScore(-1);

        Destroy(box);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Box"))
            CheckBox(other.gameObject);

    }

}
