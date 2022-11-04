using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPollingManager : MonoBehaviour
{
    public static ObjectPollingManager instance;
    public GameObject bulletPrefab;
    public int bulletAmount = 5;

    private List<GameObject> bullets;
    private void Awake()
    {
        instance = this;
        bullets = new List<GameObject>(bulletAmount);
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            bullets.Add(prefabInstance);
        }
    }

    public GameObject GetBullet()
    {
        int totalBullets = bullets.Count;
        for(int i=0; i < totalBullets; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].SetActive(true);
                //------
                return bullets[i];
            }
        }
        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.SetActive(true);
        bullets.Add(prefabInstance);
        return prefabInstance;

    }
}
