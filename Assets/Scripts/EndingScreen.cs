using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingScreen : MonoBehaviour
{
    public TMP_Text collectibleCountText;

    private void Start()
    {
        if (GameManager.instance.GetTotalCollectibles() > 0)
        {
            collectibleCountText.text = "Total Coleccionables: " + GameManager.instance.GetTotalCollectibles();
        } else
        {
            collectibleCountText.text = "Total Coleccionables: 0";
        }
    }
}
