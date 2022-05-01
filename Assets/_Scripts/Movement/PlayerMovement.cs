using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private Camera mainCamera;
    private Rigidbody rigidbody;
    private RaycastHit hit;

    private bool cooldownActive;

    private bool isPaused => PauseController.Instance.IsPaused;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        InputController.Instance.OnJump(Jump);
        InputController.Instance.OnDash(Dash);
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
            if (InputController.Instance.MoveKeyboardInput.x != 0)
            {
                rigidbody.AddForce(transform.right * InputController.Instance.MoveKeyboardInput.x * dashForce, ForceMode.Impulse);
            }
            if (InputController.Instance.MoveKeyboardInput.z != 0)
            {
                rigidbody.AddForce(transform.forward * InputController.Instance.MoveKeyboardInput.z * dashForce, ForceMode.Impulse);
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

        var ray = mainCamera.ScreenPointToRay(InputController.Instance.MousePosition);
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


            if(InputController.Instance.MoveKeyboardInput.x != 0)
            {
                rigidbody.MovePosition(rigidbody.position + transform.right
                                    * InputController.Instance.MoveKeyboardInput.x
                                    * moveSpeed * Time.fixedDeltaTime);
            }
            if (InputController.Instance.MoveKeyboardInput.z != 0)
            {
                rigidbody.MovePosition(rigidbody.position + transform.forward
                                    * InputController.Instance.MoveKeyboardInput.z 
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
