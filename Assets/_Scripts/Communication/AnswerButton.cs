using Break.Communication;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class AnswerButton : MonoBehaviour, IPooledObject
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button button;

    private Action<string, int, bool, int> callback;

    public void OnPullIn()
    {
        if (callback != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }
    public void OnPullOut()
    {
    }

    public void SetParameters(Answer answer, Action<string, int, bool, int> callback)
    {
        if (this.callback != null)
        {
            button.onClick.RemoveAllListeners();
        }

        textMesh.text = answer.Text;
        button.onClick.AddListener(() => callback(answer.Text, answer.NextNode, answer.IsEnd, answer.spriteIndex));
        this.callback = callback;
    }
}

