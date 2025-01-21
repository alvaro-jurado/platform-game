using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string sceneToLoad;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ChangeScene(sceneToLoad);
        }
    }
}
