using UnityEngine;

public class PlayerRunningMode : PlayerMovingMode {

    public PlayerRunningMode(PlayerController playerController) : base(playerController) { }

    public override float GetMoveSpeed() { 
        return playerController.stats.runSpeed;
    }

    public override void Update() {
        if (!playerController.IsRunning()) {
            playerController.SetMode(new PlayerWalkingMode(playerController));
        }
        base.Update();
        playerController.ChargeStaminaCost();
    }

}