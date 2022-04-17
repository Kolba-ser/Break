
using Unity.Assertions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class MainMenuView : MonoBehaviour
{

    public event UnityAction Play
    {
        add
        {
            Assert.IsNotNull(playButton, "Play button is not defined");
            playButton.onClick.AddListener(value);
        }
        remove
        {
        
            Assert.IsNotNull(playButton, "Play button is not defined");
            playButton.onClick.RemoveListener(value);
        }
    }


    [SerializeField] private Button playButton;
}

