using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class OnHurt:MonoBehaviour
{
    public virtual float CalculateDamage(List<float> damage, List<string> type, float initialDamage)
    {
        if (type.Count == 0)
        {
            return initialDamage;
        }
        float finalDamage = 0;
        //基类考虑的是玩家敌人，也就是会考虑元素
        for (int i = 0; i < type.Count; i++)
        {
            if (type[i] == "火元素")
            {
                finalDamage += 1;
            }
            else if (type[i] == "电元素")
            {
                finalDamage += initialDamage * (1 + damage[i]);
            }
            else if (type[i] == "冰元素")
            {
                finalDamage += (initialDamage * (1 + damage[i]));
            }
            else if (type[i] == "电磁")
            {
                finalDamage += (initialDamage * (1 + damage[i]));
            }
            else if (type[i] == "毒气")
            {
                finalDamage += (initialDamage * (1 + damage[i]));
            }
            else if (type[i] == "腐蚀")
            {
                finalDamage += (2 * initialDamage * (1 + damage[i]));
            }
            else if (type[i] == "无")
            {
                finalDamage += (initialDamage);
            } 
        }
        CalculateType(type);
        return finalDamage;
    }

    public virtual void CalculateType(List<string> type)
    {

    }
    public virtual void OnEnable()
    {
        Initialize();
    }
    public virtual void Initialize()
    {

    }
}
