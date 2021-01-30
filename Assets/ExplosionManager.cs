using System;
using UnityEngine;

[Serializable]
public class ClipWithVolume
{
    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 0.2f;
}

public class ExplosionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private ClipWithVolume explosionFx;

    public static ExplosionManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Create(Vector3 position, Quaternion rotation)
    {
        Instantiate(explosionPrefab, position, rotation);

        AudioManager.Play(explosionFx);
    }
}
