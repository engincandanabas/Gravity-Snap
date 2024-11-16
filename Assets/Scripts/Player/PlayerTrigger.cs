using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collide "+collision.gameObject.tag);
        //CameraController
    }
}
