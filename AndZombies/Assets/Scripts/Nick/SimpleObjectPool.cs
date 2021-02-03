using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool
{
    private List<GameObject> objects = new List<GameObject>();

    public SimpleObjectPool(GameObject item, int amount)
    {
        //creation
        for (int i = 0; i < amount; i++)
        {
            GameObject newObject = Object.Instantiate(item);
            objects.Add(newObject);
            newObject.SetActive(false);
        }
    }

    public void DestroyPool()
    {
        //destruction
        objects.Clear();
    }

    public GameObject GetObject()
    {
        //gets an inactive object from a pool
        foreach (GameObject item in objects)
        {
            if (!item.activeSelf)
            {
                return item;
            }
        }

        return null;
    }
}