using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPIcon : MonoBehaviour
{
    public Image bloodStrip;
    public float fullLife;
    public float life;
    public float percent;
    public float radius;
    private void Awake() {
        life = fullLife;
    }
    public Vector3 GetRandomPosition()
    {
        Vector3 pos = transform.position;
        pos.x += Random.Range(-radius, radius);
        pos.y += Random.Range(-radius, radius);
        return pos;
    }
    public void ChangeLife(float damage){
        life -= damage;
        if(life<=0){
            SceneManager.LoadScene("MyWork");
        }
    }
    
    private void Update() {
        bloodStrip.fillAmount = life / fullLife;
    }
}
