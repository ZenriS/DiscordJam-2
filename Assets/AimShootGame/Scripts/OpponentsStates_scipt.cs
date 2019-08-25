using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpponentsStates_scipt : MonoBehaviour
{
    [SerializeField]
    private float _reaction;
    [SerializeField]
    private float _aimTime;
    [SerializeField]
    private float _skill;
    private Image _reactionFill;
    private Image _aimTimeFilll;
    private Image _skillFill;
    private OpponentsCreator_script _opponentsCreatorScript;
    private Button _selectButton;
    private TextMeshProUGUI _bountyText;
    private int _bounty;
    private Color _headColor;
    private Color _torsoColor;
    private Color _legsColor;
    private Sprite _headSprite;
    private Sprite _torsoSprite;
    private Sprite _legsSprite;
    private Sprite _arm;
    private Image _headImage;
    private Image _torsoImage;
    private Image _armImage;
    private int _bullets;
    private EffectsManager_script _effectsManager;
    private TextMeshProUGUI _bulletText;
    
    
    void Awake()
    {
        _reactionFill = transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>();
        _aimTimeFilll = transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<Image>();
        _skillFill = transform.GetChild(1).GetChild(5).GetChild(1).GetComponent<Image>();
        _bountyText = transform.GetChild(1).GetChild(6).GetComponent<TextMeshProUGUI>();
        _opponentsCreatorScript = transform.parent.parent.GetComponent<OpponentsCreator_script>();
        _headImage = transform.GetChild(0).GetChild(2).GetComponent<Image>();
        _torsoImage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
        _armImage = transform.GetChild(0).GetChild(3).GetComponent<Image>();
        _effectsManager = _opponentsCreatorScript.GameController.GetComponent<EffectsManager_script>();
        _selectButton = transform.GetComponentInChildren<Button>();
        _selectButton.onClick.AddListener(()=> Select());
        _bulletText = transform.GetChild(1).GetChild(7).GetComponent<TextMeshProUGUI>();
    }

    public void Config(float r,float rm, float a, float am, float s ,float sm, Color h, Color t, Color l, Sprite hs, Sprite ts, Sprite ls, Sprite arm, int bullets)
    {
        float tempbounty = 0;
        _reaction = r;
        float temp = r / rm;
        tempbounty += rm - r;
        _reactionFill.fillAmount = temp;
        _aimTime = a;
        temp = a / am;
        tempbounty += am - a;
        _aimTimeFilll.fillAmount = temp;
        _skill = s;
        temp = s / sm;
        tempbounty += s;
        _skillFill.fillAmount = temp;
        _bounty = Mathf.RoundToInt(tempbounty) + 50;
        _headColor = h;
        _torsoColor = t;
        _legsColor = l;
        _headSprite = hs;
        _torsoSprite = ts;
        _legsSprite = ls;
        _arm = arm;
        _headImage.color = h;
        _headImage.sprite = hs;
        _torsoImage.color = t;
        _torsoImage.sprite = _torsoSprite;
        _armImage.sprite = arm;
        _armImage.color = t;
        _bullets = bullets;
        _bulletText.text = "Bullets\n" + _bullets;
        _bounty += _bullets * 5;
        _bountyText.text = "Reward\n" + _bounty;
    }

    public void Select()
    {
        _effectsManager.PlayAudioClip(0);
        _opponentsCreatorScript.EnemyActions.Config(_aimTime,_reaction,_skill,_bounty,_headSprite,_torsoSprite,_legsSprite,_headColor,_torsoColor,_legsColor,_arm, _bullets);
        _opponentsCreatorScript.gameObject.SetActive(false);
        _opponentsCreatorScript.GameController.StartNewRound();
    }
}
