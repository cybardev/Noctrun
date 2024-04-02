using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    public float enemySpawnTime = 2f;
    public float enemySpeed = 1f;

    private float timeUntilEnemySpawn;

    private void Start()
    {
        // ground enemies aren't stopped by the ground layer
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    private void Update()
    {
        timeUntilEnemySpawn += Time.deltaTime;

        if (timeUntilEnemySpawn >= enemySpawnTime)
        {
            Spawn();
            timeUntilEnemySpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * enemySpeed;
    }
}
