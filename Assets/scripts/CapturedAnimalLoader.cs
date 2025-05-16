using UnityEngine;

public class CapturedAnimalLoader : MonoBehaviour
{
    public Transform placementPoint;  // Assign in inspector
    public KeyCode placeKey = KeyCode.F;

    private GameObject capturedAnimalInstance;
    private bool isHolding = false;

    void Start()
    {
        Debug.Log("[Loader] Start called.");

        if (PersistentHolder.Instance == null)
        {
            Debug.LogError("[Loader] PersistentHolder instance is NULL!");
            return;
        }

        if (PersistentHolder.Instance.capturedAnimalPrefab == null)
        {
            Debug.LogWarning("[Loader] No captured animal prefab found in PersistentHolder.");
            return;
        }

        if (placementPoint == null)
        {
            Debug.LogError("[Loader] PlacementPoint is NOT assigned in inspector.");
            return;
        }

        capturedAnimalInstance = Instantiate(PersistentHolder.Instance.capturedAnimalPrefab);

        if (capturedAnimalInstance == null)
        {
            Debug.LogError("[Loader] Instantiation of capturedAnimalPrefab FAILED.");
            return;
        }

        Debug.Log($"[Loader] Instantiated captured animal: {capturedAnimalInstance.name}");

        Rigidbody rb = capturedAnimalInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            Debug.Log("[Loader] Rigidbody set to isKinematic = true");
        }
        else
        {
            Debug.LogWarning("[Loader] Rigidbody component NOT found on captured animal.");
        }

        Collider col = capturedAnimalInstance.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
            Debug.Log("[Loader] Collider disabled");
        }
        else
        {
            Debug.LogWarning("[Loader] Collider component NOT found on captured animal.");
        }

        capturedAnimalInstance.transform.position = placementPoint.position;
        capturedAnimalInstance.transform.rotation = Quaternion.identity;

        Debug.Log($"[Loader] Animal positioned at placementPoint: {placementPoint.position}");

        isHolding = true;
        Debug.Log("[Loader] Holding animal ready to place.");
    }

    void Update()
    {
        if (isHolding && Input.GetKeyDown(placeKey))
        {
            Debug.Log("[Loader] Place key pressed.");
            PlaceAnimal();
        }
    }

    private void PlaceAnimal()
    {
        if (capturedAnimalInstance == null)
        {
            Debug.LogError("[Loader] No captured animal instance to place.");
            return;
        }

        if (placementPoint == null)
        {
            Debug.LogError("[Loader] PlacementPoint is NOT assigned during placement.");
            return;
        }

        capturedAnimalInstance.transform.position = placementPoint.position;
        capturedAnimalInstance.transform.rotation = Quaternion.identity;

        Rigidbody rb = capturedAnimalInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            Debug.Log("[Loader] Rigidbody set to isKinematic = false");
        }
        else
        {
            Debug.LogWarning("[Loader] Rigidbody component NOT found on captured animal at placement.");
        }

        Collider col = capturedAnimalInstance.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
            Debug.Log("[Loader] Collider enabled");
        }
        else
        {
            Debug.LogWarning("[Loader] Collider component NOT found on captured animal at placement.");
        }

        PersistentHolder.Instance.ClearCapturedAnimal();

        isHolding = false;
        capturedAnimalInstance = null;

        Debug.Log("[Loader] Animal placed successfully.");
    }
}
