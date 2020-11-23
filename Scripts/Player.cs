using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;

    private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
    private bool[] inputs;

    Quaternion desRot;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;

        inputs = new bool[4];
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        Vector2 _inputDirection = Vector2.zero;
        if (inputs[0])
        {
            _inputDirection.y += 1;
        }
        if (inputs[1])
        {
            _inputDirection.y -= 1;
        }
        if (inputs[2])
        {
            _inputDirection.x -= 1;
        }
        if (inputs[3])
        {
            _inputDirection.x += 1;
        }

        //Rotation
        if (inputs[3] && !inputs[0])
        {
            desRot = Quaternion.Euler(0, 0, -90);
        }
        if (inputs[3] && inputs[0])
        {
            desRot = Quaternion.Euler(0, 0, -45);
        }
        if (!inputs[3] && inputs[0])
        {
            desRot = Quaternion.Euler(0, 0, 0);
        }
        if (inputs[2] && inputs[0])
        {
            desRot = Quaternion.Euler(0, 0, 45);
        }
        if (inputs[2] && !inputs[0])
        {
            desRot = Quaternion.Euler(0, 0, 90);
        }
        if (inputs[2] && inputs[1])
        {
            desRot = Quaternion.Euler(0, 0, 135);
        }
        if (!inputs[3] && inputs[1])
        {
            desRot = Quaternion.Euler(0, 0, 180);
        }
        if (inputs[3] && inputs[1])
        {
            desRot = Quaternion.Euler(0, 0, 225);
        }

        transform.rotation = desRot;
        //SetInput(inputs, desRot);
        Move(_inputDirection);
    }

    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move(Vector2 _inputDirection)
    {
        Vector3 _moveDirection = Vector2.right * _inputDirection.x + Vector2.up * _inputDirection.y;
        transform.position += _moveDirection * moveSpeed;
        //transform.rotation = desRot;

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    /// <summary>Updates the player input with newly received input.</summary>
    /// <param name="_inputs">The new key inputs.</param>
    /// <param name="_rotation">The new rotation.</param>
    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;
    }
}