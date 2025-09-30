using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Transform weaponSpot;
    private Weapon currentWeapon;

    private void Start()
    {
        if (weaponSpot.childCount > 0)
            currentWeapon = weaponSpot.GetComponentInChildren<Weapon>();
    }

    public void EqiupWeapon(WeaponPickup pickup)
    {
        if (currentWeapon != null) Destroy(currentWeapon.gameObject);

        Weapon newWeapon = Instantiate(pickup.GetWeaponPrefab(), weaponSpot.position, weaponSpot.rotation, weaponSpot);
        currentWeapon = newWeapon;
        currentWeapon.enabled = true;
        Destroy(pickup.gameObject);
    }
}
