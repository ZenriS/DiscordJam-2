using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerControlls_script : MonoBehaviour
{
    private Transform _armPivot;
    private bool _rot;
    public float BulletForce;
    public GameObject BulletPrefab;
    private Transform _bulletExit;
    private PlayerStates_script _playerStatesScript;
    public GameController_script GameController;
    private List<GameObject> _bullets;
    private EffectsManager_script _effectsManager;

    void Start()
    {
        _playerStatesScript = GetComponent<PlayerStates_script>();
        _armPivot = transform.GetChild(1);
        _bulletExit = _armPivot.GetChild(1).GetChild(0);
        _effectsManager = GameController.GetComponent<EffectsManager_script>();
        _bullets = new List<GameObject>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Jump") && GameController_script.GameIsRunning)
        {
            if (!_rot)
            {
                RotAim();
            }
            else
            {
                Shoot();
            }
        }
    }

    public void SetRandomPos()
    {
        float r = Random.Range(-6.5f, 6.5f);
        Vector2 pos = new Vector2(this.transform.position.x, r);
        this.transform.position = pos;
    }

    void RotAim()
    {
        _armPivot.DOPlay();
        //float s = Random.Range(RotTime/2, RotTime);
        _armPivot.DORotate(new Vector3(0, 0, 125), _playerStatesScript.AimTime).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        _rot = true;
    }

    void Shoot()
    {
        if (_playerStatesScript.Bullets > 0)
        {
            _effectsManager.PlayAudioClip(1);
            GameObject b = Instantiate(BulletPrefab, _bulletExit.position, Quaternion.identity);
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            rb.velocity = _bulletExit.transform.right * BulletForce;
            _playerStatesScript.ModifyBullets(-1);
            _bullets.Add(b);
            //_armPivot.DOPause();
            Invoke("ActivateEnemy",0.5f);
        }
        else
        {
            _effectsManager.PlayAudioClip(3);
            if (GameController.EnemyActions.Bullets <= 0)
            {
                GameController.RoundOver(true,true);
            }
            //click sound
            //Game Over?
        }
    }

    void ActivateEnemy()
    {
        GameController.EnemyActions.AllowShoot = true;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Bullet")
        {
            Destroy(c.gameObject);
            Death();
        }
    }

    void Death()
    {
        _effectsManager.PlayAudioClip(2);
        GameController.RoundOver(false);
        //sound sfx and animations stuff
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void RoundOver()
    {
        foreach (GameObject g in _bullets)
        {
            if (g != null)
            {
                Destroy(g);
            }
        }
        _bullets.Clear();
        _armPivot.DOKill();
        _rot = false;
        _armPivot.localEulerAngles = new Vector3(0,0,25);
    }
}
