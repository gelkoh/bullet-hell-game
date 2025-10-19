using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    private Camera m_mainCamera;

    [SerializeField]
    private GameObject m_enemyPrefab;

    void Awake()
    {
        m_mainCamera = Camera.main;
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine(1));
    }

    private IEnumerator SpawnRoutine(float waitTime)
    {
        // TODO: Fix enemies spawning inside each other

        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 playerPosition = Player.Instance.transform.position;
            float zDistanceToCamera = Mathf.Abs(m_mainCamera.transform.position.z - playerPosition.z);
            Vector3 bottomLeft = m_mainCamera.ScreenToWorldPoint(new Vector3(0, 0, zDistanceToCamera));
            float distanceToScreenCorner = Vector3.Distance(playerPosition, bottomLeft);

            float minimumSpawnDistance = distanceToScreenCorner + 1f;
            float maximumSpawnDistance = distanceToScreenCorner + 5f;

            int randomNumber = Random.Range(0, 359);
            Vector2 direction = new Vector2(Mathf.Cos(randomNumber), Mathf.Sin(randomNumber)).normalized;

            float randomSpawnDistance = Random.Range(minimumSpawnDistance, maximumSpawnDistance);
            Vector2 scaledDirection = direction * randomSpawnDistance;

            Vector2 finalSpawnPosition = new Vector2(playerPosition.x, playerPosition.y) + scaledDirection;

            Instantiate(m_enemyPrefab, finalSpawnPosition, Quaternion.identity, transform.parent);
        }
    }
}
