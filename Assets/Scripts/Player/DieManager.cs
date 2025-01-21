using UnityEngine;
using DG.Tweening;

public class DieManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private float deathAnimationDuration = 0.2f;

    private bool isDying = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes") && !isDying)
        {
            Die();
        }
    }

    public void Die()
    {
        isDying = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        deathParticles.Play();

        transform.DOScale(0, deathAnimationDuration)
            .OnComplete(() => Respawn());
    }

    private void Respawn()
    {
        transform.position = spawnPoint.position;
        transform.localScale = Vector3.one;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3;

        isDying = false;
    }
}
