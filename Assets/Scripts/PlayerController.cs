using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public JoystickController joystick;        // Tu joystick virtual
    public float speed = 5f;                   // Velocidad de movimiento
    public Animator animator;                  // Referencia al Animator
    public float rotationSpeed = 10f;          // Velocidad para girar el personaje

    private Vector2 joystickInput = Vector2.zero;

    void Update()
    {
        Vector3 move = new Vector3(joystickInput.x, 0f, joystickInput.y);

        // Mover personaje
        transform.Translate(move * speed * Time.deltaTime, Space.World);

        // Girar personaje hacia la dirección de movimiento
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (animator != null)
        {
            float speedValue = joystickInput.magnitude;
            animator.SetFloat("Speed", speedValue);
            Debug.Log("Speed Animator Value: " + speedValue); // Quita esto si no lo necesitas
        }
    }
    private void OnEnable()
    {
        if (joystick != null)
            joystick.onJoystickMove.AddListener(UpdateInput);
    }

    private void OnDisable()
    {
        if (joystick != null)
            joystick.onJoystickMove.RemoveListener(UpdateInput);
    }

   private void UpdateInput(Vector2 input)
{
    joystickInput = input;

    if (animator != null)
    {
        float speedValue = input.magnitude; // Esto será 0 cuando no haya input
        animator.SetFloat("Speed", speedValue);
    }
}

}
