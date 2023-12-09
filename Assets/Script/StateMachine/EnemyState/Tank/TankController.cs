using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    private GameObject tower;
    public Rigidbody2D rb;
    //用来判断是否和同类碰撞
    public GameObject tankInitial;
    //存储路径
    public List<Vector3> myPath;

    void Awake()
    {
        //transform.Find()只能获取一级子物体
        tower = transform.Find("Tower").gameObject;
        rb = GetComponent<Rigidbody2D>();
        //一旦启用就会不停的发射子弹
        tankInitial.SetActive(false);
    }
    public Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }
    public void ChangeFace(Vector2 dir){
        //这里应该有优化空间
        transform.up = -dir;
    }
    //武器转向
    public virtual void Aim(Vector2 dir){
        tower.transform.up = dir;
    }
    public void SetVelocity(Vector2 dir)
    {
        rb.velocity = dir;
    }
    public void SetPosition(Vector2 dir)
    {
        transform.position = dir;
    }
}
