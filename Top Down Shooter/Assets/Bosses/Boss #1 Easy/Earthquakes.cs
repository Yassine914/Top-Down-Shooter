using UnityEngine;

public class Earthquakes : StateMachineBehaviour
{
    private BossHandler bossHandler;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler = animator.GetComponent<BossHandler>();
        bossHandler.StartEarthquakes();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Earthquakes Are Active");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossHandler.StopEarthquakes();
    }
}
