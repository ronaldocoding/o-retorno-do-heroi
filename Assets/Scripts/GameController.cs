using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public TextMeshProUGUI texto;
    public int Health;
    public static GameController instance;
    // Start is called before the first frame update

    private void Awake() {
        texto.SetText(Health.ToString());
    }
    void Start()
    {
        instance = this;
    }

    public void UpdateHealth(int amount)
    {
        Health += amount;
        if(Health < 0){
            Health = 0;
        }
        texto.SetText(Health.ToString());
    }
}
