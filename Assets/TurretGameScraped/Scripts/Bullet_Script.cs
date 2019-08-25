using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    [HideInInspector] public float Damage;
    public float LifeTime;
    public string Owner;

    void Start()
    {
        Invoke("LifeTimer",LifeTime);
    }

    void LifeTimer()
    {
        Destroy(this.gameObject);
    }

    public void Config(float d, string o)
    {
        Damage = d;
        Owner = o;
    }
}
