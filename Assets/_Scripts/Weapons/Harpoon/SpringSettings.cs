
using UnityEngine;

public sealed class SpringSettings
{

    private float springForce;
    private float breakForce;
    private Rigidbody connectedBody;

    public float SpringForce => springForce;
    public float BreakForce => breakForce;
    public Rigidbody ConnectedBody => connectedBody;

    public SpringSettings(Rigidbody connectedBody, float springForce, float breakForce)
    {
        this.breakForce = breakForce;
        this.springForce = springForce;
        this.connectedBody = connectedBody;
    }
}

