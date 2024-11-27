using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Zenject;

public class PlaceObjects : MonoBehaviour
{
    [Inject] private readonly ViewManager _viewManager;

    private XRGrabInteractable _interactable;
    private Transform _kitchen;
    private Transform _castle;

    private void Awake()
    {
        FindFirstObjectByType<ObjectSpawner>().objectSpawned += HandleSpawnedObject;
    }

    private void HandleSpawnedObject(GameObject obj)
    {
        _interactable = obj.GetComponent<XRGrabInteractable>();
        if (_interactable.GetComponentInChildren<Kitchen>() != null)
            _kitchen = _interactable.transform;
        if (_interactable.GetComponentInChildren<Castle>() != null)
            _castle = _interactable.transform;
        _interactable.hoverEntered.AddListener((e) => Take());
        _interactable.hoverExited.AddListener((e) => Place());
    }

    private void Place()
    {
        if (_castle != null)
        {
            if (Vector3.Distance(_kitchen.position, _castle.position) > 1 &&
                Vector3.Distance(_kitchen.position, _castle.position) < 2)
            {
                
                if (_interactable.GetComponentInChildren<Castle>() != null)
                    _viewManager.GetView<MainView>().TryGetUIElement<TaskLabel>().ShowCastleButton();
            }
        }
        if (_interactable.GetComponentInChildren<Kitchen>() != null)
            _viewManager.GetView<MainView>().TryGetUIElement<TaskLabel>().ShowKitchenButton();
        if (Physics.Raycast(_interactable.transform.position, Vector3.down, out RaycastHit hit, 10))
            _interactable.transform.position = hit.point;
        _interactable.transform.rotation = Quaternion.identity;
    }

    private void Take()
    {
        if (_interactable.GetComponentInChildren<Kitchen>() != null)
            _viewManager.GetView<MainView>().TryGetUIElement<TaskLabel>().HideKitchenButton();
        if (_interactable.GetComponentInChildren<Castle>() != null)
            _viewManager.GetView<MainView>().TryGetUIElement<TaskLabel>().HideCastleButton();
    }

    public void PlaceObject()
    {
        if (_interactable.GetComponentInChildren<Kitchen>() != null)
            _viewManager.GetView<MainView>().TryGetUIElement<TaskLabel>().HideKitchenButton();
        if (_interactable.GetComponentInChildren<Castle>() != null)
            _viewManager.GetView<MainView>().TryGetUIElement<TaskLabel>().HideCastleButton();

        _interactable.enabled = false;
    }
}
