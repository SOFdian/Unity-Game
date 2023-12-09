using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    #region ��ʼ��
    public ItemCarry itemCarry;
    public Creator creator;
    public PlayerAttribute playerAttribute;

    bool isShow = false;
    //����UI
    public GameObject levelPanel;
    public GameObject showButton;
    public Text[] itemText;
    public Button bagButton;
    public Image timeBar;
    //�ִ�ʱ��
    public float levelTime = 200;

    MyTimer myTimer;
    private void Awake()
    {
        creator = GetComponent<Creator>();
        playerAttribute = GameObject.Find("Player").GetComponent<PlayerAttribute>();
        timeBar = GameObject.Find("�ؿ���ʱ��").transform.Find("ʱ��").GetComponent<Image>();
        bagButton = GameObject.Find("��").GetComponent<Button>();
        showButton = GameObject.Find("����/��ʾ");
        itemCarry = GameObject.Find("Player").GetComponent<ItemCarry>();
        levelPanel = GameObject.Find("�ؿ��������");
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
    //���ν���
    public void LevelOver()
    {
        playerAttribute.FullLife();
        //ֹͣˢ��
        creator.StopCreate();
        //ֹͣ��ʱ
        myTimer.refreshTime();
        myTimer.stop = true;
        //���ñ����Ϳ���
        bagButton.interactable = true;
        //��ʾ����
        levelPanel.SetActive(false);
        showButton.SetActive(true);
        //���ѡȡ���߲�����ʾ
        List<string> allItem = itemCarry.ShowItem();
        for(int i=0;i<allItem.Count;i++)
        {
            itemText[i].text = allItem[i];
        }
        //��������
        EnemyData.Instance.UpdateData();
    }
    //���ο�ʼ
    public void LevelBegin()
    {
        //��ʼ��ʱ
        myTimer.stop = false;
        //����
        creator.BeginCreate();
        //���ñ����Ϳ���
        bagButton.interactable = false;
        //�رս���
        levelPanel.SetActive(false);
        showButton.SetActive(false);
    }
    //ѡ�����
    public void ChoosePrize(int num)
    {
        itemCarry.ChooseItem(num);
        LevelBegin();
    }
    //����/��ʾ
    public void SwitchShow()
    {
        levelPanel.SetActive(!isShow);
        isShow = !isShow;
    }
}
