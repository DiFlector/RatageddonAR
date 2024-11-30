using UnityEngine;

public class PickableObject : MonoBehaviour, IInteractable
{
    public virtual void Interact(Player player)
    {
        if (player.ItemInHand == null)
            player.TakeItem(this);
    }
}
