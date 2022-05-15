using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{

    private EnemyFollow enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyFollow>();
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName(enemyParent.AttackAnimationName))
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
            inRange = false;

            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;

            enemyParent.SelectTarget();
        }
    }
}
