using UnityEngine;

public class WindZoneAffection : MonoBehaviour
{
    public float windForce = -10f; // Fuerza del viento (negativa para derecha a izquierda)

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verifica si el objeto tiene un Rigidbody2D
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        Movement playerMovement = other.GetComponent<Movement>(); // Referencia al script del jugador

        if (rb != null && other.CompareTag("Player") && playerMovement != null)
        {
            // Aplica fuerza solo si el jugador NO está agarrado o deslizándose por una pared
            if (!playerMovement.wallGrab && !playerMovement.wallSlide)
            {
                rb.AddForce(Vector2.right * windForce, ForceMode2D.Force);
            }
        }
    }
}
