using UnityEngine;

public class RunAround : StateMachineBehaviour
{
    [SerializeField] private float moveSpeedAdd;
    
    private BossHandler bossHandler;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler = animator.GetComponent<BossHandler>();
        bossHandler.moveSpeed += moveSpeedAdd;
        bossHandler.StartMoveAround();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.MoveAround();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.moveSpeed -= moveSpeedAdd;
        bossHandler.StopMoveAround();
    }
}
