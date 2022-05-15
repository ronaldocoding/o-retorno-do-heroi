using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossTriggerArea : MonoBehaviour
{

    private MiniBoss enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<MiniBoss>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            Debug.Log(other.gameObject.transform.position.x);
            gameObject.SetActive(false);
            enemyParent.target = other.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}