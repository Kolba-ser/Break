using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class Graspable : Characteristic
{
    private Rigidbody rigidbody;

    private float impulse;
    private Direction direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Execute()
    {
        if (!IsPrepared)
            return;

        rigidbody.AddForce(direction.GetNormalized() * impulse, ForceMode.Impulse);
        TakeOff();
    }

    public void Join(Direction direction, float grappingForce)
    {
        this.direction = direction;

        impulse = grappingForce / rigidbody.mass;

        Prepare();
    }
}

