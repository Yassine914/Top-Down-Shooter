using UnityEngine;

public class MoveTowardsPlayer : StateMachineBehaviour
{
    private BossHandler bossHandler;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler = animator.GetComponent<BossHandler>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.MoveToPlayer();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
