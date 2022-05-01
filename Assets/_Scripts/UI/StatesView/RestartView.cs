using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class RestartView : MonoBehaviour
{

    public event UnityAction Restart
    {
        add
        {
            Assert.IsNotNull(restartButton, "Main menu button is not defined");
            restartButton.onClick.AddListener(value);
        }
        remove
        {

            Assert.IsNotNull(restartButton, "Main menu button is not defined");
            restartButton.onClick.RemoveListener(value);
        }
    }


    [SerializeField] private Button restartButton;
}

