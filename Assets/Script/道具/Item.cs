using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemCarry itemCarry;
    protected virtual void Awake() {
        //初始化道具
        Initialize(itemCarry);
    }
    public virtual void Initialize(ItemCarry temp){
        itemCarry = temp;
    }
    public virtual void run(){

    }

}
