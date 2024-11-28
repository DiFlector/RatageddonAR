using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TaskLabel : UIElement
{
    [SerializeField] private TMP_Text _taskText;

    public void SetText(string text)
    {
        _taskText.text = text;
    }
}
