using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPlaneAround : HelperPlane
{
    public float angleSpeed;
    float angle;
    Vector3 dir;
    protected override void Awake()
    {
        base.Awake();
        angle = 0;
    }
    private void FixedUpdate() {
        //确保transform.up始终指向玩家
        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0,0,1));
        Vector3 temp = new Vector3(1,0,0);
        dir = q * temp;
        transform.position = parent.transform.position + dir * 1.5f;
        angle+=angleSpeed;
        if(angle>=360){
            angle = 0;
        }
    }
}
