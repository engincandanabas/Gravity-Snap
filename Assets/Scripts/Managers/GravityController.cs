using UnityEngine;

public class GravityController : MonoBehaviour
{
    public delegate void GravityChanged(bool flippedNormal);
    public static event GravityChanged OnGravityChange;

    GravitySnap controls;

    private bool isGravityFlipped = false;
    private bool canChangeGravity = false;

    private void Awake()
    {
        controls=new GravitySnap();
        controls.Player.Gravity.performed += ctx => ChangeGravity();
    }
    private void OnEnable()
    {
        controls.Player.Enable();
        PlayerTrigger.OnGroundEnter += ChangeGroundState;
        PlayerTrigger.OnGroundExit += ChangeGroundState;
    }
    private void OnDisable()
    {
        controls.Player.Disable();
        PlayerTrigger.OnGroundEnter -= ChangeGroundState;
        PlayerTrigger.OnGroundExit -= ChangeGroundState;
    }
    private void ChangeGroundState(bool isGround)
    {
        canChangeGravity = isGround;
    }
    private void ChangeGravity()
    {
        if (canChangeGravity)
        {
            isGravityFlipped = !isGravityFlipped;

            // Flip gravity (example)
            Physics2D.gravity = isGravityFlipped ? new Vector2(0, 9.8f) : new Vector2(0, -9.8f);

            OnGravityChange?.Invoke(isGravityFlipped);
        }
    }
   
}
