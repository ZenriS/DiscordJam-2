using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyActions_script : MonoBehaviour
{
    public float RotTime;
    private Transform _armPivot;
    private Transform _bulletExit;
    public float BulletForce;
    public GameObject BulletPrefab;
    public int Bullets;
    public float HitChange;
    private RaycastHit2D _raycastHit2D;
    private bool _runOnce;
    public bool AllowShoot;
    public float ReactionTime;
    private bool _doRay;
    private PlayerStates_script _playerStatesScript;
    public int Bounty;
    public GameController_script GameController;
    private List<GameObject> _bullets;
    private SpriteRenderer _headRend;
    private SpriteRenderer _torsoRend;
    private SpriteRenderer _legsRend;
    private SpriteRenderer _armRend;
    private EffectsManager_script _effectsManager;

    
    void Start()
    {
        _playerStatesScript = FindObjectOfType<PlayerStates_script>();
        _armPivot = this.transform.GetChild(1);
        _bulletExit = _armPivot.GetChild(1).GetChild(0);
        _headRend = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
        _torsoRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _legsRend = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        _armRend = _armPivot.GetChild(0).GetComponent<SpriteRenderer>();
        _effectsManager = GameController.GetComponent<EffectsManager_script>();
        _bullets = new List<GameObject>();
        AllowShoot = false;
        RandomPos();
        //Invoke("Aim",ReactionTime);
    }

    void Update()
    {
        if (_doRay)
        {
            Ray();
        }
        if (!_doRay && GameController_script.GameIsRunning)
        {
            Aim();
        }
    }

    public void Config(float a, float r, float s, int b, Sprite hs, Sprite ts, Sprite ls, Color h, Color t, Color l, Sprite arm, int bullets)
    {
        this.gameObject.SetActive(true);
        RotTime = a;
        ReactionTime = r;
        HitChange = s;
        Bounty = b;
        _headRend.sprite = hs;
        _headRend.color = h;
        _torsoRend.sprite = ts;
        _torsoRend.color = t;
        _legsRend.sprite = ls;
        _legsRend.color = l;
        _armRend.sprite = arm;
        _armRend.color = t;
        Bullets = bullets;
        RandomPos();
    }

    void ActivateShooting()
    {
        AllowShoot = true;
    }

    void RandomPos()
    {
        float r = Random.Range(-6.5f, 6.5f);
        Vector2 pos = new Vector2(this.transform.position.x,r);
        this.transform.position = pos;
    }

    void Aim()
    {
        _armPivot.DOPlay();
        _armPivot.DORotate(new Vector3(0, 0, -130), RotTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        _doRay = true;
        Invoke("ActivateShooting",ReactionTime);
    }

    void Ray()
    {
        _raycastHit2D = Physics2D.Raycast(_bulletExit.position, _bulletExit.transform.right);
        if (_raycastHit2D.collider != null)
        {
            if (_raycastHit2D.transform.tag == "Player" && !_runOnce)
            {
                _runOnce = true;
                if (AllowShoot)
                {
                    CalchitPlayer();
                }
            }
        }
        else
        {
            _runOnce = false;
        }
    }

    void CalchitPlayer()
    {
        float r = Random.Range(0, 100);
        float hitmod = HitChange - (HitChange * _playerStatesScript.Luck);
        //Debug.Log("Hit change: " +(HitChange + hitmod) +" random: "+r);
        if (r <= HitChange + hitmod)
        {
            float t = Random.Range(0f, 0.5f);
            Invoke("Shoot",t);
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot");
        if (Bullets > 0)
        {
            _effectsManager.PlayAudioClip(1);
            GameObject b = Instantiate(BulletPrefab, _bulletExit.position, Quaternion.identity);
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            rb.velocity = _bulletExit.right * BulletForce;
            _bullets.Add(b);
            Bullets--;
        }
        else
        {
            _effectsManager.PlayAudioClip(3);
            _doRay = false;
        }
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
        GameController.RoundOver(true);
        //sound sfx and animations stuff
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    public void RoundOver()
    {
        AllowShoot = false;
        foreach (GameObject g in _bullets)
        {
            if (g != null)
            {
                Destroy(g);
            }
        }
        CancelInvoke("Shoot");
        _bullets.Clear();
        _doRay = false;
        _armPivot.DOKill();
        _armPivot.localEulerAngles = new Vector3(0, 0, -35);
    }

}
