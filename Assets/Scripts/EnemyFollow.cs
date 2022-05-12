using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    #region public variables
    public float attackDistance; // Minimum distance for attack
    public float moveSpeed;
    public float timer; // colldon between attacks
    public string AttackAnimationName;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector]public Transform target;
    [HideInInspector]public bool inRange; // check if the player is in range 
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region private variables
    private Animator anim;
    private float distance; // b/w enemy and player
    private bool attackMode;
    private bool cooling; //cooldown after attack
    private float intTimer;
    public GameObject player;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackMode)
        {
            move();
        }

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName(AttackAnimationName))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }

        Debug.Log(player.transform.position.x - transform.position.x);

    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            stopAttack();
        }
        else
        {
            if (!cooling)
            {
                Attack();
            }
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Hit", false);
        }
    }

    void stopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Hit", false);
    }

    void move()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(AttackAnimationName))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("Hit", true);
    }

    void TriggerCooling()
    {
        cooling = true;
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
