using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController_script : MonoBehaviour
{
    public float MoveSpeed;
    private Transform _gun;
    private Transform _bulletExit;
    public float ShootCooldown;
    public GameObject BulletPrefab;
    public float BulletForce;
    public float BulletDmg;

    void Start()
    {
        _bulletExit = transform.GetChild(1).GetChild(0);
        //Move();
        InvokeRepeating("Shoot", ShootCooldown/ 2, ShootCooldown);
    }

    void Move()
    {
        Vector2 movePos = new Vector2(transform.position.x + 10f, transform.position.y);
        transform.DOMove(movePos, MoveSpeed).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
    }

    void Shoot()
    {
        GameObject b = Instantiate(BulletPrefab, _bulletExit.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        Bullet_Script bs = b.GetComponent<Bullet_Script>();
        bs.Config(BulletDmg,"Enemy");
        rb.velocity = -_bulletExit.transform.up * BulletForce;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Bullet_Script b = c.GetComponent<Bullet_Script>();
        if (b != null)
        {
            if (b.Owner == "Player")
            {
                Destroy(b.gameObject);
                Death();
            }
        }
    }

    void Death()
    {
        //Add animations and stuff
        Destroy(this.gameObject);
    }
}
