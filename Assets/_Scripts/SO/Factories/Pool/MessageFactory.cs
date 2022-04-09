
using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Factory/MessageFactory")]
public class MessageFactory : PoolFactory
{
    [SerializeField] private Message message; 

    public override Type ProductType => typeof(Message);

    public override bool TryCreate(out IPooledObject pooledObject, Transform parent = null)
    {
        pooledObject = null;

        if (verified || Verify(message.transform))
        {
            pooledObject = Instantiate(message, parent);
            return true;
        }

        return false;
    }
}

