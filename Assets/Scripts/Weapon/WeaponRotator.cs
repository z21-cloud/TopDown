using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private Vector2 aimDirection;
    private Quaternion targetRotation;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAiming();
        UpdateRotation();
    }

    private void UpdateAiming()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        aimDirection = (mouseWorldPos - transform.position).normalized;
    }

    private void UpdateRotation()
    {
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, 0, -angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        //transform.rotation = rotation;
    }

    public Vector2 GetAimDirection() => aimDirection;
    public Quaternion GetTargetRotation() => targetRotation;
}
