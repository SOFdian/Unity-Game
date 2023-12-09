using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_CardList : CardList
{
    public float life;
    public float interval;
    public float bulletSpeed;
    private PlayerAttribute playerAttribute;

     
    public override void Awake()
    {
        life = 0;
        interval = 0;
        bulletSpeed = 0;

        base.Awake();

        playerAttribute = GetComponent<PlayerAttribute>();

        GameObject temp = GameObject.Find("Canvas").transform.Find("�ɻ�����").gameObject;

        text = temp.transform.Find("Text").gameObject.GetComponent<Text>();

        Image[] tempImage = temp.transform.Find("Equiped").GetComponentsInChildren<Image>();
        foreach (var x in tempImage)
        {
            equipedImages.Add(x);
        }
        tempImage = temp.transform.Find("UnEquiped").GetComponentsInChildren<Image>();
        foreach (var x in tempImage)
        {
            unEquipedImages.Add(x);
        }
    }
    public override void RefreshUI()
    {
        base.RefreshUI();
        refresh();
    }
    //���ݿ��Ƽ�������
    public void refresh()
    {
        life = 0;
        interval = 0;
        bulletSpeed = 0;
        text.text = "";
        //����ҵ�ui�У������Ƭ���ͻᴥ�����������ͬʱ�޸�PlayerAttribute������ֵ��һ����

        //��������
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (equipedCard[i, j].cname != null)
                {
                    if (equipedCard[i, j].cname == "life")
                    {
                        life = equipedCard[i, j].num;
                    }
                    else if (equipedCard[i, j].cname == "interval")
                    {
                        interval = equipedCard[i, j].num;
                    }
                    else if (equipedCard[i,j].cname=="bulletSpeed")
                    {
                        bulletSpeed = equipedCard[i,j].num;
                    }
                }
            }
        }

        //û�������ӳ�
        if (life == 0)
        {
            //�ָ���ʼ����ֵ�������ScriptObject��
            playerAttribute.fullLife = 100;
            playerAttribute.life = 100;
        } else
        {
            //��Ϊֻ�ڲ��μ������װ��������ֱ�ӻ���Ѫ
            playerAttribute.fullLife = life;
            playerAttribute.life = life;
        }
    }
    public float GetLife()
    {
        return life;
    }
    public float GetInterval()
    {
        return interval;
    }
    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
}
