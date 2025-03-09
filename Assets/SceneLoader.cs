using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SceneLoader : MonoBehaviour
{
    [Header("Folder Settings")]
    public string folderPath = "Prefabs"; // Folder inside Resources

    [Header("Spawn Settings")]
    public Transform spawnCenter; // Center of the spawn area
    public Vector3 spawnSize = new Vector3(10f, 0f, 10f); // Defines the spawn area size
    public int spawnCount = 5; // Number of objects to spawn

    private GameObject[] availablePrefabs; // Stores loaded prefabs

    void Start()
    {
        LoadPrefabs();
        SpawnObjects();
    }

    void LoadPrefabs()
    {
        availablePrefabs = Resources.LoadAll<GameObject>(folderPath);

        if (spawnCount > availablePrefabs.Length)
        {
            spawnCount = availablePrefabs.Length;
        }

        if (availablePrefabs.Length == 0)
        {
            Debug.LogError($"No prefabs found in Resources/{folderPath}! Check the folder name.");
        }
    }

    void SpawnObjects()
    {
        if (availablePrefabs.Length == 0) return;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        if (availablePrefabs.Length == 0) return;

        GameObject prefabToSpawn = availablePrefabs[Random.Range(0, availablePrefabs.Length)];

        Vector3 randomPosition = new Vector3(
            spawnCenter.position.x + Random.Range(-spawnSize.x / 2, spawnSize.x / 2),
            spawnCenter.position.y + 1f, // Lift it slightly above ground
            spawnCenter.position.z + Random.Range(-spawnSize.z / 2, spawnSize.z / 2)
        );

        GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);

        if (spawnedObject.GetComponent<Collider>() == null)
        {
            spawnedObject.AddComponent<MeshCollider>();
        }

        // Ensure the object has a Rigidbody with appropriate physics settings
        if (spawnedObject.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.linearDamping = 0.1f;
            rb.angularDamping = 0.05f;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        // Ensure the object has an XR Grab Interactable
        if (spawnedObject.GetComponent<XRGrabInteractable>() == null)
        {
            XRGrabInteractable grabInteractable = spawnedObject.AddComponent<XRGrabInteractable>();
            grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking; // Set to Velocity Tracking
        }


        Debug.Log($"Spawned {spawnedObject.name} at {randomPosition} with Rigidbody & Collider");
    }
}
