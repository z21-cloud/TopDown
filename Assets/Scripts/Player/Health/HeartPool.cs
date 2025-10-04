using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPool : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform container;

    private List<Image> pool = new List<Image>();

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    public void UpdateHearts(int current, int max)
    {
        while(pool.Count < max)
        {
            var obj = Instantiate(heartPrefab, container).GetComponent<Image>();
            pool.Add(obj);
        }

        for (int i = 0; i < pool.Count; i++)
        {
            if(i < max)
            {
                pool[i].gameObject.SetActive(true);
                pool[i].sprite = (i < current) ? fullHeart : emptyHeart;
            }
            else
            {
                pool[i].gameObject.SetActive(false);
            }
        }
    }
}
