using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyClick : MonoBehaviour,  IPointerClickHandler
{
    public UnityEvent<PointerEventData> rightClick;

    public CardList cardList;

    private void Awake()
    {
        rightClick.AddListener(new UnityAction<PointerEventData>(UpGradeCard));

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            rightClick.Invoke(eventData);
        }
    }
    private void UpGradeCard(PointerEventData eventData)
    {
        //获取按钮的index
        int index = int.Parse(gameObject.name.Substring(7, 1));
        //设置升级面板
        cardList.SelectCard(index);
    }
}
