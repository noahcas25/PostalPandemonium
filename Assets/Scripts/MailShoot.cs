using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailShoot : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private bool leftSide;
    private bool _canShoot = true;
    private float _speed = 5f;

    private void Update() => Shoot();

    private void Shoot() {
        if(!_canShoot) return;

        StartCoroutine(ShootDelayCoroutine());
        GameObject package = Instantiate(_box);
        package.transform.position = _box.transform.position;
        package.SetActive(true);
        package.transform.Rotate(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));

        if(leftSide)
            package.GetComponent<Rigidbody>().AddForce(Random.Range(5, 25), 0, Random.Range(-5, 6), ForceMode.Impulse);
        else package.GetComponent<Rigidbody>().AddForce(Random.Range(-5, -25), 0, Random.Range(-5, 6), ForceMode.Impulse);
    }

    private IEnumerator ShootDelayCoroutine() { 
        _canShoot = false;
        yield return new WaitForSeconds(_speed);
        _canShoot = true;
        _speed = Random.Range(2f, 6f);
    }
}
