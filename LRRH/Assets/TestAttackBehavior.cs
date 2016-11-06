using UnityEngine;
using System.Collections;

public class TestAttackBehavior : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Enter Anim");
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("End Anim" + stateInfo.ToString());
        Player p = GameObject.Find("Player").GetComponent<Player>();
        //p.GetComponentInChildren<Animator>().SetBool("Attack", false);
        //p.ComboCheck();
        //p.Invoke("ComboCheck", 0.5f);
        //p.ResetAttackTrippleAnim();
        p.GetComponentInChildren<Animator>().SetBool("Attack", false);
        p.m_AttackCount--;
        if(p.m_AttackCount <= 0)
            p.m_Attacking = false;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
