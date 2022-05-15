using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float Atacking;
    public float JumpForce;
    private Rigidbody2D rig;
    private bool isJumping;
    private Animator anim;
    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerEnemy;
    float timeNextAttack;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isAlive", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.Health <= 0) 
        {
            anim.SetBool("isAlive", false);
            rig.Sleep();
        } 
        else
        {
            Move();
            Jump();
            Attack();
        }
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

        if(movement > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        else if(movement < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        else
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
        }
    }

    void Attack()
    {
        if(timeNextAttack <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("atack");
                timeNextAttack = 0.2f;
            }
        } else {
            timeNextAttack -= Time.deltaTime;
        }
    }

    void PlayerAttack()
    {
        Collider2D[] enemiesAttack = Physics2D.OverlapCircleAll(attackCheck.position, radiusAttack, layerEnemy);
        for(int i = 0; i < enemiesAttack.Length; i++)
        {
            enemiesAttack[i].SendMessage("Death");
            Debug.Log(enemiesAttack[i].name);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            isJumping = true;
        }
    }

    /*void OnTriggerEnter2D(Collider2D collider)
    {
        int enemyLayer = 6;
        if(collider.gameObject.layer == enemyLayer)
        {
            collider.gameObject.GetComponent<EnemyFollow>().Death();
        }
    }*/
}
