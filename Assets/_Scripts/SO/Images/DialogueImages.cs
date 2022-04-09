using UnityEngine;

[CreateAssetMenu(fileName ="Images/Dialogue")]
public sealed class DialogueImages : Images
{
    [SerializeField] private Sprite[] sprites;

    public override Sprite[] Sprites => sprites;
}

