using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Creator : MonoBehaviour
{
    public GameObject test;
    //生成敌人的间隔
    public float interval ;
    //存储着敌人种类
    public List<Level> levels = new List<Level>();
    //敌人刷新点
    List<Transform> enemyCreator = new List<Transform>();
    //从当前Level中读取数据后存在这里
    public List<GameObject> currentEnemies = new List<GameObject>();
    //存取生成的敌人
    public List <GameObject> allEnemies = new List<GameObject>();
    //战斗中
    bool fighting;


    MyTimer timer;
    public int level = 0;
    //先暂且定个5关，循环着来

    void Awake()
    {
        timer = new MyTimer();
        GameObject temp = GameObject.Find("敌人出生点");
        int childCount = temp.transform.childCount;
        // 遍历
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

    //开始创建敌人
    public void BeginCreate()
    {
        //读取当前关卡数据
        levels[level].enemies.ForEach(i => currentEnemies.Add(i));
        interval = levels[level].interval;
        fighting = true;
    }
    //停止创建敌人
    public void StopCreate()
    {

        level++;
        if (level == 4)
        {
            level = 0;
        }
        fighting = false;
        timer.refreshTime();
        //遍历场景中未死亡的敌人
        foreach(var x in allEnemies)
        {
            if (x.activeSelf)
            {
                ObjectPool.Instance.RecycleGameObject(x);
                //爆金币
            }
        }
    }

    public void CreateEnemy()
    {
        //从敌人中随机选一个，再从出生点中随机选一个
        int enemyIndex = Random.Range(0, currentEnemies.Count);
        int creatorIndex = Random.Range(0, enemyCreator.Count);

        GameObject temp = ObjectPool.Instance.GetGameObject(currentEnemies[enemyIndex]);
        //GameObject temp = ObjectPool.Instance.GetGameobject(test); 
        allEnemies.Add(temp);
        //必须先设置position，因为初始化中的寻路依赖于position
        temp.transform.position = enemyCreator[creatorIndex].position;
        temp.GetComponent<StateMachine>().Initialize();
        //对于Cover也要重新启用
        try
        {
            temp.transform.Find("Cover").GetComponent<Cover>().Initialize();
        }
        catch
        {

        }
        
    }

}
