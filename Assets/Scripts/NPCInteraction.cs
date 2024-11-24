using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.AI; 

public class NPCInteraction : MonoBehaviour
{
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public string dialogMessage;
    public float interactionDistance = 3f;
    private bool isPlayerNearby = false;
    private bool isInteracting = false;
    public Transform[] waypoints; 
    public float dialogDisplayTime = 2f;
    public float waitTimeAtWaypoint = 2f;
    public float wanderRadius = 5f;
    public float wanderWaitTime = 3f;
    private int currentWaypointIndex = 0;
    private bool isMoving = true;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("Falta el componente NavMeshAgent.");
            enabled = false;
            return;
        }

        navMeshAgent.speed = 2f; 
        StartCoroutine(WanderOrMoveToWaypoints());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (isPlayerNearby && !isInteracting)
        {
            StartCoroutine(ShowDialog());
        }
    }

    private IEnumerator ShowDialog()
    {
        isInteracting = true;
        isMoving = false;

        navMeshAgent.ResetPath(); 
        dialogPanel.SetActive(true);
        dialogText.text = dialogMessage;

        yield return new WaitForSeconds(dialogDisplayTime);

        dialogPanel.SetActive(false);
        isMoving = true;
        isInteracting = false;
    }

    private IEnumerator WanderOrMoveToWaypoints()
    {
        while (true)
        {
            if (isMoving)
            {
                if (waypoints.Length > 0 && Random.value > 0.3f) 
                {
                    Transform targetWaypoint = waypoints[currentWaypointIndex];
                    navMeshAgent.SetDestination(targetWaypoint.position);
                    while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.5f)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(waitTimeAtWaypoint);
                    currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                }
                else
                {
                    Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
                    randomDirection += transform.position;
                    if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
                    {
                        navMeshAgent.SetDestination(hit.position);
                        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.5f)
                        {
                            yield return null;
                        }
                        yield return new WaitForSeconds(wanderWaitTime);
                    }
                }
            }

            yield return null; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialogPanel.SetActive(false);
        }
    }
}
