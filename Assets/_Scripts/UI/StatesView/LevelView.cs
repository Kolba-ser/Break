using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class LevelView : MonoBehaviour
{

    public event UnityAction Play
    {
        add
        {
            Assert.IsNotNull(mainMenuButton, "Main menu button is not defined");
            mainMenuButton.onClick.AddListener(value);
        }
        remove
        {
        
            Assert.IsNotNull(mainMenuButton, "Main menu button is not defined");
            mainMenuButton.onClick.RemoveListener(value);
        }
    }


    [SerializeField] private Button mainMenuButton;
}

