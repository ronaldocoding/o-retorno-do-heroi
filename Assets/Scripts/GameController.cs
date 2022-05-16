using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI cagliostroHealthText = null;
    public int playerHealth;
    public int cagliostroHealth;
    public static GameController instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (playerHealth < 10)
        {
            playerHealthText.SetText("0" + playerHealth.ToString());
        }
        else
        {
            playerHealthText.SetText(playerHealth.ToString());
        }
        if (cagliostroHealthText != null)
        {
            if (cagliostroHealth < 10)
            {
                cagliostroHealthText.SetText("0" + cagliostroHealth.ToString());
            }
            else
            {
                cagliostroHealthText.SetText(cagliostroHealth.ToString());
            }
        }
    }
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (cagliostroHealthText != null)
        {
            if (cagliostroHealth <= 0)
            {
                Invoke("WinGame", 3f);
            }
        }
    }

    public void UpdatePlayerHealth(int amount)
    {
        playerHealth += amount;
        if (playerHealth < 0)
        {
            playerHealth = 0;
        }
        if (playerHealth < 10)
        {
            playerHealthText.SetText("0" + playerHealth.ToString());
        }
        else
        {
            playerHealthText.SetText(playerHealth.ToString());
        }
    }

    public void UpdateCagliostroHealth(int amount)
    {
        cagliostroHealth += amount;
        if (cagliostroHealth < 0)
        {
            cagliostroHealth = 0;
        }
        if (cagliostroHealth < 10)
        {
            cagliostroHealthText.SetText("0" + cagliostroHealth.ToString());
        }
        else
        {
            cagliostroHealthText.SetText(cagliostroHealth.ToString());
        }
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGameApplication()
    {
        Application.Quit();
    }

    public void ExitPlayMode()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void WinGame()
    {
        SceneManager.LoadScene("win_game");
    }
}
