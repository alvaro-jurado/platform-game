using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Vector3> playerPositions = new List<Vector3>();
    private Movement playerController;
    private int currentIndex = 0;
    private Rigidbody2D rb;

    public void Init(List<Vector3> recordedPositions, Movement player)
    {
        playerPositions = recordedPositions;
        playerController = player;

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            Debug.Log("Current Index: " + currentIndex);
            Debug.Log("Player positions count:" +  playerPositions.Count);
            if (currentIndex < playerPositions.Count)
            {
                Vector3 targetPosition = playerPositions[currentIndex];
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, 0.2f);
                transform.position = smoothedPosition;
                currentIndex++;
            }
            
            yield return new WaitForFixedUpdate();
        }
    }

    public void DecreaseIndex()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DieManager dieManager = collision.GetComponent<DieManager>();
            dieManager.Die();

            Destroy(gameObject);
            GameManager.instance.RemoveItem();
            StartCoroutine(SpawnEnemyAgain());
        }
    }

    private IEnumerator SpawnEnemyAgain()
    {
        yield return new WaitForSeconds(3f);
        playerController.SpawnEnemy();
    }
}