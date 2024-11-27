using System;

public interface ITask
{
    public string Text { get; }
    public void Initialize();
    public event Action OnTextUpdated;
    public event Action OnTaskCompleted;
}
