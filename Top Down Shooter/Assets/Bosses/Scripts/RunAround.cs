using UnityEngine;

public class RunAround : StateMachineBehaviour
{
    private BossHandler bossHandler;
    private float oldSpeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler = animator.GetComponent<BossHandler>();
        
        oldSpeed = bossHandler.moveSpeed;
        bossHandler.moveSpeed = bossHandler.runSpeed;
        bossHandler.StartMoveAround();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.MoveAround();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.moveSpeed = oldSpeed;
        bossHandler.StopMoveAround();
    }
}
