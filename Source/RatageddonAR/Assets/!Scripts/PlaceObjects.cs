using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Zenject;

[RequireComponent(typeof(RoadGenerator))]
public class PlaceObjects : MonoBehaviour
{
    [Inject] private readonly GameManager _gameManager;
    [Inject] private readonly ViewManager _viewManager;

    private XRGrabInteractable _interactable;
    private RoadGenerator _roadGenerator => GetComponent<RoadGenerator>();

    public Kitchen Kitchen { get; private set; }
    public Castle Castle { get; private set; }

    [SerializeField] private GameObject _explotionFX;

    private void Awake()
    {
        FindFirstObjectByType<ObjectSpawner>().objectSpawned += HandleSpawnedObject;
    }


    private void HandleSpawnedObject(GameObject obj)
    {
        _interactable = obj.GetComponent<XRGrabInteractable>();
        if (_interactable.GetComponentInChildren<Kitchen>() != null)
        {
            Kitchen = _interactable.GetComponent<Kitchen>();
            Kitchen.PlaceButton.onClick.AddListener(() => ConfirmPlace(true));
        }
        if (_interactable.GetComponentInChildren<Castle>() != null)
        {
            Castle = _interactable.GetComponent<Castle>();
            Castle.PlaceButton.onClick.AddListener(() => ConfirmPlace(false));
        }

        _interactable.selectEntered.AddListener((e) => Take());
        _interactable.selectExited.AddListener((e) => Place());
    }

    private void Place()
    {
        if (Castle != null)
        {
            if (Vector3.Distance(Kitchen.transform.position, Castle.transform.position) > 1 &&
                Vector3.Distance(Kitchen.transform.position, Castle.transform.position) < 2)
            {
                Castle.PlaceButton.gameObject.SetActive(true);
                _interactable.transform.LookAt(Kitchen.transform.position);
            }
        }
        else if (Kitchen != null)
        {
            Kitchen.PlaceButton.gameObject.SetActive(true);
            _interactable.transform.LookAt(transform.position);
        }
        if (Physics.Raycast(_interactable.transform.position, Vector3.down, out RaycastHit hit, 10))
        {
            _interactable.transform.position = hit.point;
            Instantiate(_explotionFX, hit.point, Quaternion.identity);
        }
        _interactable.transform.eulerAngles = new Vector3(0, _interactable.transform.eulerAngles.y, 0);
    }

    private void Take()
    {
        if (Kitchen != null)
            Kitchen.PlaceButton.gameObject.SetActive(false);
        if (Castle != null)
            Castle.PlaceButton.gameObject.SetActive(false);
    }

    private void ConfirmPlace(bool isKitchen)
    {
        if (isKitchen)
        {
            Kitchen.PlaceButton.gameObject.SetActive(false);
            Kitchen.GetComponent<XRGrabInteractable>().enabled = false;
            _gameManager.GetTask<PreparationTask>().ConfirmKitchenPlacement();
            _viewManager.GetView<BattleView>().ConnectToCanon(Kitchen.Canon);
        }
        else
        {
            Castle.GetComponent<XRGrabInteractable>().enabled = false;
            Castle.PlaceButton.gameObject.SetActive(false);
            _gameManager.GetTask<PreparationTask>().ConfirmCastlePlacement();
            _roadGenerator.GenerateRoad(Castle.transform, Kitchen.transform);
            Kitchen.transform.LookAt(Castle.transform.position);
        }

    }
}
