using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickable
{
    [SerializeField] private Weapon weaponPrefab;
    public void OnPickUp(GameObject picker)
    {
        WeaponHolder holder = picker.GetComponent<WeaponHolder>();
        if (holder != null) holder.EqiupWeapon(this);
    }

    public Weapon GetWeaponPrefab()
    {
        return weaponPrefab;
    }
}
