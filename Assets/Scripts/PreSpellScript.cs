using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSpellScript : StateMachineBehaviour
{
    private MiniBoss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<MiniBoss>();
        boss.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 targetPosition = new Vector2(boss.target.position.x, boss.transform.position.y);

        boss.transform.position = Vector2.MoveTowards(boss.transform.position, targetPosition, boss.moveSpeed * 4 * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
