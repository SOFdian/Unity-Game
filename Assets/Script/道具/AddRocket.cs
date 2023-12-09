using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRocket : Item
{
    public GameObject rocket;
    public override void run()
    {
        ////只用考虑是霰弹的情况
        //if(itemCarry.currentWeapon==0){
        //    //如果是shotgun
        //    itemCarry.weapons[0].GetComponent<ShotGun>().bullet = rocket;
        //}
    }
}
