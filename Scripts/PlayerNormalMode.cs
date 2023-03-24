using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalMode : Mode
{

    PlayerController playerController;

    public PlayerNormalMode(PlayerController playerController)
    {
        this.playerController = playerController;
        playerController.NullifyToolForFarming();
        playerController.NullifyFarmingTarget();
    }

    public override void Update()
    {
        playerController.CheckForMovement();
        playerController.CheckForInteractions();

        //if (stamina < 100f && Time.time > nextStaminaRegeneration)
        //{
        //    stamina += staminaRegenrationRate;
        //    if (stamina > 100f)
        //    {
        //        stamina = 100f;
        //    }
        //    nextStaminaRegeneration = Time.time + 1f;
        //}
        //if (stamina == 100f)
        //{
        //    nextStaminaRegeneration = 0f;
        //}
    }

    public override void FixedUpdate()
    {
        playerController.SearchForNearInteractables();
    }

}
