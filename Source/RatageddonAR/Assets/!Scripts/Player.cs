using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(PlaceObjects))]
public class Player : MonoBehaviour
{
    [Inject] private readonly ViewManager _viewManager;
    public PlaceObjects PlaceObjects => GetComponent<PlaceObjects>();
    [SerializeField] private InputAction _input;
    public Transform ItemAnchor => _anchor;
    [SerializeField] private Transform _anchor;

    [SerializeField] private List<Projectile> _ingredientsPrefabs;

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
        if (ItemInHand) return;
        int randIndex = Random.Range(0, _ingredientsPrefabs.Count);
        Projectile proj = Instantiate(_ingredientsPrefabs[randIndex].gameObject, _anchor).GetComponent<Projectile>();
        proj.Init(this);
        ItemInHand = proj;
    }

    public void ToggleJoystick(bool show)
    {
        _viewManager.GetView<BattleView>().ShowJoystick(show);
    }

    private IEnumerator AnimatePickup(Transform item)
    {
        float duration = 0.4f;
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
