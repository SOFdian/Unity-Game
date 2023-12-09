using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class Level : ScriptableObject
{
    //���������õ��������ģ������ۺϿ��Ǿ���ͨ��interval����
    public float interval = 1;
    public List<int> numOfEnemies = new List<int>();
    public List<GameObject> enemies = new List<GameObject>();
    
}
