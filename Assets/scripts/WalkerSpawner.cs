
using UnityEngine;

public class WalkerSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] prefabsToSpawn;   // Array of different prefabs to spawn
    public int numberToSpawn = 5;         // Total number of objects to spawn
    public float spawnRadius = 20f;       // Radius within which objects will spawn

    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 randomPos = GetRandomPosition();
            GameObject prefab = GetRandomPrefab();
            if (prefab != null)
            {
                Instantiate(prefab, randomPos, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDir = Random.insideUnitSphere * spawnRadius;
        randomDir.y = 0;
        return transform.position + randomDir;
    }

    GameObject GetRandomPrefab()
    {
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogWarning("No prefabs assigned to spawner.");
            return null;
        }

        int index = Random.Range(0, prefabsToSpawn.Length);
        return prefabsToSpawn[index];
    }
}
