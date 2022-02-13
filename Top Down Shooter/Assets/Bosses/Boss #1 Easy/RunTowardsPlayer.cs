using UnityEngine;

public class RunTowardsPlayer : StateMachineBehaviour
{
    [SerializeField] private float moveSpeedAdd;
    
    private BossHandler bossHandler;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler = animator.GetComponent<BossHandler>();
        bossHandler.moveSpeed += moveSpeedAdd;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.MoveToPlayer();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.moveSpeed -= moveSpeedAdd;
    }
}
