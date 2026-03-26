using UnityEngine;

public class AudioManager : MonoBehaviour {
  public static AudioManager Instance { get; private set; }

  [Header("Audio Settings")]
  [Range(0f, 1f)]
  public static float masterVolume = 1f;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
    } else {
      Destroy(gameObject);
    }
  }

  public static void PlayAudioClip(AudioClip clip, float volume = 1f, float pitchVariance = 0.1f) {
    if (clip == null)
      return;

    GameObject tempGO = new("Audio Clip (Temporary)");
    if (Camera.main != null)
      tempGO.transform.position = Camera.main.transform.position;

    tempGO.transform.parent = null;

    // Set up AudioSource
    AudioSource audioSource = tempGO.AddComponent<AudioSource>();
    audioSource.clip = clip;
    audioSource.volume = volume * masterVolume;

    // random pitch
    audioSource.pitch = Random.Range(1 - pitchVariance, 1 + pitchVariance);

    audioSource.spatialBlend = 0f; // 2D sound
    audioSource.Play();

    // Destroy after the adjusted length
    Destroy(tempGO, clip.length / audioSource.pitch);
  }
}
