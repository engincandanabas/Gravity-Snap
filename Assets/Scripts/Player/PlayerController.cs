using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private void OnEnable()
    {
        GravityController.OnGravityChange +=GravityChanged;
    }
    private void GravityChanged(bool flippedNormal)
    {
        
    }
}
