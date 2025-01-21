using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image fadeImage;
    public float fadeDuration = 1f;

    private int totalCollectibles = 0;

    private void Awake()
    {
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
        if (fadeImage != null)
        {
            StartCoroutine(Fade(0));
        }
    }

    public void CollectItem()
    {
        totalCollectibles++;
    }

    public void RemoveItem()
    {
        totalCollectibles--;
    }

    public int GetTotalCollectibles()
    {
        return totalCollectibles;
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(FadeAndChangeScene(sceneName));
    }

    private IEnumerator FadeAndChangeScene(string sceneName)
    {
        yield return StartCoroutine(Fade(1));

        SceneManager.LoadScene(sceneName);

        yield return null;

        GameManager newGameManager = FindObjectOfType<GameManager>();
        if (newGameManager != null && newGameManager.fadeImage != null)
        {
            newGameManager.StartCoroutine(newGameManager.Fade(0));
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (fadeImage == null)
        {
            yield break;
        }

        Color color = fadeImage.color;
        float startAlpha = color.a;

        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            fadeImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
    }
}
