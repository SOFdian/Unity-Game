using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCover : Item
{
    public GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");

    }
    public override void run()
    {
        //找到player，启用Cover
        this.gameObject.SetActive(true);
        
        this.transform.SetParent(player.transform);
        Debug.Log(9);
        
    }
}
