using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmingMode : Mode
{

    PlayerController playerController;

    public PlayerFarmingMode(PlayerController playerController)
    {
        this.playerController = playerController;
        playerController.SelectToolForFarming();
    }

    public override void Update()
    {
        playerController.CheckForExitMode();
        playerController.FarmResource();
    }


}
