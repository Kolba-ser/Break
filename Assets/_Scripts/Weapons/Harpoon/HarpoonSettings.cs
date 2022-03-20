using UnityEngine;

[CreateAssetMenu(fileName ="WeaponSettings/Harpoon")]
public sealed class HarpoonSettings : ScriptableObject
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private LayerMask graspable;

    [Header("Rope settings")]
    [SerializeField] private float ropeDistance;
    [SerializeField] private float grappingForce;


    [Header("Spring Settings")]
    [SerializeField] private float springForce;
    [SerializeField] private float breakForce;

    private SpringSettings springSettings;

    public Projectile Projectile => projectile;
    public LayerMask Graspable => graspable;
    public SpringSettings SpringSettings => springSettings;

    public float RopeLength => ropeDistance;
    public float GrappingForce => grappingForce;
    
    public void InitSpring(Rigidbody connectedBody)
    {
        springSettings = new SpringSettings(connectedBody, springForce, breakForce);
    }
}

