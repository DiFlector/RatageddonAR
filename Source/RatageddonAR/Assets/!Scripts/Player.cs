using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlaceObjects))]
public class Player : MonoBehaviour
{
    public PlaceObjects PlaceObjects => GetComponent<PlaceObjects>();
    [SerializeField] private InputAction _input;
    public Transform ItemAnchor => _anchor;
    [SerializeField] private Transform _anchor;

    [SerializeField] private List<Projectile> _ingredientsPrefabs;
    public Button Babax;

    public PickableObject ItemInHand { get; private set; } = null;

    private void Awake()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 200;
#else
        Application.targetFrameRate = 60;
#endif
        Input.gyro.enabled = true;
        _input.Enable();
        _input.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(_input.ReadValue<Vector2>());
        Physics.Raycast(ray, out RaycastHit hit, 3);
        if (hit.collider != null)
            hit.collider.GetComponent<IInteractable>()?.Interact(this);
    }

    public void ClearItem()
    {
        ItemInHand = null;
    }

    public void TakeItem(PickableObject item)
    {
        Debug.Log("MOVE");
        ItemInHand = item;
        StartCoroutine(AnimatePickup(item.transform));
    }

    public void GetRandomIngredient()
    {
        int randIndex = Random.Range(0, _ingredientsPrefabs.Count);
        ItemInHand = Instantiate(_ingredientsPrefabs[randIndex].gameObject, _anchor).GetComponent<PickableObject>();
    }

    private IEnumerator AnimatePickup(Transform item)
    {
        float duration = 1;
        float timeElapsed = 0;
        Vector3 startPos = item.position;
        while (timeElapsed / duration < 1)
        {
            item.position = Vector3.Lerp(startPos, _anchor.position, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        item.SetParent(_anchor);
    }
}
