using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Creator : MonoBehaviour
{
    public GameObject test;
    //���ɵ��˵ļ��
    public float interval ;
    //�洢�ŵ�������
    public List<Level> levels = new List<Level>();
    //����ˢ�µ�
    List<Transform> enemyCreator = new List<Transform>();
    //�ӵ�ǰLevel�ж�ȡ���ݺ��������
    public List<GameObject> currentEnemies = new List<GameObject>();
    //��ȡ���ɵĵ���
    public List <GameObject> allEnemies = new List<GameObject>();
    //ս����
    bool fighting;


    MyTimer timer;
    public int level = 0;
    //�����Ҷ���5�أ�ѭ������

    void Awake()
    {
        timer = new MyTimer();
        GameObject temp = GameObject.Find("���˳�����");
        int childCount = temp.transform.childCount;
        // ����
        for (int i = 0; i < childCount; i++)
        {
            var child = temp.transform.GetChild(i);
            enemyCreator.Add(child.transform);
        }

    }
    private void FixedUpdate()
    {
        timer.runTheClock();
        if (timer.GetTime() > interval && fighting)
        {
            CreateEnemy();
            timer.refreshTime();
        }
    }

    //��ʼ��������
    public void BeginCreate()
    {
        //��ȡ��ǰ�ؿ�����
        levels[level].enemies.ForEach(i => currentEnemies.Add(i));
        interval = levels[level].interval;
        fighting = true;
    }
    //ֹͣ��������
    public void StopCreate()
    {

        level++;
        if (level == 4)
        {
            level = 0;
        }
        fighting = false;
        timer.refreshTime();
        //����������δ�����ĵ���
        foreach(var x in allEnemies)
        {
            if (x.activeSelf)
            {
                ObjectPool.Instance.RecycleGameObject(x);
                //�����
            }
        }
    }

    public void CreateEnemy()
    {
        //�ӵ��������ѡһ�����ٴӳ����������ѡһ��
        int enemyIndex = Random.Range(0, currentEnemies.Count);
        int creatorIndex = Random.Range(0, enemyCreator.Count);

        GameObject temp = ObjectPool.Instance.GetGameObject(currentEnemies[enemyIndex]);
        //GameObject temp = ObjectPool.Instance.GetGameobject(test); 
        allEnemies.Add(temp);
        //����������position����Ϊ��ʼ���е�Ѱ·������position
        temp.transform.position = enemyCreator[creatorIndex].position;
        temp.GetComponent<StateMachine>().Initialize();
        //����CoverҲҪ��������
        try
        {
            temp.transform.Find("Cover").GetComponent<Cover>().Initialize();
        }
        catch
        {

        }
        
    }

}
