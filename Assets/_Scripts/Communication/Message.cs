using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour, IPooledObject
{
    [SerializeField] private HorizontalLayoutGroup layoutGroup;
    [Space(20)]
    [SerializeField] private Image avatar;
    [SerializeField] private Image imageContent;
    [SerializeField] private TextMeshProUGUI messageContent;


    public void SetParameters(ContentSide mode, string text, Sprite sprite = null)
    {
        SetMode(mode);
        SetText(text);
        SetSprite(sprite);
    }

    private void SetMode(ContentSide mode)
    {
        switch (mode)
        {
            case ContentSide.Right:
                layoutGroup.childAlignment = TextAnchor.UpperRight;
                layoutGroup.reverseArrangement = false;
                break;
            case ContentSide.Left:
                layoutGroup.childAlignment = TextAnchor.UpperLeft;
                layoutGroup.reverseArrangement = true;
                break;
        }

    }
    private void SetSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            imageContent.sprite = sprite;
            imageContent.gameObject.SetActive(true);
            return;
        }

        imageContent.gameObject.SetActive(false);
    }

    private void SetText(string text)
    {
        if (!text.IsEmpty())
        {
            messageContent.gameObject.SetActive(true);
            messageContent.text = text;
            return;
        }
        messageContent.gameObject.SetActive(false);
    }

    public void OnPullIn()
    {
        messageContent.text = "";
        messageContent.gameObject.SetActive(false);
        imageContent.sprite = null;
        imageContent.gameObject.SetActive(false);
    }

    public void OnPullOut()
    {
    }
}
public enum ContentSide
{
    Right,
    Left
}

