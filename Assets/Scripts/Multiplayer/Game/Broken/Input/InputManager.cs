using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    PhotonView view;

    void Awake()
    {
        view = GetComponent<PhotonView>();
        
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        if (view.IsMine)
        {
            motor = GetComponent<PlayerMotor>();
            look = GetComponent<PlayerLook>();

            onFoot.Jump.performed += ctx => motor.Jump();
            onFoot.Pause.performed += ctx => motor.Pause();
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            // tell PlayerMotor to move using movement action val
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }
    }

    private void LateUpdate()
    {
        if (view.IsMine)
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
