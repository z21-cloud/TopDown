using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    void Update()
    {
        HandleInput();  
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.TryShoot();
        }
    }
}
