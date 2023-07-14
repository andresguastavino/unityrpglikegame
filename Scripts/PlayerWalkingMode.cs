using UnityEngine;

public class PlayerWalkingMode : PlayerMovingMode {

    public PlayerWalkingMode(PlayerController playerController) : base(playerController) { }

    public override float GetMoveSpeed() { 
        return playerController.stats.walkSpeed;
    }

    public override void Update() {
        if (playerController.IsRunning()) {
            playerController.SetMode(new PlayerRunningMode(playerController));
        }
        base.Update();
    }

}