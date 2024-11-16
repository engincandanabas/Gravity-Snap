using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera
    public CinemachinePositionComposer positionComposer;
    public Vector3 normalOffset = new Vector3(0, 2, -10); // Offset for normal gravity
    public Vector3 flippedOffset = new Vector3(0, -2, -10); // Offset for flipped gravity
    public float transitionDuration = 0.5f; // Duration of the smooth transition
    private void Start()
    {
        positionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
    }
    private void OnEnable()
    {
        GravityController.OnGravityChange += GravityChanged;
    }

    private void GravityChanged(bool flippedNormal)
    {
        // Set the new offset
        Quaternion targetRotation = flippedNormal ? Quaternion.Euler(Vector3.forward * 180) : Quaternion.Euler(Vector3.zero);
        Vector3 targetOffset = flippedNormal ? flippedOffset : normalOffset;

        // Smoothly transition to the new offset
        StartCoroutine(SmoothOffsetChange(targetRotation, targetOffset));
    }
    private IEnumerator SmoothOffsetChange(Quaternion targetRotation, Vector3 targetOffset)
    {
        Transform initialOffset = cinemachineCamera.Target.TrackingTarget;
        // Ensure the offset is set to the final target
        float elapsedTime = 0;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            cinemachineCamera.transform.rotation = Quaternion.Lerp(cinemachineCamera.transform.rotation, targetRotation, elapsedTime / transitionDuration);
            yield return null;
        }
        //positionComposer.TargetOffset = targetOffset;
    }
}
