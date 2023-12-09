using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoom : MonoBehaviour
{
    // Start is called before the first frame update
    public void FinishBoom()
    {
        ObjectPool.Instance.RecycleGameObject(gameObject);
    }
}
