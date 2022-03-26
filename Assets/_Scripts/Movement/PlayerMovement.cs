using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float MIN_DISTANCE_FROM_TARGET = 0.1f;

    [SerializeField] private float speed;
    [SerializeField] private float accelerations;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float distanceFromGround;

    private Camera mainCamera;
    private Rigidbody rigidbody;
    private RaycastHit hit;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        var ray = mainCamera.ScreenPointToRay(InputController.Instance.MousePosition);
        var up = rigidbody.rotation * Vectors.Up;

        var rigidbodyPosition = rigidbody.position;
        var plane = new Plane(up, rigidbodyPosition);

        if (plane.Raycast(ray, out var enter))
        {

            Physics.Raycast(body.position, Vectors.Down, out hit, distanceFromGround, Layers.Instance.Ground);

            var targetPoint = ray.GetPoint(enter);
            var directionToTarget = (targetPoint - rigidbodyPosition).normalized;

            var normalDirection = directionToTarget - Vector3.Dot(directionToTarget, hit.normal) * hit.normal;
            directionToTarget.y = normalDirection.y;

            var movementSpeed = directionToTarget * speed * InputController.Instance.MoveInput;

            var currentDistanceFromGround = (body.position - hit.point).magnitude;

            if (currentDistanceFromGround < distanceFromGround)
            {
                rigidbody.position = new Vector3(rigidbodyPosition.x, rigidbodyPosition.y + distanceFromGround - currentDistanceFromGround, rigidbodyPosition.z);
            }

            var currentDistacneFormTarget = (rigidbody.position - targetPoint).magnitude;

            if (currentDistacneFormTarget > MIN_DISTANCE_FROM_TARGET)
            {
                rigidbody.MovePosition(rigidbody.position + movementSpeed * Time.fixedDeltaTime);
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
}
