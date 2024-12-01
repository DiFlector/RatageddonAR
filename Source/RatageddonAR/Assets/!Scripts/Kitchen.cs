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

    [SerializeField] public int _hp;
    [SerializeField] private TMP_Text _hpText;

    private void Awake()
    {
        SpawnLuckyBox();
        _hpText.text = "Здоровье кухни: " + _hp;
    }

    private void SpawnLuckyBox()
    {
        _currentBox = Instantiate(_boxPrefab, _boxSpawn).GetComponent<LuckyBox>();
        _currentBox.OnExplode += () => Invoke(nameof(SpawnLuckyBox), 10);
    }

    public void GetDamage(int damage, DamageType damageType)
    {
        if (_hp - damage >= 0)
        {
            _hp -= damage;
            _hpText.text = "Здоровье кухни: " + _hp;
        }
        else
            Explode();
    }

    private void Explode()
    {
        Debug.Log("game over)");
    }
}
