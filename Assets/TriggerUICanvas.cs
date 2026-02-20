using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUICanvas : MonoBehaviour
{
    [Tooltip("The UI canvas to show when the player enters this zone")]
    public GameObject uiCanvas;

    [Tooltip("The player GameObject to detect")]
    public GameObject playerObject;

    private void Start()
    {
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false); // Ensure it's hidden initially
        }
    }

    private void OnTriggerEnter(Collider other)
    {Debug.Log("Entered Trigger: " + other.name);
        if (other.transform.root.gameObject == playerObject && uiCanvas != null)
        {Debug.Log("Player detected. Showing UI.");
            uiCanvas.SetActive(true);
            // Position canvas in front of the camera
            uiCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2f;

            // Rotate to face the camera
            uiCanvas.transform.LookAt(Camera.main.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.gameObject == playerObject && uiCanvas != null)
        {Debug.Log("Player left. Hiding UI.");
            uiCanvas.SetActive(false);
        }
    }
}
