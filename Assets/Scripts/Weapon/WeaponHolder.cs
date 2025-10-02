using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform weaponSpot;
    private Weapon currentWeapon;
    private WeaponRotator currentWeaponRotator;
    private WeaponInput currentWeaponInput;

    private void Start()
    {
        if (weaponSpot.childCount > 0)
        {
            currentWeapon = weaponSpot.GetComponentInChildren<Weapon>();
            currentWeaponRotator = weaponSpot.GetComponentInChildren<WeaponRotator>();
            currentWeaponInput = weaponSpot.GetComponentInChildren<WeaponInput>();
        }
    }

    public void EqiupWeapon(WeaponPickup pickup)
    {
        if (currentWeapon != null) Destroy(currentWeapon.gameObject);

        Weapon newWeapon = Instantiate(pickup.GetWeaponPrefab(), weaponSpot.position, weaponSpot.rotation, weaponSpot);
        currentWeapon = newWeapon;
        currentWeaponRotator = newWeapon.gameObject.GetComponent<WeaponRotator>();
        currentWeaponInput = newWeapon.gameObject.GetComponent<WeaponInput>();
        
        currentWeapon.enabled = true;
        currentWeaponRotator.enabled = true;
        currentWeaponInput.enabled = true;
        Destroy(pickup.gameObject);
    }
}
