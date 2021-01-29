using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField]
    private GameObject spawnPrefab;

    [Header("Delay")]
    [SerializeField]
    [Range(0f, 10f)]
    private float initialDelay = 1f;

    [SerializeField]
    [Range(0f, 10f)]
    private float spawnDelay = 1f;

    [Header("Range")]
    [SerializeField]
    private Range rangeX;

    [SerializeField]
    private Range rangeY;

    private void Awake()
    {
        InvokeRepeating(nameof(Spawn), initialDelay, spawnDelay);
    }

    private void Spawn()
    {
        var randomX = Random.Range(rangeX.min, rangeX.max);
        var randomY = Random.Range(rangeY.min, rangeY.max);

        var position = new Vector3(
            transform.position.x + randomX,
            transform.position.y + randomY,
            transform.position.z
        );

        Instantiate(spawnPrefab, position, transform.rotation);
    }
}
