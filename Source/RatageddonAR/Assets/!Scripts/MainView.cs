using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;

    public override void Initialize()
    {
        _hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
        _clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
