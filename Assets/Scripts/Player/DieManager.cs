using UnityEngine;
using DG.Tweening;

public class DieManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ParticleSystem deathParticles; // Partículas de muerte
    [SerializeField] private float deathAnimationDuration = 0.2f; // Duración de la animación de muerte

    private bool isDying = false; // Para evitar múltiples activaciones

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes") && !isDying)
        {
            Die();
        }
    }

    private void Die()
    {
        isDying = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero; // Detener el movimiento
        rb.gravityScale = 0;       // Desactivar gravedad temporalmente

        // Reproducir partículas de muerte
        deathParticles.Play();

        // Hacer desaparecer al personaje con escala o transparencia
        transform.DOScale(0, deathAnimationDuration)
            .OnComplete(() => Respawn());
    }

    private void Respawn()
    {
        // Restaurar posición y escala
        transform.position = spawnPoint.position;
        transform.localScale = Vector3.one;

        // Reactivar la gravedad
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3;

        isDying = false; // Permitir futuras muertes
    }
}
