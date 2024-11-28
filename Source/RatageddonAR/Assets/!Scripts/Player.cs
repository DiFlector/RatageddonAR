using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlaceObjects))]
public class Player : MonoBehaviour
{
    public PlaceObjects PlaceObjects => GetComponent<PlaceObjects>();
    [SerializeField] private InputAction _input;
    public Transform ItemAnchor => _anchor;
    [SerializeField] private Transform _anchor;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Input.gyro.enabled = true;
        _input.Enable();
        _input.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("INTERACT");
        Ray ray = Camera.main.ScreenPointToRay(_input.ReadValue<Vector2>());
        Physics.Raycast(ray, out RaycastHit hit, 3);
        if (hit.collider != null)
        {
            Debug.Log("HIT");
            hit.collider.GetComponent<IInteractable>()?.Interact(this);
        }
    }
}
