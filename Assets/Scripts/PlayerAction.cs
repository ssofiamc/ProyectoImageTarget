using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Rigidbody playerRb;
    public float jumpForce = 15f;

    public void Jump()
    {
        if (isGrounded())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Mensaje");
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
