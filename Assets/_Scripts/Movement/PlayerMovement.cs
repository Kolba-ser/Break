using Break.Pause;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform body;

    [Header("Speed")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    
    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float distanceFromGround;

    [Inject] private InputService inputService;
    [Inject] private IPauseService pauseService;

    private Camera mainCamera;
    private Rigidbody rigidbody;
    private RaycastHit hit;

    private bool cooldownActive;

    private bool isPaused => pauseService.IsPaused;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        inputService.OnJump(Jump);
        inputService.OnDash(Dash);
    }

    private void Jump()
    {
        if (!isPaused && CanJump())
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Dash()
    {
        if (!isPaused && !cooldownActive)
        {
            cooldownActive = true;
            if (inputService.MoveKeyboardInput.x != 0)
            {
                rigidbody.AddForce(transform.right * inputService.MoveKeyboardInput.x * dashForce, ForceMode.Impulse);
            }
            if (inputService.MoveKeyboardInput.z != 0)
            {
                rigidbody.AddForce(transform.forward * inputService.MoveKeyboardInput.z * dashForce, ForceMode.Impulse);
            }

            Observable.Timer(dashCooldown.InSec()).TakeUntilDisable(gameObject)
                .Finally(() => cooldownActive = false)
                .Subscribe();
        }
    }

    private void FixedUpdate()
    {
        if (isPaused)
            return;

        var ray = mainCamera.ScreenPointToRay(inputService.MousePosition);
        var up = rigidbody.rotation * Vectors.Up;

        var rigidbodyPosition = rigidbody.position;
        var plane = new Plane(up, rigidbodyPosition);

        if (plane.Raycast(ray, out var enter))
        {

            Physics.Raycast(body.position, -transform.up, out hit, distanceFromGround, Layers.Instance.Ground);

            var targetPoint = ray.GetPoint(enter);
            var directionToTarget = (targetPoint - rigidbodyPosition).normalized;

            var normalDirection = directionToTarget - Vector3.Dot(directionToTarget, hit.normal) * hit.normal;
            directionToTarget.y = normalDirection.y;


            if(inputService.MoveKeyboardInput.x != 0)
            {
                rigidbody.MovePosition(rigidbody.position + transform.right
                                    * inputService.MoveKeyboardInput.x
                                    * moveSpeed * Time.fixedDeltaTime);
            }
            if (inputService.MoveKeyboardInput.z != 0)
            {
                rigidbody.MovePosition(rigidbody.position + transform.forward
                                    * inputService.MoveKeyboardInput.z 
                                    * moveSpeed * Time.fixedDeltaTime);
            }

            rigidbody.rotation =
                Quaternion.RotateTowards
                (
                rigidbody.rotation,
                Quaternion.LookRotation(directionToTarget, Vectors.Up),
                rotationSpeed + Time.fixedDeltaTime
                );
        }
    }

    private bool CanJump()
    {
        return (rigidbody.position - hit.point).magnitude < distanceFromGround;
    }

}
