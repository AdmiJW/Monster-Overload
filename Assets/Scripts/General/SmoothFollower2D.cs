using UnityEngine;


public class SmoothFollower2D : MonoBehaviour {

    [Header("References")]
    public Transform target;

    [Header("Follow Settings")]
    [Range(0f, 1f)]
    public float lerpFactor = 0.5f;


    // Update is called once per frame
    void FixedUpdate() {
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;    // Do not move camera's z position
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpFactor);
    }
}
