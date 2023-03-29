using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array of prefabs to spawn.
    public float spawnInterval = 3.0f; // Time between spawns in seconds.
    public float spawnForce = 10.0f; // Force to launch spawned object.
    public float spinForce = 5.0f; // Force to apply spin to spawned object.

    public Transform playerTransform;
    public Transform startingTransform;

    private void Start()
    {

        // Start spawning objects.
        InvokeRepeating("SpawnObject", 0.0f, spawnInterval);
    }

    private void SpawnObject()
    {
        // Choose a random prefab from the array.
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject prefabToSpawn = prefabs[randomIndex];

        // Instantiate a new prefab.
        GameObject newObject = Instantiate(prefabToSpawn, startingTransform.position, Quaternion.identity);
        InitializeObject(newObject);


        // Calculate the direction to launch the object towards the player.
        Vector3 launchDirection = (playerTransform.position - startingTransform.position).normalized;

        // Apply the launch force and spin force to the object.
        Rigidbody rb = newObject.GetComponent<Rigidbody>();
        rb.AddForce(launchDirection * spawnForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * spinForce, ForceMode.Impulse);
        //// Calculate the direction to launch the object towards the player.
        //Vector3 forwardDirection = transform.forward;
        //Vector3 targetDirection = (playerTransform.position - startingTransform.position).normalized;
        //Vector3 launchDirection = Vector3.Slerp(forwardDirection, targetDirection, 0.5f).normalized;

        //// Apply the launch force and spin force to the object.
        //Rigidbody rb = newObject.GetComponent<Rigidbody>();
        //rb.AddForce(targetDirection * spawnForce, ForceMode.Impulse);
        //rb.AddTorque(Random.insideUnitSphere * spinForce, ForceMode.Impulse);
    }

    private void InitializeObject(GameObject obj)
    {
        if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
            rb = obj.AddComponent<Rigidbody>();
        rb.mass = 0.5f;
        rb.useGravity = false;

        if (obj.TryGetComponent<DeleteAfter5Seconds>(out DeleteAfter5Seconds temp) == false)
            temp = obj.AddComponent<DeleteAfter5Seconds>();

        obj.tag = "Sliceable";

    }
}
