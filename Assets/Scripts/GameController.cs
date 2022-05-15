using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI cagliostroHealthText;
    public int playerHealth;
    public int cagliostroHealth;
    public static GameController instance;
    // Start is called before the first frame update

    private void Awake() {
        playerHealthText.SetText((playerHealth/2).ToString());
        cagliostroHealthText.SetText((cagliostroHealth/2).ToString());
    }
    void Start()
    {
        instance = this;
    }

    public void UpdatePlayerHealth(int amount)
    {
        playerHealth += amount;
        if(playerHealth < 0){
            playerHealth = 0;
        }
        playerHealthText.SetText(playerHealth.ToString());
    }

    public void UpdateCagliostroHealth(int amount)
    {
        cagliostroHealth += amount;
        if(cagliostroHealth < 0){
            cagliostroHealth = 0;
        }
        cagliostroHealthText.SetText(cagliostroHealth.ToString());
    }
}
