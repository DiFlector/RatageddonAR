using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private readonly ViewManager _viewManager;
    [SerializeField] private List<ScriptableObject> _tasks;
    private int _taskIndex;

    private ITask _currentTask;

    private void Awake()
    {
        foreach (var task in _tasks)
        {
            if (task is ITask itask)
            {
                itask.Initialize();
                itask.OnTaskCompleted += () => SetTask(++_taskIndex);
            }
        }
        SetTask(_taskIndex);
    }

    public void SetTask(int taskindex)
    {
        _currentTask = _tasks[taskindex] as ITask;
        _currentTask.OnTextUpdated += () => _viewManager.GetView<MainView>().UpdateTask(_currentTask);
        _viewManager.GetView<MainView>().UpdateTask(_currentTask);
    }

    public T GetTask<T>() where T : ITask
    {
        foreach (var task in _tasks)
        {
            if (task is T targetTask)
                return targetTask;
        }
        return default;
    }
}
