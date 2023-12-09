using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_CardList : CardList
{

    Dictionary<string, GameObject> uiType = new Dictionary<string, GameObject>();


    List<float> damage = new List<float>();
    List<string> type = new List<string>();
    //���������Ǵӿ����м����������ֵ����������ֻ�ǿ���ֵ
    List<float> newDamage = new List<float>();
    List<string> newType = new List<string>();
    
    public override void Awake()
    {
        base.Awake();
        //���ڳ�ʼ��˳��ԭ�򣬿��ܴ�ʱui����active�ģ��޷���Gameobject.Find
        GameObject temp = GameObject.Find("Canvas").transform.Find("��������").gameObject;

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
        //Ԫ��ͼƬ
        GameObject uiBar = GameObject.Find("Ԫ����");
        int childCount = uiBar.transform.childCount;
        // ����
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
        //Ҫ��ȡ��ǰ�������ӵ�������
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
        //�޿�Ƭ
        if (type.Count == 0)
        {

        }
        //�����
        else if (type.Count == 1)
        {
            
            newType = type;
            newDamage = damage;

        } 
        //�����
        else
        {
            newType = type;
            newDamage = damage;
            //���ּ�����Ԫ�ػᷢ�����
            while (newType.Count >= 2)
            {
                string a = newType[0];
                string b = newType[1];
                float c = newDamage[0];
                float d = newDamage[1];

                //�����if�൱�ڰ�ǰ����Ԫ������������ŵ�list�����
                //removeat������Ԫ�ػ�����ǰ��
                if ((a == "��Ԫ��" && b == "��Ԫ��") || (a == "��Ԫ��" && b == "��Ԫ��"))
                {
                    newType.Add("���");
                }
                else if ((a == "��Ԫ��" && b == "��Ԫ��") || (a == "��Ԫ��" && b == "��Ԫ��"))
                {
                    newType.Add("����");
                }
                else if ((a == "��Ԫ��" && b == "��Ԫ��") || (a == "��Ԫ��" && b == "��Ԫ��"))
                {
                    newType.Add("��ʴ");
                }
                //����������������˵�����������Ԫ�أ�����ʱֻʣһ��ԭʼԪ���ˣ��˳�����
                //��˱�����Add����Remove
                else
                {
                    break;
                }
                newType.RemoveAt(0);
                newType.RemoveAt(0);
                //ͬ��
                newDamage.RemoveAt(0);
                newDamage.RemoveAt(0);
                newDamage.Add(c + d);
            }
        }
        //�޸�ui
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
