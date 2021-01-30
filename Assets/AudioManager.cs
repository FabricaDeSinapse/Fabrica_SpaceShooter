using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void Play(ClipWithVolume clipWithVolume)
    {
        var newGameObject = new GameObject();
        var audioSource = newGameObject.AddComponent<AudioSource>();
        audioSource.clip = clipWithVolume.clip;
        audioSource.volume = clipWithVolume.volume;

        audioSource.Play();

        Destroy(newGameObject, clipWithVolume.clip.length);
    }
}
