using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityUICanvas : MonoBehaviour
{[Tooltip("The UI canvas to display when the player is nearby")]
    public GameObject uiCanvas;

    [Tooltip("The player GameObject to detect")]
    public GameObject playerObject;

    [Tooltip("The max distance for the UI to appear")]
    public float activationDistance = 5f;

    private void Start()
    {
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false); // Start hidden
        }
    }

    private void Update()
    {
        if (uiCanvas == null || playerObject == null) return;

        float distance = Vector3.Distance(playerObject.transform.position, transform.position);

        if (distance <= activationDistance)
        {
            if (!uiCanvas.activeSelf)
                uiCanvas.SetActive(true);

            // Optional: make it face the camera
            uiCanvas.transform.position = transform.position + Vector3.up * 2f;
            uiCanvas.transform.LookAt(Camera.main.transform);
            uiCanvas.transform.Rotate(0, 180, 0); // Flip to face correctly
        }
        else
        {
            if (uiCanvas.activeSelf)
                uiCanvas.SetActive(false);
        }
    }
}
