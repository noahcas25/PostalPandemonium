using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScore : MonoBehaviour
{
    [SerializeField] private string _color;
    [SerializeField] private ScriptableObject _gameManager;

    private void CheckBox(GameObject box) {
        if(box.GetComponent<Box>().GetColor().Equals(_color)) {
            // _gameManager
            // Do Something in the ScriptableObject
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Box"))
            CheckBox(other.gameObject);

    }

}
