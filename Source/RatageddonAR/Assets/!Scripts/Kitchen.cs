using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    public Button PlaceButton => _placeButton;
    [SerializeField] private Button _placeButton;
    public Canon Canon => _canon;
    [SerializeField] private Canon _canon;

    [SerializeField] private GameObject _boxPrefab;
    private LuckyBox _currentBox;
    [SerializeField] private Transform _boxSpawn;

    private void Awake()
    {
        SpawnLuckyBox();
    }

    private void SpawnLuckyBox()
    {
        _currentBox = Instantiate(_boxPrefab, _boxSpawn).GetComponent<LuckyBox>();
        _currentBox.OnExplode += SpawnLuckyBox;
    }
}
