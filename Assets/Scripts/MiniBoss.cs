using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniBoss : MonoBehaviour
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
    public GameObject hitBox;
    public float Specialtimer = 10;
    public bool isOnSpecial;
    public int Health = 10;
    public TextMeshProUGUI texto;
    #endregion

    #region private variables
    [HideInInspector] public Animator anim;
    [HideInInspector] public float distance; // b/w enemy and player
    [HideInInspector] public bool attackMode;
    /*[HideInInspector]*/
    public bool cooling; //cooldown after attack
    [HideInInspector] public float intTimer;
    [HideInInspector] public bool isDead;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        texto.SetText((Health/2).ToString());
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
        texto.SetText((Health/2).ToString());

        Specialtimer -= Time.deltaTime;

        if (Specialtimer <= 0 && !attackMode)
        {
            randomSpecialAttack();
            isOnSpecial = true;
        }

        if (isOnSpecial)
        {
            Specialtimer = 10;
        }
    }

    public void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        //Debug.Log("DISTANCIA:"+distance);

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

    public void stopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Hit", false);
    }

    public void move()
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

    public bool InsideofLimits()
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
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }

    public void Death()
    {
        anim.SetTrigger("Damage");
        Health -= 1;

        if (Health <= 0)
        {
            Health = 0;
            Invoke("kill", 1.15f);
            isDead = true;
            anim.SetBool("isDead",true);
        }
    }

    private void kill()
    {
        Destroy(gameObject);
        Destroy(transform.parent.gameObject, 0.48f + 0.51f);
        Destroy(texto.GetComponentInParent<GameObject>());
    }
    void randomSpecialAttack()
    {
        switch (Random.Range(1, 2))
        {
            case 1:
                anim.SetTrigger("Spell");
                break;
            default:
                break;
        }
    }
}
