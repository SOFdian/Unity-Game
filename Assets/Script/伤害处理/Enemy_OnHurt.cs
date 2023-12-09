using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_OnHurt : OnHurt
{
    public GameObject coin;
    public delegate void OnHurtDelegate();
    public event OnHurtDelegate onHurt;
    //���ڵ��ˣ���ScriptableObject������������
    public EnemyData enemyData;
    //��ǰ�������������ֵ
    public float curLife;
    public float fullLife;
    //��������
    public string armor;
    //ֻ�е��˻���Ϊ�ܵ��˺������Ͳ�ͬ����ɫ
    //�ɻ���̹�˵�spriteRenderer������ͬ��Ҫ����
    public SpriteRenderer spriteRenderer;
    public Color color;
    public Color fireColor;
    public Color iceColor;
    public Color thunderColor;
    public Color poisonColor;
    public Color normalColor;

    MyTimer timer;
    float interval;
    //���˵�OnHurtͬʱҲ��Attribute������

    private void Start()
    {
        coin = Resources.Load<GameObject>("Coin");
        timer = new MyTimer();
        interval = 0.2f;
    }
    void ChangeLife(float damage)
    {
        curLife-=damage;
        if (curLife <= 0)
        {
            //����ҿ�
            GameObject temp = ObjectPool.Instance.GetGameObject(coin);
            temp.transform.position = gameObject.transform.position;
            //���ٲ��ҳ�ʼ��״̬
            ObjectPool.Instance.RecycleGameObject(this.gameObject);

        }
    }

    public override float CalculateDamage(List<float> damage, List<string> type, float initialDamage)
    {
        ChangeLife(base.CalculateDamage(damage, type, initialDamage));
        return 0;
    }

    public override void Initialize()
    {
        fullLife = EnemyData.Instance.fullLife;
        curLife = fullLife;
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

    #region Ԫ���˺�
    public override void CalculateType(List<string> type)
    {
        foreach (var item in type)
        {
            if (item == "��Ԫ��")
            {
                onHurt += Fire;
                Invoke("DeleteFire", 5);
            }
        }
    }
    void Fire()
    {
        ChangeLife(5);
        changeColor(fireColor); 
    }

    void DeleteFire()
    {
        onHurt -= Fire;
        changeColorBack();
    }
    #endregion

    #region ���˱�ɫ
    void changeColor(Color color)
    {
        spriteRenderer.color = color;
    }
    void changeColorBack()
    {
        if (spriteRenderer.color != color)
        {
            spriteRenderer.color = color;
        }
    }
    #endregion
}
