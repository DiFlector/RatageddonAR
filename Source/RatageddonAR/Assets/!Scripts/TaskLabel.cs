using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TaskLabel : UIElement
{
    [SerializeField] private TMP_Text _taskText;
    [Inject] private readonly GameManager _gameManager;
    [Inject] private readonly Player _player;

    [SerializeField] private GameObject _confirmKitchenButton;
    [SerializeField] private GameObject _confirmCastleButton;


    public override void Initialize()
    {
        _confirmKitchenButton.SetActive(false);
        _confirmCastleButton.SetActive(false);
        _confirmKitchenButton.GetComponentInChildren<Button>().onClick.AddListener(ConfirmKitchen);
        _confirmCastleButton.GetComponentInChildren<Button>().onClick.AddListener(ConfirmCastle);
    }

    public void ShowKitchenButton() => _confirmKitchenButton.SetActive(true);
    public void HideKitchenButton() => _confirmKitchenButton.SetActive(false);

    public void ShowCastleButton() => _confirmCastleButton.SetActive(true);
    public void HideCastleButton() => _confirmCastleButton.SetActive(false);

    private void ConfirmKitchen()
    {
        _gameManager.GetTask<PreparationTask>().ConfirmKitchenPlacement();
        _player.PlaceObjects.PlaceObject();
        HideKitchenButton();
    }

    private void ConfirmCastle()
    {
        _gameManager.GetTask<PreparationTask>().ConfirmCastlePlacement();
        _player.PlaceObjects.PlaceObject();
        HideCastleButton();
    }

    public void SetText(string text)
    {
        _taskText.text = text;
    }
}
