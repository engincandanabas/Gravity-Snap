using UnityEngine;

public class GravityController : MonoBehaviour
{
    public delegate void GravityChanged(bool flippedNormal);
    public static event GravityChanged OnGravityChange;


    private bool isGravityFlipped = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Example key to flip gravity
        {
            isGravityFlipped = !isGravityFlipped;

            // Flip gravity (example)
            Physics2D.gravity = isGravityFlipped ? new Vector2(0, 9.8f) : new Vector2(0, -9.8f);

            OnGravityChange?.Invoke(isGravityFlipped);
        }
    }
}
