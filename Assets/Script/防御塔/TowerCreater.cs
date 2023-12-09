using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] all;
    public void CreateTower(Vector3 position,string name){
        Debug.Log(name);
        for(int i=0;i<all.Length;i++){
            if(all[i].name==name){
                Instantiate(all[i],position,Quaternion.identity);
                
            }
        }
    }
}
