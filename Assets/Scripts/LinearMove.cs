using UnityEngine;

public class LinearMove : MonoBehaviour
{
    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private float moveSpeed = 1f;

    private void Update()
    {
        transform.Translate(direction * (moveSpeed * Time.deltaTime));
    }
}
