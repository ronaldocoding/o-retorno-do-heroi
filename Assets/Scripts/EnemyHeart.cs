using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeart : MonoBehaviour
{

    public GameObject collected;
    private SpriteRenderer sr;
    private BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            sr.enabled = false;
            box.enabled = false;

            collected.SetActive(true);

            GameController.instance.UpdateHealth(1);

            Destroy(transform.parent.gameObject, 0.5f);
        }
    }
}
