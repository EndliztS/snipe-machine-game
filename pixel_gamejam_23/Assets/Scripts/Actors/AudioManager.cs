using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource, sfxSource;

    void Awake() {
        if (!Instance) {
            Instance = this;
        } else Destroy(gameObject);
    }

    public void PlaySFX(Sound sound) {
        sfxSource.pitch = Random.Range(sound.minPitch,sound.maxPitch);
        sfxSource.PlayOneShot(sound.clip);
    }
}
