using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class PatrolController : MonoBehaviour
{
    [SerializeField] private Transform[] patrols;
    [SerializeField] private GameObject patrolParent;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private float distanceTreshold = .1f;
    
    public bool IsMoving { get; private set; }
    public System.Action<bool> OnMoveStateChanged;

    private List<Transform> currentPatrolRoute;
    private int randomPoint;
    private Coroutine patrolRoutine;
    private IMovementStrategy movementStrategy = new ChasePlayerMovement();

    private void OnEnable()
    {
        InitializePatrolPoints();
    }

    private void InitializePatrolPoints()
    {
        if (patrolParent == null)
            patrolParent = GameObject.FindGameObjectWithTag("PatrolPoint");

        if (patrolParent != null)
        {
            patrols = patrolParent.GetComponentsInChildren<Transform>()
                .Where(t => t != transform).ToArray();
            currentPatrolRoute = new List<Transform>(patrols);
        }
    }

    private IEnumerator PatrolRoute()
    {
        while (true)
        {
            if (currentPatrolRoute.Count == 0)
            {
                currentPatrolRoute = new List<Transform>(patrols);
            }

            Transform target = GetNextPatrolPoint();
            yield return MoveToTarget(target);

            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        OnMoveStateChanged?.Invoke(true);
        IsMoving = true;
        while (Vector2.Distance(transform.position, target.position) > distanceTreshold)
        {
            movementStrategy.Move(transform, target, speed);
            yield return null;
        }

        OnMoveStateChanged?.Invoke(false);
        IsMoving = false;
    }

    private Transform GetNextPatrolPoint()
    {
        randomPoint = Random.Range(0, currentPatrolRoute.Count);
        Transform target = currentPatrolRoute[randomPoint];
        currentPatrolRoute.RemoveAt(randomPoint);
        return target;
    }

    public void StartPatrol()
    {
        if (patrolRoutine != null) StopCoroutine(patrolRoutine);
        patrolRoutine = StartCoroutine(PatrolRoute());
    }

    public void StopPatrol()
    {
        if (patrolRoutine != null)
        {
            StopCoroutine(patrolRoutine);
            patrolRoutine = null;
        }
        OnMoveStateChanged?.Invoke(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
