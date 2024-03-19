using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] engelPrefabs;
    public Transform karakter;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;

    private float xSpawnPosition;

    private void Start()
    {
        InvokeRepeating("SpawnEngel", 0f, spawnInterval);
    }

    private void SpawnEngel()
    {
        xSpawnPosition = Random.Range(-1f, 1f) * spawnDistance / 2f; 

        int engelIndex = Random.Range(0, engelPrefabs.Length);
        GameObject engel = Instantiate(engelPrefabs[engelIndex], new Vector3(xSpawnPosition, 0f, karakter.position.z + spawnDistance), Quaternion.identity);

    }
}