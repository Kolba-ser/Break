
using System.Collections.Generic;

public sealed class UIController : MonoSingleton<UIController>
{
    private List<UIMenu> menus = new List<UIMenu>();

    private void Awake()
    {
        EventHolder.Instance.OnMenuOpenedEvent.Subscribe(() => PauseController.Instance.Pause());
        EventHolder.Instance.OnMenuClosedEvent.Subscribe(() => PauseController.Instance.Unpause());
    }

    public void Register(UIMenu menu)
    {
        menus.Add(menu);
    }
   
    /// <summary>
    /// Возвращает меню если оно было зарегистрировано
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="menu"></param>
    /// <returns></returns>
    public bool TryGet<T>(out T menu) where T : UIMenu
    {
        menu = null;
        var targetType = typeof(T);

        var targetMenu = menus.Find(_ => targetType == _.Type);
        if(targetMenu != null)
        {
            menu = targetMenu as T;
            return true;
        }

        return false;
    }
}

