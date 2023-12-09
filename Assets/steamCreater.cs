using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steamCreater : MonoBehaviour
{
    public float time;
    public float a;
    public GameObject steam;
    public void CreateSteam(Vector3 pos){
        GameObject tmep =  ObjectPool.Instance.GetGameObject(steam);
        tmep.transform.position = pos;
        Invoke("RecycleSteam",time);
    }
    void RecycleSteam(GameObject gb)
    {
        ObjectPool.Instance.RecycleGameObject(gb);
    }

    
}
