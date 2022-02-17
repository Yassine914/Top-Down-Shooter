using UnityEngine;

public class RunTowardsPlayer : StateMachineBehaviour
{
    private BossHandler bossHandler;
    private float oldSpeed;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler = animator.GetComponent<BossHandler>();

        oldSpeed = bossHandler.moveSpeed;
        bossHandler.moveSpeed = bossHandler.runSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.MoveToPlayer();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.moveSpeed = oldSpeed;
    }
}
