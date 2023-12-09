using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour
{
    Image bloodStrip;
    GameObject playerUI;
    GameObject weaponUI;
    Text coinUI;
    public Text text;

    public float life;
    public float fullLife;
    public float speed;
    public int coin;
    public Initial[] weapons;

    private void Awake()
    {
        coinUI = GameObject.Find("金币").GetComponentInChildren<Text>();
        playerUI = GameObject.Find("飞机卡槽");
        weaponUI = GameObject.Find("武器卡槽");
        bloodStrip = GameObject.Find("血").GetComponent<Image>();


        life = fullLife;
        coin = 0;

        weapons = GetComponentsInChildren<Initial>();


        playerUI.SetActive(false);
        weaponUI.SetActive(false);
    }

    //角色相关的UI控制
    public void ClickBag()
    {
        if (playerUI.activeSelf || weaponUI.activeSelf)
        {
            playerUI.SetActive(false);
            weaponUI.SetActive(false);
            Time.timeScale = 1;
            
        }
        else
        {
            playerUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void OpenPlayer()
    {
        playerUI.SetActive(true);
        weaponUI.SetActive(false);
    }
    public void OpenWeapon()
    {
        playerUI.SetActive(false);
        weaponUI.SetActive(true);
    }
    
    //伤害计算和生命恢复
    public void FullLife()
    {
        life = fullLife;
    }
    public void CalculateDamage(float damage)
    {
        life -= damage;
    }
    
    //金币计算
    public void AddCoin()
    {
        coin++;
        coinUI.text = coin.ToString();
    }
    public void SubCoin()
    {
        coin--;
        coinUI.text = coin.ToString();
    }
    
    //更新血条，死亡加载场景
    private void Update()
    {
        
        bloodStrip.fillAmount = life / fullLife;
        
        if (life <= 0)
        {
            SceneManager.LoadScene("MyWork");
        }
    }

}
