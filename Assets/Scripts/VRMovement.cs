using UnityEngine;
using Valve.VR;

public class VRMovement : MonoBehaviour
{
    public float speed = 3f;
    public float smoothTime = 0.1f;
    public SteamVR_Action_Vector2 moveAction;
    public Transform headTransform;
    public Transform orientationTransform; // Трансформ для ориентации движения (обычно это контроллер)

    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 currentVelocity;
    private Vector2 smoothedAxis;
    private Vector2 currentAxisVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        if (headTransform == null)
        {
            headTransform = Camera.main.transform;
        }

        // Если orientationTransform не назначен, используем направление камеры
        if (orientationTransform == null)
        {
            orientationTransform = headTransform;
        }
    }

    private void Update()
    {
        // Получаем входные данные со стика
        Vector2 trackpadValue = moveAction.axis;

        // Сглаживаем ввод стика
        smoothedAxis = Vector2.SmoothDamp(
            smoothedAxis,
            trackpadValue,
            ref currentAxisVelocity,
            smoothTime
        );

        // Получаем направление "вперед" и "вправо" из orientationTransform
        Vector3 forward = Vector3.ProjectOnPlane(orientationTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(orientationTransform.right, Vector3.up).normalized;

        // Создаем вектор движения, комбинируя forward и right направления
        Vector3 targetMoveDirection = (forward * smoothedAxis.y + right * smoothedAxis.x);

        // Сглаживаем само движение
        moveDirection = Vector3.SmoothDamp(
            moveDirection,
            targetMoveDirection,
            ref currentVelocity,
            smoothTime
        );

        // Применяем движение
        if (moveDirection.magnitude > 0.01f)
        {
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
    }
}