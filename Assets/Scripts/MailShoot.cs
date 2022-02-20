using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailShoot : MonoBehaviour
{
    [SerializeField] GameObject _box;
    private bool _canShoot = true;

    private void Update() => Shoot();

    private void Shoot() {
        if(!_canShoot) return;

        StartCoroutine(ShootDelayCoroutine());
        GameObject package = Instantiate(_box);
        package.transform.position = _box.transform.position;
        package.SetActive(true);
        package.transform.Rotate(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
        package.GetComponent<Rigidbody>().AddForce(Random.Range(10, 25), 0, Random.Range(-5, 6), ForceMode.Impulse);
    }

    private IEnumerator ShootDelayCoroutine() { 
        _canShoot = false;
        yield return new WaitForSeconds(3f);
        _canShoot = true;
    }
}
