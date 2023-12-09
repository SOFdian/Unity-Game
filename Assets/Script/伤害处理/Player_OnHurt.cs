using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player_OnHurt : OnHurt
{
    protected MyTimer timer;
    protected float interval;
    protected string armor;
    public delegate void OnHurtDelegate();
    public event OnHurtDelegate onHurt;

    PlayerAttribute playerAttribute;

    public  void Start()
    {
        //ȼ��Ч��
        timer = new MyTimer();
        interval = 0.2f;

        playerAttribute = GetComponent<PlayerAttribute>();

    }
    public override float CalculateDamage(List<float> damage, List<string> type, float initialDamage)
    {
        //��Ѫ
        //float finalDamage = base.CalculateDamage(damage, type, initialDamage);
        playerAttribute.CalculateDamage(initialDamage);
        ShowDamage();
        return 0;
    }
    //�����ٱ���type�����ò�ͬЧ��
    public override void CalculateType(List<string> type)
    {
        foreach (var item in type)
        {
            if(item== "��Ԫ��")
            {
                onHurt += Fire;
                Invoke("DeleteFire", 5);
                ShowDamage();
            }
        }
    }
    void Fire()
    {
        playerAttribute.CalculateDamage(5);
    }
    void DeleteFire()
    {
        onHurt -= Fire;
    }
    //��ʾ�˺�����
    private void ShowDamage()
    {

    }
    private void FixedUpdate()
    {
        if (onHurt != null)
        {
            //ȼ���ж�
            timer.runTheClock();
            if (timer.GetTime() >= interval)
            {
                onHurt();
                timer.refreshTime();
            }
        }
        
    }


}
