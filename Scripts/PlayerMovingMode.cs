using UnityEngine;

public abstract class PlayerMovingMode : PlayerMode {

    protected PlayerController playerController;

    public PlayerMovingMode(PlayerController playerController) {
        this.playerController = playerController;
    }

    public abstract float GetMoveSpeed();

    public void CheckForMovement() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = playerController.CalculateTargetAngle(direction.x, direction.z);
            float angle = playerController.CalculateAngle(targetAngle);
            playerController.Rotate(Quaternion.Euler(0f, angle, 0f));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerController.Move(moveDir.normalized * GetMoveSpeed() * Time.deltaTime);
        } else {
            playerController.SetMode(new PlayerIdleMode(playerController));
        }
    }

    public override void Update() {
        CheckForMovement();
    }

}