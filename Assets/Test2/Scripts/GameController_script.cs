using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController_script : MonoBehaviour
{
    private UIMananger_script _uiManangerScript;
    public static bool GameIsRunning;
    public PlayerStates_script PlayerStatesScrips;
    private PlayerControlls_script _playerControlls;
    public EnemyActions_script EnemyActions;
    private int _rounds;
    private bool _countindown;
    
    void Start()
    {
        _playerControlls = PlayerStatesScrips.GetComponent<PlayerControlls_script>();
        _uiManangerScript = GetComponent<UIMananger_script>();
        _uiManangerScript.SetupStartScreen(PlayerStatesScrips.Cash);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !GameIsRunning && _uiManangerScript.GameUI.activeInHierarchy && !_countindown)
        {
            StartRound();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R)) //For debug and test
        {
            RestartGame();
        }
#endif
    }

    public void StartRound()
    {
        _countindown = true;
        StartCoroutine("RoundCountdown");
    }

    public void StartNewRound()
    {
        Time.timeScale = 1;
        _uiManangerScript.ToggleGameScreen();
        _playerControlls.SetRandomPos();
        _countindown = false;
    }

    IEnumerator RoundCountdown()
    {
        int t = 5;
        _uiManangerScript.SetCountdownText(t.ToString("F0"));
        while (t > 0)
        {
            yield return new WaitForSeconds(1);
            //count down sound
            t--;
            _uiManangerScript.SetCountdownText(t.ToString("F0"));
        }
        _uiManangerScript.SetCountdownText("Draw!");
        GameIsRunning = true;
        yield return new WaitForSeconds(0.5f);
        _uiManangerScript.SetCountdownText("");
    }

    public void RestartGame()
    {
        GameIsRunning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RoundOver(bool w)
    {
        Time.timeScale = 0;
        GameIsRunning = false;
        EnemyActions.RoundOver();
        _playerControlls.RoundOver();
        if (w)
        {
            PlayerStatesScrips.ModifyCash(EnemyActions.Bounty);
            _rounds++;
        }
        _uiManangerScript.ToggleRoundOverScreen(w, _rounds);
    }

}
