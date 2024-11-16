using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public delegate void TriggerEnter(Collision2D other);
    public delegate void TriggerExit(Collision2D other);


    public delegate void GroundEnter(bool isGrounded);
    public delegate void GroundExit(bool isGrounded);
    public static event GroundEnter OnGroundEnter;
    public static event GroundEnter OnGroundExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collide "+collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGroundEnter?.Invoke(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Player collide exit" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnGroundExit?.Invoke(false);
        }
    }
}
