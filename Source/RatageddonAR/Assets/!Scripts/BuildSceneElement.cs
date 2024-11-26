using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class BuildSceneElement : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        Debug.Log("SPAWNED");
        GetComponent<XRGrabInteractable>().selectExited.AddListener((e) =>
        {
            //GetComponent<XRGrabInteractable>().enabled = false;
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10))
            {
                Debug.Log("zoiba");
                transform.position = hit.point;
                transform.rotation = Quaternion.identity;
            }
        });
    }
}
