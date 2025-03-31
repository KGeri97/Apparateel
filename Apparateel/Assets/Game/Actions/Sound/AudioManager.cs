using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioResource _harvestSound;
    [SerializeField]
    private AudioResource _investigateSound;
    [SerializeField]
    private AudioResource _plantSound;
    [SerializeField]
    private AudioResource _spraySound;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlaySound(int soundIndex) {
        switch (soundIndex) {
            case 1:
                _audioSource.resource = _plantSound;
                break;
            case 2:
                _audioSource.resource = _spraySound;
                break;
            case 3:
                _audioSource.resource = _investigateSound;
                break;
            case 4:
                _audioSource.resource = _harvestSound;
                break;
            default:
                _audioSource.resource = null;
                break;
        }

        _audioSource.Play();
    }
}
