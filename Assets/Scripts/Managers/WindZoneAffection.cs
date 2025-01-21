using UnityEngine;

public class WindZoneAffection : MonoBehaviour
{
    public float windForce = -10f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        Movement playerMovement = other.GetComponent<Movement>();

        if (rb != null && other.CompareTag("Player") && playerMovement != null)
        {
            if (!playerMovement.wallGrab && !playerMovement.wallSlide)
            {
                rb.AddForce(Vector2.right * windForce, ForceMode2D.Force);
            }
        }
    }
}
