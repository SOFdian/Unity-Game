using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFriend : Item
{
    public GameObject friend;
    public List<GameObject> friends = new List<GameObject>();
    // Start is called before the first frame update
    public override void run()
    {
        ////先检查现在有几个飞机
        //int num = friends.Count;
        //int tempNum = 0;
        ////遍历itemCarry
        //for (int i = 0; i < itemCarry.items.Count; i++)
        //{
        //    if (itemCarry.items[i].GetType() == typeof(AddFriend))
        //    {
        //        var temp = (AddFriend)itemCarry.items[i];
        //        if(temp.friend.name == friend.name){
        //            tempNum++;
        //        }
        //    }
        //}
        //Debug.Log(tempNum);
        //Debug.Log(num);
        ////生成tempNum-num个飞机
        //for (int i = 0; i < tempNum - num; i++)
        //{
        //    GameObject temp = Instantiate(friend, transform.position, Quaternion.identity);
        //    friends.Add(temp);
        //}
    }
}
