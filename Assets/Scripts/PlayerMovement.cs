using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;

    [SerializeField] private NavMeshAgent navMeshAgent;
    private Vector2 moveInput;
    private Camera mainCamera; 

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("Falta el componente NavMeshAgent.");
            enabled = false;
        }
        else
        {
            navMeshAgent.speed = playerSpeed;
        }

        mainCamera = Camera.main;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (moveInput.sqrMagnitude > 0.1f)
        {
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraRight.y = 0;

            Vector3 worldDirection = (cameraForward.normalized * move.z + cameraRight.normalized * move.x).normalized;
            Vector3 destination = transform.position + worldDirection;
            navMeshAgent.SetDestination(destination);
        }
        else
        {
            navMeshAgent.ResetPath();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
