using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Zenject;

public class Kitchen : MonoBehaviour
{
    public Button PlaceButton => _placeButton;
    [SerializeField] private Button _placeButton;
}
