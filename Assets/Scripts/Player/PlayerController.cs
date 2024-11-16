using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    private void OnEnable()
    {
        GravityController.OnGravityChange +=GravityChanged;
    }
    private void GravityChanged(bool flippedNormal)
    {
        Vector3 targetRotation=(flippedNormal ? Vector3.forward*180 : Vector3.zero);
        this.transform.DORotate(targetRotation, 0.7f).SetEase(Ease.InOutSine);
    }
}
