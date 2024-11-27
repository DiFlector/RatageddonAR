using System.Linq;

public class MainView : View
{
    private TaskLabel TaskLabel => _UIElements.FirstOrDefault(x => x is TaskLabel) as TaskLabel;

    public void UpdateTask(ITask task)
    {
        TaskLabel.SetText(task.Text);
    }
}
