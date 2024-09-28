using UnityEngine;

public class OverviewPlayer : MonoBehaviour
{
    [SerializeField] private Transform _transformCamera;

    private float _horizontalTurnSensitivity = 7f;
    private float _verticalTurnSensitivity = 10f;
    private float _verticalMinAngle = -89f;
    private float _verticalMaxAngle = 89f;
    private float _cameraAngle = 0;

    private void Awake()
    {
        _cameraAngle = _transformCamera.localEulerAngles.x;
    }

    public void RotateCamera(float mouseX, float mouseY)
    {
        _cameraAngle -= mouseY * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _transformCamera.localEulerAngles = Vector3.right * _cameraAngle;

        transform.Rotate(Vector3.up * _horizontalTurnSensitivity * mouseX);
    }
}
