using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHealthController_script : MonoBehaviour
{
    public float Starthealth;
    private float _activeHealth;
    private Image _healthBar;

    void Start()
    {
        _healthBar = transform.GetChild(2).GetChild(1).GetComponent<Image>();
        _activeHealth = Starthealth;
        TakeDamage(0);
    }

    public void TakeDamage(float d)
    {
        Debug.Log("Take Dmg");
        _activeHealth -= d;
        float b = _activeHealth / Starthealth;
        _healthBar.fillAmount = b;
        if (_activeHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        //animation or somthing
        Destroy(this.gameObject);
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Player hit");
        Bullet_Script b = c.transform.GetComponent<Bullet_Script>();
        if (b != null)
        {
            if (b.Owner == "Enemy")
            {
                Debug.Log("Player hit by bullet");
                TakeDamage(b.Damage);
                Destroy(b.gameObject);
            }
        }
    }
}
