using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] protected float dropChance = .2f;
    [SerializeField] private List<GameObject> weaponsPrefabs;

    public void GenerateDropChance(Vector3 position)
    {
        float roll = Random.value;
        if (roll <= dropChance && weaponsPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, weaponsPrefabs.Count);
            Instantiate(weaponsPrefabs[randomIndex], position, Quaternion.identity);
        }
    }
}
