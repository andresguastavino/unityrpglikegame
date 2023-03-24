public interface IInteractable
{
    public void InteractWith(PlayerController other);
    public bool CanInteractWith(PlayerController other);
}
