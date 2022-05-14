
using Break.Pause;
using System.Collections.Generic;
using Zenject;

public sealed class UIController : MonoSingleton<UIController>
{
    [Inject] private EventHolder eventHolder;
    [Inject] private IPauseService pauseService;

    private List<UIMenu> menus = new List<UIMenu>();

    private void Awake()
    {
        eventHolder.OnMenuOpenedEvent.Subscribe(() => pauseService.Pause());
        eventHolder.OnMenuClosedEvent.Subscribe(() => pauseService.Unpause());
    }

    public void Register(UIMenu menu)
    {
        menus.Add(menu);
    }

    /// <summary>
    /// Возвращает зарегистрированое меню. 
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

