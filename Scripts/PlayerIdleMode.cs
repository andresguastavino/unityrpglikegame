using UnityEngine;

public class PlayerIdleMode : PlayerMode {

    PlayerController playerController;

    public PlayerIdleMode(PlayerController playerController) {
        this.playerController = playerController;
    }

    public override void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            if (playerController.IsRunning()) {
                playerController.SetMode(new PlayerRunningMode(playerController));
            } else {
                playerController.SetMode(new PlayerWalkingMode(playerController));
            }
        }
    }

}