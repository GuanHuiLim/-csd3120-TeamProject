using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfter5Seconds : MonoBehaviour
{
    public float timeToLive = 5.0f; // The amount of time before the object is deleted.

    private void Start()
    {
        // Schedule the object to be deleted after the specified time.
        Destroy(gameObject, timeToLive);
    }
}
