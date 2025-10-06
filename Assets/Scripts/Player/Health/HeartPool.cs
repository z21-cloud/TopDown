using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPool : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private List<Image> pool = new List<Image>();

    public void UpdateHearts(int current, int max)
    {
        while(pool.Count < max)
        {
            var heart = Instantiate(heartPrefab, container);
            var image = heart.GetComponent<Image>();
            if(image != null)
            {
                pool.Add(image);
            }
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
