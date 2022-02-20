using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
   [SerializeField] private Material _red, _green, _blue;
   [SerializeField] private Renderer mesh;
   private string _color;

   private void Start() {
      mesh.material = PickColor();
   }

   private Material PickColor() {
       switch(Random.Range(0,3)){
           case 0: _color = "red"; return _red;
           break;
           case 1: _color = "green"; return _green;
           break;
           case 2: _color = "blue"; return _blue;
           break;
           default: return _blue;
       }
   }

   public string GetColor() => _color;

   private void OnTriggerEnter(Collider other) {
       if(other.CompareTag("Ground")) {
           GetComponent<Collider>().enabled = true;
       }
   }
}
