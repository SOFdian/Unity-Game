using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerController : MonoBehaviour
{

    public TowerCreater towerCreater;
    private Vector2 mousePos;
    private Vector2 faceDir;
    Rigidbody2D rb;
    public GameObject gb;
    private GameInput gameInput;
    Tilemap tilemap;
    //存储所有发射器
    public List<GameObject> weapens;
    public GameObject currentWeapen;

    void Awake()
    {

        weapens = new List<GameObject>();
        //获取全部子物体
        for(int i = 0; i < transform.childCount; i++){
            weapens.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
        currentWeapen = weapens[0];
        weapens[0].SetActive(true);

        rb = GetComponent<Rigidbody2D>();
        gameInput = GetComponent<GameInput>();
    }

    //使角色面对鼠标所指方向
    public void GetFaceDir(){
        //获取鼠标位置的世界坐标
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //获取面对方向
        faceDir = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        //x和y用于计算鼠标位置与玩家位置的距离
        float x = mousePos.x-transform.position.x;
        float y = mousePos.y-transform.position.y;
        
        if ((x * x + y * y) > 1f)
        {
            transform.up = faceDir;
        }
    }
   //设置速度
    public void SetVelocityX(float speed){
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    public void SetVelocityY(float speed){
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }
    public void SetVelocity(Vector2 dir){
        rb.velocity = dir;
    }
    

    private void Update() {
        //只需检查射击指令
        if(gameInput.leftClick){
            currentWeapen.SetActive(true);
        } else {
            currentWeapen.SetActive(false);
        }
    }
}
