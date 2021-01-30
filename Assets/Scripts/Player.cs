using System;
using System.Collections;
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

    [Header("Death")]
    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private Transform deathPosition;

    private Vector3 _initialDeathPosition;

    [SerializeField]
    private float returnSpeed = 0.2f;

    [SerializeField]
    private float returnThreshold = 0.4f;

    private Vector3 _initialPosition;

    private bool _isDead;

    private void Awake()
    {
        _initialPosition = transform.position;
        _initialDeathPosition = deathPosition.position;
    }

    private void Update()
    {
        Move();

        ApplyBounds();

        Shoot();
    }

    private void Move()
    {
        if (_isDead)
        {
            return;
        }

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
        if (_isDead)
        {
            return;
        }

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
        if (_isDead)
        {
            return;
        }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            // Destroy Enemy & Spawn an Explosion
            Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);

            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        _isDead = true;

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        transform.position = _initialDeathPosition;

        while (Vector3.Distance(transform.position, _initialPosition) > returnThreshold)
        {
            var direction = (_initialPosition - transform.position).normalized;

            transform.position = Vector3.Slerp(
                transform.position,
                transform.position + direction,
                Time.deltaTime * returnSpeed
            );

            yield return null;
        }

        _isDead = false;
    }
}
