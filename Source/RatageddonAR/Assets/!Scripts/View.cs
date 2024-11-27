using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class View : MonoBehaviour
{
    [Inject] protected ViewManager _viewManager;
    [SerializeField] protected List<UIElement> _UIElements;

    public T TryGetUIElement<T>() where T : UIElement
    {
        foreach (var element in _UIElements)
        {
            if (element is T)
                return element as T;
        }
        return null;
    }
    public virtual void Initialize()
    {
        foreach (UIElement element in _UIElements)
        {
            element.Initialize();
        }
    }
    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Hide() => gameObject.SetActive(false);
}
