using UnityEngine;
using System.Collections;

public class groundedFX : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		GameObject endJumpDust = animator.gameObject.GetComponentInParent<Player> ().endJumpDust;
		Vector3 newScale = animator.transform.parent.transform.localScale;
		newScale.x = -newScale.x;
		endJumpDust.transform.localScale = newScale;
		Vector3 newPos = animator.transform.position;
		newPos.y -= 10;
		newPos.x += -10 * newScale.x;
		endJumpDust.transform.position = newPos;
		endJumpDust.SetActive (true);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
