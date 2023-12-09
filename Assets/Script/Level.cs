using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class Level : ScriptableObject
{
    //本来想设置敌人数量的，但是综合考虑决定通过interval控制
    public float interval = 1;
    public List<int> numOfEnemies = new List<int>();
    public List<GameObject> enemies = new List<GameObject>();
    
}
