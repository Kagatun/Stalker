using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private OverviewPlayer _overviewPlayer;
    [SerializeField] private MoverPlayer _moverPlayer;
    [SerializeField] private JumpPlayer _jumpPlayer;
    [SerializeField] private LayerMask _groundLayers;

    private Transform _transform;
    private CharacterController _characterController;
    private Vector3 _playerSpeed;
    private Vector3 _verticalVelocity;
    private float _gravityFactor = 2f;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_characterController.isGrounded)
        {
            _jumpPlayer.Jump(_inputDetector.IsJumped, ref _verticalVelocity);
        }
        else
        {
            PinToGround();
        }

        Move(_inputDetector.MoveDirection.x, _inputDetector.MoveDirection.y);
    }

    private void OnEnable()
    {
        _inputDetector.Rotated += OnRotateCamera;
    }

    private void OnDisable()
    {
        _inputDetector.Rotated -= OnRotateCamera;
    }

    private void PinToGround()
    {
        Vector3 horizontalVelocity = _characterController.velocity;
        horizontalVelocity.y = 0;
        _verticalVelocity += Physics.gravity * Time.deltaTime * _gravityFactor;
        _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
    }

    private void OnRotateCamera(float mouseX, float mouseY) =>
        _overviewPlayer.RotateCamera(mouseX, mouseY);

    private void Move(float horizontal, float vertical)
    {
        Vector3 forward = _overviewPlayer.transform.forward;
        Vector3 right = _overviewPlayer.transform.right;

        _playerSpeed = forward * vertical + right * horizontal;
        _playerSpeed = GetNormalDirection();

        _moverPlayer.Move(_playerSpeed, _verticalVelocity);
    }

    private Vector3 GetNormalDirection()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _groundLayers))
        {
            Vector3 normal = hit.normal;
            Vector3 receivedDirection = Vector3.ProjectOnPlane(_playerSpeed, normal).normalized;

            return receivedDirection;
        }

        return Vector3.zero;
    }
}

