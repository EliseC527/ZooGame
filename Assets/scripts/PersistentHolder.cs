using UnityEngine;

public class PersistentHolder : MonoBehaviour
{
    public static PersistentHolder Instance;

    public GameObject capturedAnimalPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[PersistentHolder] Instance set and marked DontDestroyOnLoad");
        }
        else
        {
            Debug.Log("[PersistentHolder] Duplicate detected, destroying this");
            Destroy(gameObject);
        }
    }

    public void ClearCapturedAnimal()
    {
        Debug.Log("[PersistentHolder] ClearCapturedAnimal called");
        capturedAnimalPrefab = null;
    }
}
