

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory/AnswerButtonFactory")]
public sealed class AnswerButtonFactory : PoolFactory
{
    [SerializeField] private AnswerButton button;

    public override Type ProductType => typeof(AnswerButton);

    public override bool TryCreate(out IPooledObject pooledObject, Transform parent = null)
    {
        pooledObject = null;

        if (verified || Verify(button.transform))
        {
            pooledObject = Instantiate(button, parent);
            return true;
        }

        return false;
    }
}

