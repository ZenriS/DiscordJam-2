using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControls_script : MonoBehaviour
{
    private Transform _pivot;
    private Transform _bulletExit;
    public GameObject BulletPrefab;
    public float BulletForce;
    public float BulletDmg;
    private Vector2 _mousePos;

    void Start()
    {
        _pivot = transform.GetChild(1);
        _bulletExit = _pivot.GetChild(0).GetChild(0);
    }

    void Update()
    {
        Aim();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Aim()
    {
        _mousePos = Input.mousePosition;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(_mousePos);
        Vector2 direction = new Vector2(screenPos.x - _pivot.position.x, screenPos.y - _pivot.position.y);
        _pivot.transform.up = direction;
    }

    void Shoot()
    {
        GameObject b = Instantiate(BulletPrefab, _bulletExit.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        Bullet_Script bs = b.GetComponent<Bullet_Script>();
        bs.Config(BulletDmg,"Player");
        rb.velocity = _pivot.transform.up * BulletForce;
    }
}
