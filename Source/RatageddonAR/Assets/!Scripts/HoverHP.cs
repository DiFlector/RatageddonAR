using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HoverHP : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    private Transform _player;
    private void Awake()
    {
        _player = FindAnyObjectByType<Player>().transform;
        _canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_player.transform.position);
    }
}
