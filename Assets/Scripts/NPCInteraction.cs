using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    public GameObject dialogPanel; 
    public TMP_Text dialogText;        
    public string dialogMessage;
    public float interactionDistance = 3f;
    private bool isPlayerNearby = false; 
    private bool isInteracting = false; 
    public Transform[] waypoints;
    public float dialogDisplayTime;
    public float moveSpeed = 2f;
    public float waitTimeAtWaypoint = 2f;
    private int currentWaypointIndex = 0;
    private bool isMoving = true;



    private void Start()
    {
        StartCoroutine(MoveToNextWaypoint());
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
        dialogPanel.SetActive(true);
        dialogText.text = dialogMessage;

        yield return new WaitForSeconds(dialogDisplayTime);

        dialogPanel.SetActive(false);
        isMoving = true;
        isInteracting = false;
    }

    private IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            if (isMoving)
            {
                Transform targetWaypoint = waypoints[currentWaypointIndex];
                while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(waitTimeAtWaypoint);

                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
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