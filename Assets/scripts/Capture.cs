using UnityEngine;
using UnityEngine.UI; // Import for UI Image handling

public class Capture : MonoBehaviour
{
    public Transform holdPosition;
    public KeyCode interactKey = KeyCode.E;
    public Animator playerAnimator;
    public Image pocketImage;
    public Sprite defaultSprite;
    public Sprite heldObjectSprite;

    private GameObject capturedObject;
    private Rigidbody capturedRb;
    private Collider capturedCollider;
    private bool isHolding = false;

    void Start()
    {
        if (pocketImage != null)
        {
            pocketImage.sprite = defaultSprite;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (isHolding)
            {
                PlaceObject();
            }
            else
            {
                TryCaptureObject();
            }
        }
    }

    private void TryCaptureObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 4f))
        {
            if (hit.collider.CompareTag("Capturable"))
            {
                capturedObject = hit.collider.gameObject;
                capturedRb = capturedObject.GetComponent<Rigidbody>();
                capturedCollider = capturedObject.GetComponent<Collider>();

                if (capturedRb != null)
                {
                    capturedRb.isKinematic = true;
                    capturedRb.velocity = Vector3.zero;
                    capturedRb.angularVelocity = Vector3.zero;
                }

                if (capturedCollider != null)
                {
                    capturedCollider.enabled = false;
                }

                Renderer objectRenderer = capturedObject.GetComponent<Renderer>();
                if (objectRenderer != null)
                {
                    objectRenderer.enabled = false;
                }

                CapturableObject capturable = capturedObject.GetComponent<CapturableObject>();
                if (pocketImage != null)
                {
                    if (capturable != null && capturable.objectSprite != null)
                    {
                        pocketImage.sprite = capturable.objectSprite;
                    }
                    else
                    {
                        pocketImage.sprite = heldObjectSprite;
                    }
                }

                isHolding = true;
                playerAnimator.SetTrigger("Capture");
            }
        }
    }

    private void PlaceObject()
    {
        if (capturedObject != null)
        {
            isHolding = false;
            capturedObject.transform.SetParent(null);

            if (capturedRb != null)
            {
                capturedRb.isKinematic = false;
            }

            if (capturedCollider != null)
            {
                capturedCollider.enabled = true;
            }

            Renderer objectRenderer = capturedObject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.enabled = true;
            }

            Vector3 placePosition = transform.position + transform.forward * 2.5f + transform.up * 1.0f;
            capturedObject.transform.position = placePosition;

            if (pocketImage != null && defaultSprite != null)
            {
                pocketImage.sprite = defaultSprite;
            }

            playerAnimator.SetTrigger("Place");

            capturedObject = null;
        }
    }
}