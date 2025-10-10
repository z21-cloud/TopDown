using UnityEngine;
using System.Collections.Generic;

public class PatrolBehaviour : StateMachineBehaviour
{
    private List<GameObject> patrolPoints = new List<GameObject>();
    private List<GameObject> currentPatrolRoute = new List<GameObject>();
    int randomPoint;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //GameObject[] objects = GameObject.FindGameObjectsWithTag("PatrolPoints");
        //for(int i = 0; i < objects.Length; i++)
        //{
        //    patrolPoints.Add(objects[i]);
        //}
        //randomPoint = Random.Range(0, patrolPoints.Count);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
