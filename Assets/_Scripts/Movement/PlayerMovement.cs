using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform body;

    [SerializeField] private float speed;
    [SerializeField] private float accelerations;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float distanceFromGround;

    private Camera mainCamera;
    private Rigidbody rigidbody;
    private RaycastHit hit;

    private float MIN_DISTANCE_FROM_TARGET = 0.12f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        InputController.Instance.OnJump(Jump);
    }

    private void Jump()
    {
        if (CanJump())
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
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
                rigidbody.MovePosition(rigidbody.position + transform.right * InputController.Instance.MoveKeyboardInput.x * Time.fixedDeltaTime);
            }
            if (InputController.Instance.MoveKeyboardInput.z != 0)
            {
                rigidbody.MovePosition(rigidbody.position + transform.forward * InputController.Instance.MoveKeyboardInput.z * Time.fixedDeltaTime);
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
