using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    public float enemySpawnTime = 2f;
    public float enemySpeed = 8f;

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
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);

        Rigidbody2D enemyRB = spawnedEnemy.GetComponent<Rigidbody2D>();
        enemyRB.velocity = Vector2.left * enemySpeed;
    }
}
