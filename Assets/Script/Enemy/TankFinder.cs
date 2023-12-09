using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class TankFinder : MonoBehaviour
{
    //基地
    protected HPIcon hpIcon;
    protected GameObject gb_hpIcon;
    //玩家
    public GameObject player;
    protected Seeker seeker;
    private TankController tankController;
    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        gb_hpIcon = GameObject.Find("HP Icon");
        hpIcon = gb_hpIcon.GetComponent<HPIcon>();
        player = GameObject.Find("Player");
        tankController = GetComponent<TankController>();
        //寻路完成后调用onPathComplete
        seeker.pathCallback += onPathComplete;
    }
    public void StartPathAgain()
    {
        seeker.StartPath(transform.position, hpIcon.GetRandomPosition());
    }
    public Vector3 GetTargetPosition()
    {
        return hpIcon.GetRandomPosition();
    }
    public Vector3 GetTargetPosition(bool noRandom)
    {
        return hpIcon.transform.position;
    }
    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }
    private void onPathComplete(Path p)
    {
        tankController.myPath = new List<Vector3>(p.vectorPath);
    }
    private void ClearPath(){
        tankController.myPath = null;
    }
}
