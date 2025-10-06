using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    void OnPickUp(GameObject picker);
    PickableType GetPickableType();
}

public enum PickableType
{
    Weapon,
    Hearts
}
