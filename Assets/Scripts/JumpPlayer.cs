using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    private float _jumpSpeed = 9f;

    public void Jump(bool isJumped, ref Vector3 verticalVelocity)
    {
        if (isJumped)
        {
            verticalVelocity = Vector3.up * _jumpSpeed;
        }
        else
        {
            verticalVelocity = Vector3.down;
        }
    }
}