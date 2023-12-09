using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_OnHurt : OnHurt
{
    HPIcon hpIcon;
    private void Start()
    {
        hpIcon = GetComponent<HPIcon>();
    }

    public override float CalculateDamage(List<float> damage, List<string> type, float initialDamage)
    {
        hpIcon.ChangeLife(initialDamage);
        return 0;

    }
}
