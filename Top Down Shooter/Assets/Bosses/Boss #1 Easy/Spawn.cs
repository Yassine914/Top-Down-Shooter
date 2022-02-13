using UnityEngine;

public class Spawn : StateMachineBehaviour
{
    private BossHandler bossHandler;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler = animator.GetComponent<BossHandler>();
        bossHandler.StartSpawning();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.StopSpawning();
    }
}
