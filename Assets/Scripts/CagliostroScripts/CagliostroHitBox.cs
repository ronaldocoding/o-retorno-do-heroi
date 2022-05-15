using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagliostroHitBox : MonoBehaviour
{

    private GameObject player;
    private bool isAttacked;

    private void Start()
    {
        isAttacked = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isAttacked)
        {
            isAttacked = true;
            player = other.gameObject;
            GameController.instance.UpdatePlayerHealth(-1);
            player.GetComponent<Animator>().SetTrigger("take_hit");
            Debug.Log("Vida player: " + GameController.instance.playerHealth);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAttacked = false;
        }
    }
}