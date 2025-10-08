using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Patrol : MonoBehaviour
{
    [SerializeField] private GameObject[] patrols;
    private List<GameObject> currentPatrolRoute;
    private int randomPoint;
    private int currentIndex;
    void Start()
    {
        patrols = GameObject.FindGameObjectsWithTag("PatrolPoint");
        currentPatrolRoute = new List<GameObject>(patrols);
        StartCoroutine(PatrolRoute());
    }

    private IEnumerator PatrolRoute()
    {
        while (true)
        {
            if (currentPatrolRoute.Count == 0)
            {
                currentPatrolRoute = new List<GameObject>(patrols);
            }

            randomPoint = Random.Range(0, currentPatrolRoute.Count);
            while (Vector2.Distance(transform.position, currentPatrolRoute[randomPoint].transform.position) > .1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPatrolRoute[randomPoint].transform.position, 10f * Time.deltaTime);
                yield return null;
            }
            currentPatrolRoute.RemoveAt(randomPoint);
            yield return new WaitForSeconds(1f);
        }
    }
}
