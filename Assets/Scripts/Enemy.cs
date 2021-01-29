using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private int healthPoints = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shoot"))
        {
            // Spawn Hit and Destroy Shoot

            Instantiate(hitPrefab, other.transform.position, hitPrefab.transform.rotation);

            Destroy(other.gameObject);

            // Update Health Points

            healthPoints--;

            // Check if Health Points is below 0 to destroy it

            if (healthPoints <= 0)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }
    }
}
