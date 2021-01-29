using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move")]
    [Range(0f, 5f)]
    [SerializeField]
    private float moveSpeed = 1f;

    [Header("Bounds")]
    [SerializeField]
    private BoxCollider2D playerBounds;

    [Header("Shoot")]
    [SerializeField]
    private Transform shootPivot;

    [SerializeField]
    private GameObject shootPrefab;

    [SerializeField]
    private GameObject flashGameObject;

    [SerializeField]
    private float hideFlashDelay = 0.1f;

    private void Update()
    {
        Move();

        ApplyBounds();

        Shoot();
    }

    private void Move()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var move = new Vector3(
            h * moveSpeed * Time.deltaTime,
            v * moveSpeed * Time.deltaTime,
            0f
        );

        transform.Translate(move);
    }

    private void ApplyBounds()
    {
        var minX = -playerBounds.bounds.extents.x + playerBounds.offset.x + playerBounds.transform.position.x;
        var maxX = playerBounds.bounds.extents.x + playerBounds.offset.x + playerBounds.transform.position.x;

        var minY = -playerBounds.bounds.extents.y + playerBounds.offset.y + playerBounds.transform.position.y;
        var maxY = playerBounds.bounds.extents.y + playerBounds.offset.y + playerBounds.transform.position.y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }

    private void Shoot()
    {
        if (!Input.GetButtonDown("Fire1"))
        {
            return;
        }

        // Shoot

        Instantiate(shootPrefab, shootPivot.position, shootPivot.rotation);

        // Flash

        flashGameObject.SetActive(true);

        Invoke(nameof(HideFlash), hideFlashDelay);
    }

    private void HideFlash()
    {
        flashGameObject.SetActive(false);
    }
}
