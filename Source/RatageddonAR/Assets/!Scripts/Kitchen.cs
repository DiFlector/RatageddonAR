using Enemy.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour, IDamageable
{
    public Button PlaceButton => _placeButton;
    [SerializeField] private Button _placeButton;
    public Canon Canon => _canon;
    [SerializeField] private Canon _canon;

    [SerializeField] private GameObject _boxPrefab;
    private LuckyBox _currentBox;
    [SerializeField] private Transform _boxSpawn;
    public Pan Pan => _pan;
    [SerializeField] private Pan _pan;

    [SerializeField] private int _hp;
    [SerializeField] private TMP_Text _hpText;

    private void Awake()
    {
        SpawnLuckyBox();
        _hpText.text = "��������� �����: " + _hp;
    }

    private void SpawnLuckyBox()
    {
        _currentBox = Instantiate(_boxPrefab, _boxSpawn).GetComponent<LuckyBox>();
        _currentBox.OnExplode += SpawnLuckyBox;
    }

    public void GetDamage(int damage)
    {
        if (_hp - damage >= 0)
        {
            _hp -= damage;
            _hpText.text = "��������� �����: " + _hp;
        }
        else
            Explode();
    }

    private void Explode()
    {
        Debug.Log("game over)");
    }
}
