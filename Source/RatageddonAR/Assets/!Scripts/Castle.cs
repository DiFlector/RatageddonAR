using Enemy.Scripts;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{ 
    public Button PlaceButton => _placeButton;
    [SerializeField] private Button _placeButton;
    public EnemySpawner Spawner => _spawner;
    [SerializeField] private EnemySpawner _spawner;
}
