using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    public TMP_Text collectibleCountText;
    public Button titleButton;
    public Button quitButton;

    private void Start()
    {
        if (GameManager.instance.GetTotalCollectibles() > 0)
        {
            collectibleCountText.text = "Total Crystals: " + GameManager.instance.GetTotalCollectibles();
        }
        else
        {
            collectibleCountText.text = "Total Crystals: 0";
        }

        titleButton.onClick.AddListener(ReturnToTitle);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void ReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
