using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNum : Item
{
    //计算itemCarry中有多少个num，然后修改itemCarry的num，然后修改武器的num
    public override void run()
    {
        //Debug.Log(95);
        //itemCarry.num=0;
        //int n=0;
        //for (int i = 0; i < itemCarry.items.Count; i++)
        //{
        //    if (itemCarry.items[i].GetType() == typeof(AddNum))
        //    {
        //        n++;
        //    }
        //}
        //if(itemCarry.currentWeapon==0){
        //    //如果是shotgun
        //    itemCarry.weapons[0].GetComponent<ShotGun>().bulletNum = n;
        //} else {
        //    //如果是laserGun
        //    itemCarry.weapons[1].GetComponent<LaserGun>().num = n;
        //    itemCarry.weapons[1].GetComponent<LaserGun>().creatCOPY();
        //}
    }
}
