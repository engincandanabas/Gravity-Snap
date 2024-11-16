using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera
    public CinemachinePositionComposer positionComposer;

    public Vector3 normalOffset = new Vector3(0, 2, -10); // Offset for normal gravity
    public Vector3 flippedOffset = new Vector3(0, -2, -10); // Offset for flipped gravity
    public float transitionDuration = 0.5f; // Duration of the smooth transition

    [SerializeField] private float defaultLensSize = 5f;
    [SerializeField] private float flippedLensSize = 2.5f;
    [SerializeField] private Ease ease;
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
        Vector3 targetRotation = flippedNormal ? Vector3.forward * 180 : Vector3.zero;
        //positionComposer.TargetOffset = flippedOffset;
        //StartCoroutine(SmoothLensSizeChange(flippedLensSize));
        SmoothOffsetChange(targetRotation);
    }
    private IEnumerator SmoothLensSizeChange(float targetLensSize)
    {
        float animDuration =0.5f;
        float elapsedTime = 0;
        while (elapsedTime < animDuration)
        {
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetLensSize, elapsedTime / animDuration);
            yield return null;
        }
    }
    private void SmoothOffsetChange(Vector3 targetRotation)
    {
        //Time.timeScale = 0.5f;
        //Transform initialOffset = cinemachineCamera.Target.TrackingTarget;
        // Ensure the offset is set to the final target
        cinemachineCamera.transform.DORotate(targetRotation, transitionDuration).SetEase(ease).OnComplete(() =>
        {
            positionComposer.TargetOffset = normalOffset;
            //StartCoroutine(SmoothLensSizeChange(defaultLensSize));
        });
    }
}
