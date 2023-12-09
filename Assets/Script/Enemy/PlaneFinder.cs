using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PlaneFinder : TankFinder
{
    private PlaneController planeController;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        gb_hpIcon = GameObject.Find("HP Icon");

        hpIcon = gb_hpIcon.GetComponent<HPIcon>();
        player = GameObject.Find("Player");
        planeController = GetComponent<PlaneController>();
        //求路径放到了Move状态中
        //seeker.StartPath(transform.position, hpIcon.GetRandomPosition(), onPathComplete);
        seeker.pathCallback += onPathComplete;
    }
    public void FindPlayer()
    {
        seeker.StartPath(transform.position, player.transform.position, onPathComplete);
        
    }
    private void onPathComplete(Path p)
    {
        planeController.myPath = new List<Vector3>(p.vectorPath);
    }
    private void ClearPath()
    {
        planeController.myPath = null;
    }

}
