using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleText : MonoBehaviour
{
   public void Recycle()
    {
        Debug.Log("yes");
        ObjectPool.Instance.RecycleGameObject(this.transform.parent.gameObject);
    }

}
