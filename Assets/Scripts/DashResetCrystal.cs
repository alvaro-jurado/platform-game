using UnityEngine;
using System.Collections;
public class DashResetCrystal : MonoBehaviour
{
    public float respawnTime = 5f;
    public GameObject crystalSprite;
    public GameObject crystalOutline;
    //public ParticleSystem collectEffect;

    private bool isCollected = false;
    private bool isPulsing = false;
    public float pulseSpeed = 2f;
    public float pulseScale = 1.2f;

    public float appearDuration = 0.5f;
    private Vector3 originalScale;
    private Vector3 outlineScale;

    private void Start()
    {
        if (crystalSprite != null)
            originalScale = crystalSprite.transform.localScale;

        if (crystalOutline != null)
        {
            outlineScale = originalScale * 0.5f;
            crystalOutline.SetActive(false);
            crystalOutline.transform.localScale = outlineScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement playerMovement = collision.GetComponent<Movement>();
        if (playerMovement != null && !isCollected)
        {
            playerMovement.hasDashed = false;

           // if (collectEffect != null)
              //  Instantiate(collectEffect, transform.position, Quaternion.identity);

            StartCoroutine(RespawnCrystal());
        }
    }

    private IEnumerator RespawnCrystal()
    {
        isCollected = true;

        if (crystalSprite != null)
        {
            crystalSprite.SetActive(false);
        }
           
        if (crystalOutline != null)
        {
            crystalOutline.SetActive(true);
            StartCoroutine(PulseOutline());
        }

        yield return new WaitForSeconds(respawnTime);

        if (crystalSprite != null)
        {
            crystalSprite.SetActive(true);
            yield return StartCoroutine(AnimateAppear());
        }

        if (crystalOutline != null)
        {
            crystalOutline.SetActive(false);
            StopCoroutine(PulseOutline());
            crystalOutline.transform.localScale = outlineScale;

            isCollected = false;
        }

        isCollected = false;
    }

    private IEnumerator AnimateAppear()
    {
        float elapsedTime = 0f;
        crystalSprite.transform.localScale = Vector3.zero;

        while (elapsedTime < appearDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / appearDuration;
            crystalSprite.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            yield return null;
        }

        crystalSprite.transform.localScale = originalScale;
    }

    private IEnumerator PulseOutline()
    {
        isPulsing = true;

        while (isPulsing)
        {
            float elapsedTime = 0f;
            while (elapsedTime < 1f / pulseSpeed)
            {
                elapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(1f, pulseScale, elapsedTime * pulseSpeed);
                crystalOutline.transform.localScale = outlineScale * scale;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < 1f / pulseSpeed)
            {
                elapsedTime += Time.deltaTime;
                float scale = Mathf.Lerp(pulseScale, 1f, elapsedTime * pulseSpeed);
                crystalOutline.transform.localScale = outlineScale * scale;
                yield return null;
            }
        }
    }
}
