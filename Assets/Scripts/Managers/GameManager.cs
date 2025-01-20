using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton para persistencia

    public Image fadeImage; // Referencia a la imagen de fade
    public float fadeDuration = 1f; // Duración del fade in/out

    private int totalCollectibles = 0; // Conteo de coleccionables

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Realiza un fade in al inicio de la escena
        if (fadeImage != null)
        {
            StartCoroutine(Fade(0)); // Fade in (alpha a 0)
        }
    }

    // Método para registrar la recolección de un coleccionable
    public void CollectItem()
    {
        totalCollectibles++;
    }

    // Método para obtener el total de coleccionables recogidos
    public int GetTotalCollectibles()
    {
        return totalCollectibles;
    }

    // Método para cambiar de escena
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeAndChangeScene(sceneName));
    }

    private IEnumerator FadeAndChangeScene(string sceneName)
    {
        // Fade Out
        yield return StartCoroutine(Fade(1));

        // Cambiar a la nueva escena
        SceneManager.LoadScene(sceneName);

        // Esperar un frame para cargar la escena y luego hacer Fade In
        yield return null;

        // Busca nuevamente el GameManager de la nueva escena y realiza el fade in
        GameManager newGameManager = FindObjectOfType<GameManager>();
        if (newGameManager != null && newGameManager.fadeImage != null)
        {
            newGameManager.StartCoroutine(newGameManager.Fade(0));
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        // Asegurarse de que la imagen de fade existe
        if (fadeImage == null)
        {
            yield break;
        }

        // Obtenemos el color actual de la imagen y su opacidad inicial
        Color color = fadeImage.color;
        float startAlpha = color.a;

        // Realizamos la interpolación de la opacidad
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration; // Progreso normalizado entre 0 y 1
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            fadeImage.color = color;
            yield return null;
        }

        // Asegurarse de establecer la opacidad final
        color.a = targetAlpha;
        fadeImage.color = color;
    }
}
