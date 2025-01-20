using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a cargar
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Buscar al GameManager en la escena
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Iniciar cambio de escena
            gameManager.ChangeScene(sceneToLoad);
        }
    }
}
