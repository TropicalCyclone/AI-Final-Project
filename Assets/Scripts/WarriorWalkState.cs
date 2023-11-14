using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorWalkState : StateMachineBehaviour
{
    NewAiBehaviour aiBehaviour;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiBehaviour = animator.GetComponentInParent<NewAiBehaviour>();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NewAiBehaviour nearestGameObject = FindClosestEnemy();
        if (nearestGameObject != null && !nearestGameObject.IsDead)
        {
            aiBehaviour.agent.SetDestination(nearestGameObject.transform.position);
            // Check if the GameObject is within the radius
            float distance = Vector3.Distance(aiBehaviour.agent.transform.position, nearestGameObject.transform.position);
            if (distance <= aiBehaviour.range)
            {
                aiBehaviour.agent.speed = 0;
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
            }
        }
    }

    NewAiBehaviour FindClosestEnemy()
    {
        NewAiBehaviour closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (NewAiBehaviour enemy in aiBehaviour.Enemies)
        {
            float distanceToEnemy = Vector3.Distance(aiBehaviour.transform.position, enemy.transform.position);

            if (distanceToEnemy < closestDistance && enemy.gameObject.tag == aiBehaviour.tag && enemy.isActiveAndEnabled)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
