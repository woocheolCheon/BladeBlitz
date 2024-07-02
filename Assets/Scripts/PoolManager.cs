using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public GameObject[] objects;
    }

    public GameObject poolContainer; 
    public Pool[] pools;

    private void Start()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            Pool pool = pools[i];
            pool.objects = new GameObject[pool.size];

            for (int j = 0; j < pool.size; j++)
            {
                GameObject obj = Instantiate(pool.prefab, poolContainer.transform);
                obj.SetActive(false);
                pool.objects[j] = obj;
            }
        }
    }

    public GameObject MakeObj(string type)
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].tag == type)
            {
                GameObject[] targetPool = pools[i].objects;

                for (int j = 0; j < targetPool.Length; j++)
                {
                    if (!targetPool[j].activeSelf)
                    {
                        targetPool[j].SetActive(true);
                        return targetPool[j];
                    }
                }
            }
        }

        return null;
    }

    

}
