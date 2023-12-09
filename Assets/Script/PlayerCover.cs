using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : Cover
{
    private void Awake() {
        this.gameObject.SetActive(false);
        life = fullLife;
        scale = transform.localScale;
        circleCollider2D = GetComponent<CircleCollider2D>();
        radius = circleCollider2D.radius;
    }
    protected override void FixedUpdate() {
        if(this.gameObject.activeSelf==false&&life>=fullLife){
            this.gameObject.SetActive(true);
        }
        transform.position = transform.parent.transform.position;
        //慢慢回血
        if (life < fullLife)
        {
            life += 0.2f;
        }

    }

}
