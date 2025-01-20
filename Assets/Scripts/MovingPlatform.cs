using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points; // Puntos entre los que se moverá la plataforma
    public float speed = 2f; // Velocidad de movimiento
    private int currentPointIndex = 0;
    private Transform platform; // Referencia a la plataforma
    private Vector3 targetPosition;

    private bool playerOnPlatform = false; // Si el jugador está sobre o agarrado a la plataforma

    private void Start()
    {
        if (transform.childCount > 0)
        {
            platform = transform.GetChild(0); // Se asume que la plataforma visible es el primer hijo
        }
        else
        {
            Debug.LogError("La Plataforma Móvil no tiene hijos asignados como su plataforma visible.");
            return;
        }

        if (points.Length > 0)
        {
            targetPosition = points[currentPointIndex].position;
        }
        else
        {
            Debug.LogError("No se han asignado puntos en el script de MovingPlatform.");
        }
    }

    private void Update()
    {
        if (!playerOnPlatform || platform == null || points.Length == 0)
            return; // Evitar movimiento si no hay jugador en la plataforma o faltan referencias

        // Mover la plataforma hacia el punto objetivo
        platform.position = Vector3.MoveTowards(platform.position, targetPosition, speed * Time.deltaTime);

        // Si alcanza el punto objetivo, cambiar al siguiente punto
        if (Vector3.Distance(platform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
            targetPosition = points[currentPointIndex].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true; // Activar movimiento cuando el jugador esté sobre la plataforma
            other.transform.SetParent(platform); // Hacer al jugador hijo de la plataforma
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false; // Desactivar movimiento cuando el jugador salga de la plataforma
            other.transform.SetParent(null); // Quitar al jugador como hijo de la plataforma
        }
    }
}
