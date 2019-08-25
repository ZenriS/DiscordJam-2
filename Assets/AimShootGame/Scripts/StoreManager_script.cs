using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager_script : MonoBehaviour
{
    public GameObject StoreScreen;
    public int[] Prices;
    private TextMeshProUGUI[] _buttonTexts;
    private TextMeshProUGUI[] _statesTexts;
    public PlayerStates_script PlayerStatesScript;
    private Button[] _buttons;
    private TextMeshProUGUI _cashText;

    void Start()
    {
        _buttonTexts = StoreScreen.transform.GetChild(2).GetComponentsInChildren<TextMeshProUGUI>();
        _statesTexts = StoreScreen.transform.GetChild(3).GetComponentsInChildren<TextMeshProUGUI>();
        _buttons = StoreScreen.transform.GetChild(2).GetComponentsInChildren<Button>();
        _cashText = StoreScreen.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        CheckButtons();
        UpdateText();
        SetupButtons();
    }

    void SetupButtons()
    {
        _buttons[0].onClick.AddListener(() => BuyAimSpeed(-0.1f));
        _buttons[1].onClick.AddListener(() => BuyLuck());
        _buttons[3].onClick.AddListener(() => BuyBullet());
        _buttons[2].onClick.AddListener(() => BuyAimSpeed(0.1f));
        _buttons[2].gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        CheckButtons();
        UpdateText();
    }

    public void UpdateText()
    {
        _buttonTexts[0].text = "Aim\nTime\n--\n$" + Prices[0];
        _buttonTexts[1].text = "Luck\n++\n$" + Prices[1];
        _buttonTexts[2].text = "Aim\nTime\n++\n$" + Prices[0];
        _buttonTexts[3].text = "Bullets\n++\n$" + Prices[2];
        _statesTexts[0].text = "Aim Time\n" + PlayerStatesScript.AimTime.ToString("F2") +" sec";
        float l = (PlayerStatesScript.Luck - 1) * 100;
        _statesTexts[1].text = "Luck\n" + Mathf.RoundToInt(l) +"%";
        _statesTexts[2].text = "Bullets\n" + PlayerStatesScript.Bullets +"/" +PlayerStatesScript.MaxBullets;
        _cashText.text = "Cash\n" + PlayerStatesScript.Cash;
    }

    public void BuyAimSpeed(float s)
    {
        if (PlayerStatesScript.Cash >= Prices[0] && PlayerStatesScript.AimTime > PlayerStatesScript.MinAimTime)
        {
            PlayerStatesScript.ModifyAimSpeed(s);
            PlayerStatesScript.ModifyCash(-Prices[0]);
            Prices[0] += 25;
            CheckButtons();
        }
    }

    public void BuyLuck()
    {
        if (PlayerStatesScript.Cash >= Prices[1] && PlayerStatesScript.Luck < PlayerStatesScript.MaxLuck)
        {
            PlayerStatesScript.ModifyLuck(0.10f);
            PlayerStatesScript.ModifyCash(-Prices[1]);
            Prices[1] += 100;
            CheckButtons();
        }
    }

    public void BuyBullet()
    {
        if (PlayerStatesScript.Cash >= Prices[2])
        {
            PlayerStatesScript.ModifyBullets(1);
            PlayerStatesScript.ModifyCash(-Prices[2]);
            //Prices[2] += 5;
            CheckButtons();
        }
    }

    void CheckButtons()
    {
        int c = PlayerStatesScript.Cash;
        if (Prices[0] > c || PlayerStatesScript.AimTime <= PlayerStatesScript.MinAimTime)
        {
            _buttons[0].interactable = false;
            _buttons[2].interactable = false;
        }
        else
        {
            _buttons[0].interactable = true;
            _buttons[2].interactable = true;
        }
        if (Prices[1] > c || PlayerStatesScript.Luck >= PlayerStatesScript.MaxLuck)
        {
            _buttons[1].interactable = false;
        }
        else
        {
            _buttons[1].interactable = true;
        }
        if (Prices[2] > c || PlayerStatesScript.Bullets >= PlayerStatesScript.MaxBullets)
        {
            _buttons[3].interactable = false;
        }
        else
        {
            _buttons[3].interactable = true;
        }
        UpdateText();
    }
}
