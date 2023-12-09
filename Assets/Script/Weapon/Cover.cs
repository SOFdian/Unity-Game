using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public float life;
    public float radius;
    public Vector3 scale;
    public float fullLife;
    public float percent;
    public CircleCollider2D circleCollider2D;
    public GameObject parent;
    private void Awake()
    {

        parent = transform.parent.gameObject;
        transform.position = parent.transform.position;
        if(parent.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        } else
        {
            this.gameObject.layer = LayerMask.NameToLayer("Player");
        }

        this.gameObject.SetActive(true);
        life = fullLife;
        scale = transform.localScale;
        circleCollider2D = GetComponent<CircleCollider2D>();
        radius = circleCollider2D.radius;
    }
    protected virtual void FixedUpdate()
    {
        transform.position = parent.transform.position;
        //慢慢回血
        if (life < fullLife)
        {
            life += 0.2f;
        }

        percent = life / fullLife;
        //根据percent改变物体的大小
        transform.localScale = scale * percent;
        circleCollider2D.radius = radius * percent;
        if (parent == null)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void Initialize()
    {
        this.gameObject.SetActive(true);
        transform.localScale = scale;
        life = fullLife;
        circleCollider2D.radius = radius;
    }
    public void ChangeLife(float damage)
    {
        life -= damage;
         percent = life / fullLife;
        if (percent <= 0.5f)
        {
            this.gameObject.SetActive(false);
         }
    }

}
