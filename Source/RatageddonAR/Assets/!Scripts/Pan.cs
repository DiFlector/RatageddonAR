using System;
using UnityEngine;

public class Pan : MonoBehaviour, IInteractable
{
    public event Action OnMinigameStart;
    public void Interact(Player player)
    {
        OnMinigameStart?.Invoke();
    }
}
