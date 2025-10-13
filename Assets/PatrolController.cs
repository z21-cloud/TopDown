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
    
    public bool CanPatrol { get; private set; }
    public System.Action<bool> OnMoveStateChanged;

    private List<Transform> currentPatrolRoute;
    private int randomPoint;
    private Coroutine patrolRoutine;

    private void OnEnable()
    {
        if(patrolParent == null)
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
        while (CanPatrol)
        {
            if (currentPatrolRoute.Count == 0)
            {
                currentPatrolRoute = new List<Transform>(patrols);
            }
            randomPoint = Random.Range(0, currentPatrolRoute.Count);

            OnMoveStateChanged?.Invoke(true);
            while (Vector2.Distance(transform.position, currentPatrolRoute[randomPoint].position) > distanceTreshold)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPatrolRoute[randomPoint].position, speed * Time.deltaTime);
                yield return null;
            }

            OnMoveStateChanged?.Invoke(false);
            currentPatrolRoute.RemoveAt(randomPoint);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void StartPatrol()
    {
        if (patrolRoutine != null) StopCoroutine(patrolRoutine);
        CanPatrol = true;
        patrolRoutine = StartCoroutine(PatrolRoute());
    }

    public void StopPatrol()
    {
        CanPatrol = false;
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
        CanPatrol = false;
    }
}
