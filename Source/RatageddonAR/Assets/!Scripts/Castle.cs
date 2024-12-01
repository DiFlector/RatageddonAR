using Enemy.Scripts;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{ 
    public Button PlaceButton => _placeButton;
    [SerializeField] private Button _placeButton;
    public EnemySpawner Spawner => _spawner;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] public int _hp;
    [SerializeField] private TMP_Text _hpText;

    public void ApplyDamage(int damage)
    {
        if (_hp - damage >= 0)
        {
            _hp -= damage;
            _hpText.text = "�������� �����: " + _hp;
            Debug.Log(_hp);
        }
        else
            Debug.Log("proigrali");
    }
}
