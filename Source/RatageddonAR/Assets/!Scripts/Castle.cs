using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{ 
    public Button PlaceButton => _placeButton;
    [SerializeField] private Button _placeButton;
}
