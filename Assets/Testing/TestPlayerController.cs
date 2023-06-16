using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class TestPlayerController : MonoBehaviour, TestPlayerControls.IPlayerActions
{
    public float speed = 10f;

    private Vector3 inputVal = Vector3.zero;
    private float inputMagnitude;
    private TestPlayerControls controls;

    private void OnEnable()
    {
        controls = new TestPlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        Step();
    }

    private void Step()
    {
        inputMagnitude = inputVal.sqrMagnitude;
        if (inputMagnitude >= 0.01f)
        {
            Vector3 newPos = transform.position + inputVal * Time.deltaTime * speed;
            NavMeshHit hit;
            bool isValid = NavMesh.SamplePosition(newPos, out hit, 0.3f, NavMesh.AllAreas);
            if (isValid)
            {
                if ((transform.position - hit.position).magnitude >= 0.02f)
                {
                    transform.position = hit.position;
                }
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input2D = context.ReadValue<Vector2>();
        inputVal.x = input2D.x;
        inputVal.z = input2D.y;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //looking
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //jumping
    }
}
