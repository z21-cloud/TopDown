using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private int damage = 2;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectileEffects;

    [Header("Spawn points")]
    [SerializeField] private Transform projectileParent;

    [Header("Weapon Rotator")]
    [SerializeField] private WeaponRotator weaponRotator;
    [SerializeField] private AimProvider aimProvider;
    public int Damage
    {
        get { return damage; }
    }

    private float shooterTimer;

    private void Update()
    {
        HandleTimer();
    }


    private void HandleTimer()
    {
        shooterTimer += Time.deltaTime;
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
                                        weaponRotator.GetTargetRotation());

        Projectile projectileDamage = newProjectile.GetComponent<Projectile>();
        if(projectileDamage != null)
        {
            projectileDamage.Initialize(aimProvider.GetAimDirection(), projectileSpeed, projectileLifetime, damage, projectileEffects);
        }
    }
}
