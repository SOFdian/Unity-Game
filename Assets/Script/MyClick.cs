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
        //��ȡ��ť��index
        int index = int.Parse(gameObject.name.Substring(7, 1));
        //�����������
        cardList.SelectCard(index);
    }
}
