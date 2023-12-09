using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ObjectPool
{
    //这个对象池是总对象池，它的子物体是具体的对象池（如BulletPool）
    private GameObject pool;
    private static ObjectPool instance;
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();
    
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }


    public GameObject GetGameObject(GameObject prefab)
    {
        //先初始化总对象池
        if (pool == null)
        {
            pool = new GameObject("ObjectPool");
        }
        //如果说没有该对象的对象池，就创建一个该对象的对象池
        if (!objectPool.ContainsKey(prefab.name))
        {
            objectPool.Add(prefab.name, new Queue<GameObject>());
            //创建了这个对象池后，把这个对象池附加到总对象池上
            GameObject go = new GameObject(prefab.name + "Pool");
            go.transform.parent = pool.transform;
        }
        //如果说这个对象池中没有对象，就先创建一个放入对象池
        if (objectPool[prefab.name].Count == 0)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.transform.parent = pool.transform.Find(prefab.name + "Pool");
            objectPool[prefab.name].Enqueue(go);
            go.SetActive(false);
        }
        //然后从这个对象池中取出一个对象，设置为Active
        GameObject gameObject = objectPool[prefab.name].Dequeue();
        gameObject.SetActive(true);
        return gameObject;
    }
    
    public void RecycleGameObject(GameObject gameObject)
    {
        //首先找到对应的对象池,将这个Gameobject设为Inactive，然后放入对象池
        objectPool[gameObject.name.Replace("(Clone)", string.Empty)].Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}

