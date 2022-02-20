using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator _anim;
    private float _turnSmoothVelocity;
    private bool _hasBox;
    private GameObject _boxHeld;

    private void Update() => Move();   

    private void Move() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f) {
            if(_hasBox)
                _anim.Play("Running");
            else _anim.Play("RunningNoBox");

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, .1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            direction.Normalize();
            direction *= 10f * Time.deltaTime;
            transform.Translate(direction, Space.World);
        } 
        else {
            if(_hasBox) 
                _anim.Play("Carrying");
            else _anim.Play("Breathing Idle");
        }
    }

    private void PickUpBox(GameObject box) {
        
        if(_hasBox) return;

        _hasBox = true;
        _boxHeld = box;
        _boxHeld.GetComponent<Rigidbody>().useGravity = false;
        _boxHeld.GetComponent<Rigidbody>().freezeRotation = true;
        _boxHeld.transform.position = transform.GetChild(0).position;
        _boxHeld.GetComponent<MeshCollider>().enabled = false;
        _boxHeld.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        _boxHeld.GetComponent<Rigidbody>().isKinematic = false;
        _boxHeld.GetComponent<Collider>().enabled = false;
        _boxHeld.transform.SetParent(transform);
        _boxHeld.transform.position = transform.GetChild(0).position;
    }

    private void DropBox() {
        if(!_hasBox) return;
        _boxHeld.GetComponent<Rigidbody>().useGravity = true;
        _boxHeld.GetComponent<Rigidbody>().freezeRotation = true;
         _boxHeld.GetComponent<MeshCollider>().enabled = true;
        _boxHeld.transform.SetParent(null);
        _hasBox = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Box")) 
            PickUpBox(other.gameObject);
        if(other.CompareTag("DropOff")) 
            DropBox();
    }


}
