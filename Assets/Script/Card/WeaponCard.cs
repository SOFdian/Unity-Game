using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : Card
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //找到player的cardlist并调用add
            other.GetComponent<Weapon_CardList>().AddCard(this.cname, this.num);
            //销毁gameobject不会销毁掉脚本
            Destroy(gameObject);
        }
    }
}
