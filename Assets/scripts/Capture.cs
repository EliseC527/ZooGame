using UnityEngine;

public class Capture : MonoBehaviour
{
    public Transform holdPosition;
    public KeyCode interactKey = KeyCode.F;

    private bool isHolding = false;
    private GameObject capturedObject;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (isHolding)
            {
                Debug.Log("Already holding an object.");
                return;
            }

            TryCaptureObject();
        }
    }

    private void TryCaptureObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
        {
            if (hit.collider.CompareTag("Capturable"))
            {
                CapturableObject capturable = hit.collider.GetComponent<CapturableObject>();

                if (capturable != null && capturable.prefabReference != null)
                {
                    PersistentHolder.Instance.capturedAnimalPrefab = capturable.prefabReference;

                    if (capturable.gameObject != null)
                        capturable.gameObject.SetActive(false);

                    isHolding = true;
                    Debug.Log("Captured: " + capturable.prefabReference.name);
                }
                else
                {
                    Debug.LogWarning("Captured object missing prefab reference.");
                }
            }
            else
            {
                Debug.Log("Hit object is not capturable.");
            }
        }
        else
        {
            Debug.Log("Nothing to capture in front.");
        }
    }
}
