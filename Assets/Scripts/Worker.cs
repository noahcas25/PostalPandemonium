using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator _anim;
    private float _turnSmoothVelocity;
    private GameObject _boxHeld;
    private Rigidbody _boxHeldRB;
    private Vector3 startPosition;
    private bool _hasBox;
    private Vector3 _direction;

    private void Awake() => startPosition = transform.position;

    private void Update() { 
        if(Input.GetKeyDown("j")) 
            Respawn();
        if(Input.GetKeyDown("k"))
            ThrowBox();
    }

    private void FixedUpdate() => Move();   

    private void Move() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(_direction.magnitude >= 0.1f) {
            if(_hasBox)
                _anim.Play("Running");
            else _anim.Play("RunningNoBox");

            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, .1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            _direction.Normalize();
            GetComponent<Rigidbody>().MovePosition(transform.position + _direction * Time.deltaTime * 10f);
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
        _boxHeldRB = _boxHeld.GetComponent<Rigidbody>();
        _boxHeld.transform.SetParent(transform);

        _boxHeld.transform.position = transform.GetChild(0).position;
        _boxHeld.GetComponent<MeshCollider>().enabled = false;
        _boxHeld.GetComponent<Collider>().enabled = false;
        _boxHeldRB.velocity = new Vector3(0, 0, 0);
        _boxHeldRB.useGravity = false;
        _boxHeldRB.freezeRotation = true;
        _boxHeldRB.isKinematic = true;
    }

    private void DropBox() {
        if(!_hasBox) return;

         _hasBox = false;
        _boxHeld.transform.SetParent(null);
        _boxHeld.GetComponent<MeshCollider>().enabled = true;
        _boxHeldRB.useGravity = true;
        _boxHeldRB.isKinematic = false;
        _boxHeldRB.freezeRotation = false;
        _boxHeldRB.AddForce(0, 2, 5, ForceMode.Impulse);
    }

    private void ThrowBox() {
        if(!_hasBox) return;
        
        DropBox();
        _boxHeld.GetComponent<Rigidbody>().AddForce(_direction * 5f + new Vector3(0, 2, 0), ForceMode.Impulse);
    }

    private void Respawn() {
        transform.position = startPosition;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Box")) 
            PickUpBox(other.gameObject);
        if(other.CompareTag("DropOff")) 
            DropBox();
        if(other.CompareTag("Wall"))
            transform.Rotate(0, 180, 0);
    }
}
