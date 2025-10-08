using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashDuration = .05f;
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashSpeed = 100f;    
    public bool IsDashing { get; private set; }

    private float lastDashTime = 0;
    private Vector2 direction;
    void Update()
    {
        GetNormalizedVector(InputManager.Instance.moveInputX, InputManager.Instance.moveInputY);

        if (InputManager.Instance.DashPressed 
            && direction != Vector2.zero 
            && Time.time - lastDashTime > dashCooldown)
        {
            StartCoroutine(DashPlayer());
        }
    }

    private Vector2 GetNormalizedVector(float x, float y)
    {
        Vector2 input = new Vector2(x, y).normalized;

        if (input != Vector2.zero)
            direction = input;
        else
            direction = Vector2.zero;

        return direction;
    }

    private IEnumerator DashPlayer()
    {
        IsDashing = true;
        lastDashTime = Time.time;
        Vector3 dashDir = direction;
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            float speed = Mathf.Lerp(dashSpeed, 0, t);
            //float speed = dashSpeed * Mathf.Pow(1-t, 2f);
            transform.position += dashDir * speed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        IsDashing = false;
    }
}
