using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLaserGun : Item
{
    public GameObject laserGun;
    public PlayerController playerController;
    protected override void Awake()
    {
        base.Awake();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }
    public override void run()
    {
        playerController.currentWeapen.SetActive(false);
        playerController.currentWeapen = playerController.weapens[1];

    }
}
