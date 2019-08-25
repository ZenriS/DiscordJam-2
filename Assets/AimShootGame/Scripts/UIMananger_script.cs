using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIMananger_script : MonoBehaviour
{
    public GameObject GameUI;
    private TextMeshProUGUI _countDownText;
    private TextMeshProUGUI _startRoundText;
    public GameObject RoundOverScreen;
    private TextMeshProUGUI _roundOverTitle;
    private TextMeshProUGUI _roundOverCashText;
    private GameObject _roundOverGrid;
    private StoreManager_script _storeManagerScript;
    private GameController_script _gameController;
    
    void Start()
    {
        _countDownText = GameUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _roundOverGrid = RoundOverScreen.transform.GetChild(3).gameObject;
        _roundOverTitle = RoundOverScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _roundOverCashText = RoundOverScreen.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _storeManagerScript = GetComponent<StoreManager_script>();
        _gameController = GetComponent<GameController_script>();
    }

    public void SetCountdownText(string s)
    {
        _countDownText.transform.DOPunchScale(new Vector3(2, 2, 2), 0.5f,2,0);
        _countDownText.text = s;
    }

    public void ToggleRoundOverScreen(bool w, int r, string s)
    {
        RoundOverScreen.SetActive(true);
        if (w)
        {
            _roundOverTitle.text = s;
            _roundOverCashText.text = "Current Cash\n " + _gameController.PlayerStatesScrips.Cash;
            _storeManagerScript.UpdateUI();
            _roundOverGrid.transform.GetChild(0).gameObject.SetActive(false);
            _roundOverGrid.transform.GetChild(1).gameObject.SetActive(true);
            _roundOverGrid.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            _roundOverTitle.text = s;
            _roundOverCashText.text = "You survived " + r + " rounds.\nWant to try again?";
            _roundOverGrid.transform.GetChild(0).gameObject.SetActive(true);
            _roundOverGrid.transform.GetChild(1).gameObject.SetActive(false);
            _roundOverGrid.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void SetupStartScreen(int c)
    {
        _roundOverTitle.text = "Jetpack Duels";
        _roundOverCashText.text = "Current cash\n" + c;
        RoundOverScreen.SetActive(true);
    }

    public void ToggleGameScreen()
    {
        _countDownText.transform.localScale = new Vector3(1,1,1);
        _countDownText.text = "Press\n 'Space'";
        GameUI.SetActive(true);
    }

    public void UpdateRoundOverCashText()
    {
        _roundOverCashText.text = "Current Cash\n " + _gameController.PlayerStatesScrips.Cash;
    }
}
