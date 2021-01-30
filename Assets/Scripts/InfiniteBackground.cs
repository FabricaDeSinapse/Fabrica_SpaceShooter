using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField]
    private float moveAmount = 2.72f;

    private void Update()
    {
        if (transform.position.x < -moveAmount)
        {
            transform.Translate(Vector2.right * moveAmount * 2);
        }
    }
}
