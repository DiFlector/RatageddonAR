using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

[CreateAssetMenu(menuName = "Tasks/PreparationTask")]
public class PreparationTask : ScriptableObject, ITask
{
    public event Action OnTextUpdated;
    public event Action OnTaskCompleted;

    public string Text { get; private set; }
    [SerializeField, TextArea(1, 10)] private string _textForKitchen;
    [SerializeField, TextArea(1, 10)] private string _textForCastle;
    private ObjectSpawner _spawner;

    public void Initialize()
    {
        Text = _textForKitchen;
        _spawner = FindAnyObjectByType<ObjectSpawner>();
        _spawner.spawnOptionIndex = 0;
        _spawner.objectSpawned += (e) => { _spawner.gameObject.SetActive(false);  Debug.Log("PIZDA"); };
    }

    public void ConfirmKitchenPlacement()
    {
        _spawner.gameObject.SetActive(true);
        _spawner.spawnOptionIndex = 1;
        Text = _textForCastle;
        OnTextUpdated?.Invoke();
    }

    public void ConfirmCastlePlacement()
    {
        OnTaskCompleted?.Invoke();
    }
}
