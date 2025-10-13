using UnityEngine;

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
        Vector3 playerPosition = Player.Instance.transform.position;
        float zDistanceToCamera = Mathf.Abs(m_mainCamera.transform.position.z - playerPosition.z);
        Vector3 bottomLeft = m_mainCamera.ScreenToWorldPoint(new Vector3(0, 0, zDistanceToCamera));
        Vector3 differenceVector = bottomLeft - playerPosition;
        
        float minimumSpawnDistance = differenceVector.magnitude + 1f;
        float maximumSpawnDistance = differenceVector.magnitude + 5f;
        
        Debug.Log("SpawnManager: Minimum spawn distance from player is: " + minimumSpawnDistance);
        Debug.Log("SpawnManager: Maximum spawn distance from player is: " + maximumSpawnDistance);

        for (int i = 0; i < 10; i++)
        {
            int randomNumber = Random.Range(0, 359);
            Vector2 direction = new Vector2(Mathf.Cos(randomNumber), Mathf.Sin(randomNumber)).normalized;

            float randomSpawnDistance = Random.Range(minimumSpawnDistance, maximumSpawnDistance);
            Vector2 scaledDirection = direction * randomSpawnDistance;

            Vector2 finalSpawnPosition = new Vector2(playerPosition.x, playerPosition.y) + scaledDirection;

            Instantiate(m_enemyPrefab, finalSpawnPosition, Quaternion.identity, transform.parent);
        }
    }
}
