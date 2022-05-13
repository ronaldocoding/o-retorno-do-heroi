using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHitBox : MonoBehaviour
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
            player.GetComponent<Player>().Health -= 1;
            player.GetComponent<Animator>().SetTrigger("take_hit");
            Debug.Log("Vida player: " + player.GetComponent<Player>().Health);
            
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