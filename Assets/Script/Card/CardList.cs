using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;


public struct CardData
{
    public string cname;
    public float num;
    public CardData(string cname,float num)
    {
        this.cname = cname;
        this.num = num;
    }
}
public class CardList : MonoBehaviour
{
    protected PlayerController playerController;
    protected PlayerAttribute playerAttribute;
    protected Text text;
    //升级面板
    public Text cardName;
    public Text cardNum;
    public Button upgradeButton;
    //已装备的卡
    protected CardData[,] equipedCard = new CardData[2, 3];
    //未装备的卡
    protected CardData[,] unEquipedCard = new CardData[3, 5];

    //需要空白卡槽的sprite
    public Sprite blankCard;
    //需要所有卡片的sprite
    public Sprite[] cardSprite;
    Dictionary<string, Sprite> nameToSprite = new Dictionary<string, Sprite>();

    //已装备区的image组件
    protected List<Image> equipedImages = new List<Image>();
    //未装备区的image组件
    protected List<Image> unEquipedImages = new List<Image>();

    //右键选中的card，枚举是浅拷贝所以这里用index
    public int selectedCardIndex;

    public virtual void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAttribute = GetComponent<PlayerAttribute>();
        foreach (var sprite in cardSprite)
        {
            nameToSprite.Add(sprite.name, sprite);
        }
    }

    //捡到卡之后会触发这个函数
    public virtual void AddCard(string cname, float num)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (unEquipedCard[i, j].cname==null)
                {
                    unEquipedCard[i, j] = new CardData(cname, num);
                    RefreshUI();
                    return;
                }
            }
        }
        
    }
    //升级卡片
    public virtual void UpGradeCard()
    {
        if (playerAttribute.coin <= 0)
        {
            cardName.text = "金币不足";
            cardNum.text = "金币不足";
            return;
        }
        playerAttribute.SubCoin();
        int row = selectedCardIndex / 5;
        int column = selectedCardIndex % 5;
        if (unEquipedCard[row, column].cname == null)
        {
            return;
        }
        //这里根据卡牌的类型不同进行不同的升级
        string cname = unEquipedCard[row, column].cname;
        switch (cname)
        {
            case "life":
                unEquipedCard[row, column].num += 100;
                break;
            case "bulletSpeed":
                unEquipedCard[row, column].num += 1;
                break;
            case "interval":
                unEquipedCard[row, column].num += 0.01f;
                break;
            default:
                unEquipedCard[row, column].num += 10;
                break;
        }
        RefreshUI();
        
    }
    //右键选择卡牌
    public virtual void SelectCard(int index)
    {
        selectedCardIndex = index;
        RefreshUI();
    }
    #region 装卡和卸卡
    public virtual void EquipCard(int index)
    {
        //如果已装备满了
        if (equipedCard[1, 2].cname != null)
        {
            return;
        }
        //查看点击的是几行几列的按钮
        int row = index / 5;
        int column = index % 5;
        //放到已装备区
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (equipedCard[i, j].cname == null) 
                {
                    equipedCard[i, j] = unEquipedCard[row, column];
                    //卸下未装备区的卡片
                    unEquipedCard[row, column] = new CardData(null, 0);
                    RefreshUI();
                    return;
                }
            }
        }

    }

    public virtual void UnEquipCard(int index)
    {
        //这个不会满
        //查看点击的是几行几列的按钮
        int row = index / 5;
        int column = index % 5;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (unEquipedCard[i, j].cname == null) 
                {
                    unEquipedCard[i, j] = equipedCard[row,column];
                    equipedCard[row, column] = new CardData(null, 0);
                    RefreshUI();
                    return;
                }
            }
        }

    }

    public virtual void RefreshUI()
    {
        int index = 0 ;
        //已装备区
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (equipedCard[i, j].cname == null)
                {
                    equipedImages[index].sprite = blankCard;
                } else
                {
                    equipedImages[index].sprite = nameToSprite[equipedCard[i, j].cname];
                }
                index++;
            }
        }
        index = 0;
        //未装备区
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if(unEquipedCard[i, j].cname == null)
                {
                    unEquipedImages[index].sprite = blankCard;
                } else
                {
                    unEquipedImages[index].sprite = nameToSprite[unEquipedCard[i, j].cname];
                }
                index++;
            }
        }
        //升级区
        int row = selectedCardIndex / 5;
        int column = selectedCardIndex % 5;
        if (unEquipedCard[row,column].cname == null)
        {
            cardName.text = "未选中";
            cardNum.text = "未选中";
        } else
        {
            cardName.text = unEquipedCard[row, column].cname;
            cardNum.text = unEquipedCard[row, column].num.ToString();
        }

    }
    #endregion

}
