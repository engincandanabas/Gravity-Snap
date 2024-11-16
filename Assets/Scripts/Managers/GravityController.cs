using UnityEngine;

public class GravityController : MonoBehaviour
{
    public delegate void GravityChanged(bool flippedNormal);
    public static event GravityChanged OnGravityChange;


    private bool isGravityFlipped = false;
    private bool canChangeGravity = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Example key to flip gravity
        {
            if(canChangeGravity)
            {
                isGravityFlipped = !isGravityFlipped;

                // Flip gravity (example)
                Physics2D.gravity = isGravityFlipped ? new Vector2(0, 9.8f) : new Vector2(0, -9.8f);

                OnGravityChange?.Invoke(isGravityFlipped);
            }
        }
    }
    private void OnEnable()
    {
        PlayerTrigger.OnGroundEnter += ChangeGroundState;
        PlayerTrigger.OnGroundExit += ChangeGroundState;
    }
    private void OnDisable()
    {
        PlayerTrigger.OnGroundEnter -= ChangeGroundState;
        PlayerTrigger.OnGroundExit -= ChangeGroundState;
    }
    private void ChangeGroundState(bool isGround)
    {
        canChangeGravity = isGround;
    }
}
