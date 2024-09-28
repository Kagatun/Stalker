using System;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    public event Action<float, float> Rotated;

    public Vector2 MoveDirection { get; private set; }
    public bool IsJumped { get; private set; }

    private void Update()
    {
        float mouseX = Input.GetAxis(MouseX);
        float mouseY = Input.GetAxis(MouseY);
        Rotated?.Invoke(mouseX, mouseY);

        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);
        MoveDirection = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsJumped = true;
        }
        else
        {
            IsJumped = false;
        }
    }
}

