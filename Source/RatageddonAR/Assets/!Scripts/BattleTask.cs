using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Tasks/BattleTask")]
public class BattleTask : ScriptableObject, ITask
{
    public string Text { get; private set; }
    [SerializeField] private string _text;

    public event Action OnTextUpdated;
    public event Action OnTaskCompleted;

    public void Initialize()
    {
        Text = _text;
    }
}
