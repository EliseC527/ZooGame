using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -5);
    public float sensitivity = 3f;
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float minPitch = -10f;
    public float maxPitch = 60f;
    public LayerMask groundLayer;

    private float yaw = 0f;
    private float pitch = 20f;
    private float distance;

    private void Start()
    {
        distance = offset.magnitude;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minZoom, maxZoom);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance) + Vector3.up * 2f;

        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredPosition - target.position, out hit, distance, groundLayer))
        {
            desiredPosition = hit.point;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10f);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}