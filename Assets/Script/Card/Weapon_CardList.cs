using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_CardList : CardList
{

    Dictionary<string, GameObject> uiType = new Dictionary<string, GameObject>();


    List<float> damage = new List<float>();
    List<string> type = new List<string>();
    //以下两个是从卡牌中计算出的最终值，以上两个只是卡牌值
    List<float> newDamage = new List<float>();
    List<string> newType = new List<string>();
    
    public override void Awake()
    {
        base.Awake();
        //由于初始化顺序原因，可能此时ui不是active的，无法用Gameobject.Find
        GameObject temp = GameObject.Find("Canvas").transform.Find("武器卡槽").gameObject;

        text = temp.transform.Find("Text").gameObject.GetComponent<Text>();

        Image[] tempImage = temp.transform.Find("Equiped").GetComponentsInChildren<Image>();
        foreach(var x in tempImage)
        {
            equipedImages.Add(x);
        }
        tempImage = temp.transform.Find("UnEquiped").GetComponentsInChildren<Image>();
        foreach (var x in tempImage)
        {
            unEquipedImages.Add(x);
        }
        //元素图片
        GameObject uiBar = GameObject.Find("元素栏");
        int childCount = uiBar.transform.childCount;
        // 遍历
        for (int i = 0; i < childCount; i++)
        {
            var child = uiBar.transform.GetChild(i);
            uiType.Add(child.name, child.gameObject);
            child.gameObject.SetActive(false);
        }
    }


    public override void RefreshUI()
    {
        base.RefreshUI();
        refresh();
    }

    void refresh()
    {
        text.text = "";
        //要获取当前武器的子弹的属性
        try
        {
            text.text += playerController.currentWeapen.GetComponent<ShotGun>().bullet.GetComponent<Bullet>().name + " " + playerController.currentWeapen.GetComponent<ShotGun>().bullet.GetComponent<Bullet>().initialDamage + "\n";
        }
        catch
        {
            text.text += playerController.currentWeapen.GetComponent<LaserGun>().damage + "\n";
        }
        damage = new List<float>();
        type = new List<string>();
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (equipedCard[i, j].cname != null)
                {
                    type.Add(equipedCard[i, j].cname);
                    damage.Add(equipedCard[i, j].num);
                }
            }
        }
        CalTypeAndDamage();
        List<string> temp = getType();
        List<float> newD = getDamage();
        for (int i = 0; i < temp.Count; i++)
        {
            text.text += temp[i] + " " + newD[i] + "\n";
        }
    }

    public void CalTypeAndDamage()
    {
        newType.Clear();
        newDamage.Clear();
        //无卡片
        if (type.Count == 0)
        {

        }
        //无组合
        else if (type.Count == 1)
        {
            
            newType = type;
            newDamage = damage;

        } 
        //有组合
        else
        {
            newType = type;
            newDamage = damage;
            //两种及以上元素会发生组合
            while (newType.Count >= 2)
            {
                string a = newType[0];
                string b = newType[1];
                float c = newDamage[0];
                float d = newDamage[1];

                //下面的if相当于把前两个元素组合起来，放到list最后面
                //removeat后，其余元素会依次前移
                if ((a == "火元素" && b == "电元素") || (a == "电元素" && b == "火元素"))
                {
                    newType.Add("电磁");
                }
                else if ((a == "火元素" && b == "冰元素") || (a == "冰元素" && b == "火元素"))
                {
                    newType.Add("毒气");
                }
                else if ((a == "电元素" && b == "冰元素") || (a == "冰元素" && b == "电元素"))
                {
                    newType.Add("腐蚀");
                }
                //如果不是上述情况，说明碰到了组合元素，即此时只剩一个原始元素了，退出即可
                //因此必须先Add，再Remove
                else
                {
                    break;
                }
                newType.RemoveAt(0);
                newType.RemoveAt(0);
                //同理
                newDamage.RemoveAt(0);
                newDamage.RemoveAt(0);
                newDamage.Add(c + d);
            }
        }
        //修改ui
        foreach(var x in uiType)
        {
            if (newType.Contains(x.Key))
            {
                x.Value.SetActive(true);
            } else
            {
                x.Value.SetActive(false);
            }
        }
    }

    public List<float> getDamage()
    {
        return newDamage;
    }
    public List<string> getType()
    {
        return newType;
    }
}
