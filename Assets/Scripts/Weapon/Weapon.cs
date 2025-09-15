using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private int damage = 2;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileEffects;

    [Header("Spawn points")]
    [SerializeField] private Transform projectileParent;

    public int Damage
    {
        get { return damage; }
    }

    private Vector2 aimDirection;
    private Quaternion targetRotation;
    private float shooterTimer;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;    
    }

    private void Update()
    {
        UpdateAiming();
        UpdateRotation();
        HandleShooting();
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

    private void HandleShooting()
    {
        shooterTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            Shoot();
            shooterTimer = 0f;
        }
    }

    private bool CanShoot()
    {
        return shooterTimer >= timeBetweenShots;
    }

    public void TryShoot()
    {
        if(CanShoot())
        {
            Shoot();
            shooterTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, 
                                        projectileParent.position, 
                                        targetRotation);

        Projectile projectileDamage = newProjectile.GetComponent<Projectile>();
        if(projectileDamage != null)
        {
            projectileDamage.Initialize(aimDirection, projectileSpeed, projectileLifetime, damage, projectileEffects);
        }
    }
}
