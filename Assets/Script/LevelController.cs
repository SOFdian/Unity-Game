using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    #region 初始化
    public ItemCarry itemCarry;
    public Creator creator;
    public PlayerAttribute playerAttribute;

    bool isShow = false;
    //各个UI
    public GameObject levelPanel;
    public GameObject showButton;
    public Text[] itemText;
    public Button bagButton;
    public Image timeBar;
    //轮次时间
    public float levelTime = 200;

    MyTimer myTimer;
    private void Awake()
    {
        creator = GetComponent<Creator>();
        playerAttribute = GameObject.Find("Player").GetComponent<PlayerAttribute>();
        timeBar = GameObject.Find("关卡计时器").transform.Find("时间").GetComponent<Image>();
        bagButton = GameObject.Find("○").GetComponent<Button>();
        showButton = GameObject.Find("隐藏/显示");
        itemCarry = GameObject.Find("Player").GetComponent<ItemCarry>();
        levelPanel = GameObject.Find("关卡控制面板");
        itemText = levelPanel.GetComponentsInChildren<Text>();
        levelPanel.SetActive(false);
        showButton.SetActive(false);

        myTimer = new MyTimer();
        LevelBegin();
    }
    #endregion

    private void FixedUpdate()
    {
        myTimer.runTheClock();
        if (myTimer.GetTime() >= levelTime)
        {
            LevelOver();
        }
        timeBar.fillAmount = 1-myTimer.GetTime()/levelTime;
    }
    //波次结束
    public void LevelOver()
    {
        playerAttribute.FullLife();
        //停止刷怪
        creator.StopCreate();
        //停止计时
        myTimer.refreshTime();
        myTimer.stop = true;
        //启用背包和卡槽
        bagButton.interactable = true;
        //显示界面
        levelPanel.SetActive(false);
        showButton.SetActive(true);
        //随机选取道具并且显示
        List<string> allItem = itemCarry.ShowItem();
        for(int i=0;i<allItem.Count;i++)
        {
            itemText[i].text = allItem[i];
        }
        //升级敌人
        EnemyData.Instance.UpdateData();
    }
    //波次开始
    public void LevelBegin()
    {
        //开始计时
        myTimer.stop = false;
        //出怪
        creator.BeginCreate();
        //禁用背包和卡槽
        bagButton.interactable = false;
        //关闭界面
        levelPanel.SetActive(false);
        showButton.SetActive(false);
    }
    //选择道具
    public void ChoosePrize(int num)
    {
        itemCarry.ChooseItem(num);
        LevelBegin();
    }
    //隐藏/显示
    public void SwitchShow()
    {
        levelPanel.SetActive(!isShow);
        isShow = !isShow;
    }
}
