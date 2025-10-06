using Unity.VisualScripting;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField] private float xSpeed = 4f;
    [SerializeField] private float ySpeed = 4f;
    [SerializeField] private float g = 15f;
    private float t;
    private Vector2 startPosition;
    private Vector2 currentPosition;
    private bool isMoving = true;
    
    private void Start()
    {
        startPosition = transform.position;
        currentPosition = startPosition;

        float minAngle = 30f;
        float maxAngle = 60f;
        float minForce = 4f;
        float maxForce = 6f;

        float randomAngle = Random.Range(minAngle, maxAngle);
        float randomForce = Random.Range(minForce, maxForce);
        float randomDirection = Random.value < 0.5f ? -1f : 1f;

        float angleRad = randomAngle * Mathf.Deg2Rad;

        xSpeed = Mathf.Cos(angleRad) * randomForce * randomDirection;
        ySpeed = Mathf.Sin(angleRad) * randomForce;
    }

    private void Update()
    {
        if (!isMoving) return;

        t += Time.deltaTime;
        currentPosition.x = startPosition.x + xSpeed * t;
        currentPosition.y = startPosition.y + ySpeed * t - .5f * g * t * t;
        transform.position = currentPosition;

        if (currentPosition.y <= startPosition.y)
        {
            transform.position = new Vector2(currentPosition.x, startPosition.y);
            isMoving = false;
            Destroy(this);
        }
    }

    /*[SerializeField] private float force = .5f;
    [SerializeField] private float radius = 1f;

    public void Start()
    {
        // Выбираем случайное направление в радиусе
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)transform.position;

        // Добавляем Rigidbody2D для “подброса”
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 3f;

        // Задаём импульс (направление + вверх)
        Vector2 launchDir = randomDir + Vector2.up * 0.5f;
        rb.AddForce(launchDir.normalized * force, ForceMode2D.Impulse);

        // Через 1 сек убираем физику (предмет “ложится”)
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(RemovePhysicsAfterDelay(rb));
    }

    private System.Collections.IEnumerator RemovePhysicsAfterDelay(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(1f);
        if (rb != null) Destroy(rb);
        Destroy(this);
    }*/
}
