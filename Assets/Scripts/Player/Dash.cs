using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashSpeed = 20f;
    private bool isDashing = false;
    private float lastDashTIme = 0;
    private Vector3 direction;
    void Update()
    {
        GetNormalizedVector(InputManager.Instance.moveInputX, InputManager.Instance.moveInputY);

        if (InputManager.Instance.DashPressed && direction != Vector3.zero && Time.time - lastDashTIme > dashCooldown)
        {
            isDashing = true;
            lastDashTIme = Time.time;
            StartCoroutine(DashPlayer());
        }
    }

    private Vector2 GetNormalizedVector(float x, float y)
    {
        Vector2 input = new Vector2(x, y).normalized;
        if (input != Vector2.zero)
        {
            direction = input;
            return direction;
        }

        return Vector2.zero;
    }

    private IEnumerator DashPlayer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
    }
}
