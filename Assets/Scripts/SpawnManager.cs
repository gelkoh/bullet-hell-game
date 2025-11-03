using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    private Camera m_mainCamera;

    [SerializeField]
    private Tilemap m_groundTilemap;

    [SerializeField]
    private Tilemap m_wallsTilemap;

    [SerializeField]
    private GameObject m_enemiesParent;

    [SerializeField]
    private GameObject m_enemyPrefab;

    private float m_spawnRate = 1f;

    private List<GameObject> m_enemyGameObjects = new();

    void Awake()
    {
        m_mainCamera = Camera.main;
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(DifficultyScaler());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_spawnRate);

            Vector2 spawnPosition = GetRandomSpawnPosition();

            while (!CanSpawnAt(spawnPosition)) {
                spawnPosition = GetRandomSpawnPosition();
            }

            GameObject enemy = Instantiate(m_enemyPrefab, spawnPosition, Quaternion.identity, m_enemiesParent.transform);
            m_enemyGameObjects.Add(enemy);
        }
    }

    private IEnumerator DifficultyScaler()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);

            m_spawnRate = m_spawnRate - m_spawnRate * 0.1f;
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
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

        return new Vector2(playerPosition.x, playerPosition.y) + scaledDirection;
    }

    private bool CanSpawnAt(Vector2 position)
    {
        Vector3Int cell = m_groundTilemap.WorldToCell(position);

        bool isInsideMap = m_groundTilemap.GetTile(cell);
        bool isInsideWall = m_wallsTilemap.GetTile(cell);

        if (!isInsideMap || isInsideWall) return false;

        foreach (Transform enemyTransform in m_enemiesParent.transform)
        {
            if (position.x >= enemyTransform.position.x - 1f &&
                position.x <= enemyTransform.position.x + 1f &&
                position.y >= enemyTransform.position.y - 1f &&
                position.y <= enemyTransform.position.y + 1f)
            {
                Debug.Log("TOO CLOSE TO ANOTHER ENEMY");
                return false;
            }
        }

        return true;
    }
}
