using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class Placable : MonoBehaviour
{
    private XRGrabInteractable _grab => GetComponent<XRGrabInteractable>();


    
}
