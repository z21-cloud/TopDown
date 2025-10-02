using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private AimProvider aimProvider;
    [SerializeField] private float rotationSpeed = 5f;

    private Quaternion targetRotation;
    private Vector2 aimDirection;

    private void Update()
    {
        aimDirection = aimProvider.GetAimDirection();
        UpdateRotation(aimDirection);
    }

    

    private void UpdateRotation(Vector2 aimDirection)
    {
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, 0, -angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        //transform.rotation = rotation;
    }

    public Quaternion GetTargetRotation() => targetRotation;
}
