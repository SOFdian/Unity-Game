using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBoom_PlaneState_Boom : PlaneState
{
    public override void Enter()
    {
        planeController.SetVelocity(Vector2.zero);
        //���ű�ը����
        planeController.GetComponent<Animator>().Play("��ը");

    }
    

}
