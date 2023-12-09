using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover_OnHurt : OnHurt
{
    Cover cover;
    void Start()
    {
        cover = GetComponent<Cover>();
    }
    public override float CalculateDamage(List<float> damage, List<string> type, float initialDamage)
    {
        cover.ChangeLife(initialDamage);
        return 0;
    }

}
