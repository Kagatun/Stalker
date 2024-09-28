using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoverPlayer : MonoBehaviour
{
    private CharacterController _characterController;
    private float _speed = 7f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 playerSpeed, Vector3 verticalVelocity)
    {
        playerSpeed *= _speed;

        if (_characterController.isGrounded)
            _characterController.Move((playerSpeed + verticalVelocity) * Time.deltaTime);
    }
}
