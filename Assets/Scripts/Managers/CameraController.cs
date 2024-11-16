using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera
    [SerializeField] private CinemachinePositionComposer positionComposer;

    [SerializeField] private Vector3 normalOffset = new Vector3(0, 2, -10); // Offset for normal gravity
    [SerializeField] private float transitionDuration = 0.5f; // Duration of the smooth transition

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
        Vector3 targetRotation = flippedNormal ? Vector3.forward * 180 : Vector3.zero;
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
        this.transform.parent.transform.DORotate(targetRotation, transitionDuration).SetEase(ease).OnComplete(() =>
        {
            positionComposer.TargetOffset = normalOffset;
        });
    }
}
