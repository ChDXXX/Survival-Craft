using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterRun : StateMachineBehaviour
{  
    public float attackRange = 3f;
    public float damage = 0.05f; 
    private Transform player;
    private Rigidbody rigidbody;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.GetComponent<Rigidbody>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        if(Vector3.Distance(player.position, rigidbody.position) <= attackRange)
        { 
            animator.SetTrigger("TriggerAttack");
        } 
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.PlayerDamage(damage);
        animator.ResetTrigger("TriggerAttack");
    }
}
