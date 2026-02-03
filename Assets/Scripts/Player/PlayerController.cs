using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveForce = 5f;
    [SerializeField] private float VelocidadMaxima = 10f;

    private Rigidbody _rigidBody;

    private void Awake()
    {
            _rigidBody = GetComponent<Rigidbody>();
        
        
    }

    private void Update()
    {
        if (InputManager.Actions.Player.Move.IsPressed())
        {
            MoveBall();
        }
        
    }

    public void MoveBall()
    {
        _rigidBody.AddForce(InputManager.Actions.Player.Move.ReadValue<Vector3>().normalized * MoveForce * Time.deltaTime, ForceMode.Impulse);

        if (Mathf.Abs( _rigidBody.velocity.x) > VelocidadMaxima)
        {
            if(_rigidBody.velocity.x > 0) _rigidBody.velocity = new Vector3(VelocidadMaxima,0, _rigidBody.velocity.z).normalized;
            else _rigidBody.velocity = new Vector3(-VelocidadMaxima, 0, _rigidBody.velocity.z).normalized;
        }
        if (Mathf.Abs(_rigidBody.velocity.z) > VelocidadMaxima)
        {
            if (_rigidBody.velocity.x > 0) _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, VelocidadMaxima).normalized;
            else _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, -VelocidadMaxima).normalized;
        }
    }

}
