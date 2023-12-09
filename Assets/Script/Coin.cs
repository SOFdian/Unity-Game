using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameObject player;
    PlayerAttribute playerAttribute;
    MyTimer timer;
    bool chasePlayer = false;
    float lerpSpeed = 5;
    float moveSpeed = 40;
    void Start()
    {
        player = GameObject.Find("Player");
        playerAttribute = player.GetComponent<PlayerAttribute>();   
        timer = new MyTimer();
    }
    private void OnEnable()
    {
        chasePlayer = false;
    }

    private void FixedUpdate()
    {
        if (chasePlayer)
        {
            Follow();
            if(Vector2.Distance(transform.position, player.transform.position) < 0.3f)
            {
                ObjectPool.Instance.RecycleGameObject(this.gameObject);
                //让玩家金币++
                playerAttribute.AddCoin();

            }
            return;
        }
        timer.runTheClock();
        if (!chasePlayer&&timer.GetTime() > 0.2f)
        {
            timer.refreshTime();
            //寻找玩家
            if (Vector2.Distance(transform.position, player.transform.position) < 5f)
            {
                chasePlayer = true;
            }
        }  

    }

    void Follow()
    {
        Vector2 lerpTarget = Vector3.Lerp(transform.position, player.transform.position , lerpSpeed * Time.fixedDeltaTime);
        //第三个参数是每帧最大移动距离
        transform.position = Vector3.MoveTowards(transform.position, lerpTarget, moveSpeed * Time.fixedDeltaTime);
    }
}
