using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CagliostroFollow : MonoBehaviour
{
    #region public variables
    public float attackDistance; // Minimum distance for attack
    public float moveSpeed;
    public float timer; // colldon between attacks
    public string AttackAnimationName;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; // check if the player is in range 
    public GameObject hotZone;
    public GameObject triggerArea;
    public GameObject player;
    #endregion

    #region private variables
    private Animator anim;
    private float distance; // b/w enemy and player
    private bool attackMode;
    private bool cooling; //cooldown after attack
    private float intTimer;
    private bool isDead;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isAlive", true);
    }

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackMode && !isDead)
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

        teleportByDistance();
    }

    private void teleportByDistance() {
        if(transform.position.x - player.transform.position.x >= 30.0) {
            transform.position = new Vector2(player.transform.position.x-5, player.transform.position.y+10);
        }
        else if(player.transform.position.x - transform.position.x >= 30.0)
        {
            transform.position = new Vector2(player.transform.position.x+5, player.transform.position.y+10);
        }
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
            anim.SetBool("attack", false);
        }
    }

    void stopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("attack", false);
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

        anim.SetBool("attack", true);
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

    public void Death()
    {
        GameController.instance.UpdateCagliostroHealth(-1);
        if(GameController.instance.cagliostroHealth <= 0) {
            isDead = true;
            GetComponentInChildren<CagliostroHitBox>().gameObject.SetActive(false);
            anim.SetBool("isAlive", false);
            anim.SetTrigger("take_hit");
            Invoke("kill", 1.94f);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
        anim.SetTrigger("take_hit");
    }

    private void kill()
    {
        Destroy(transform.parent.gameObject);
    }
}