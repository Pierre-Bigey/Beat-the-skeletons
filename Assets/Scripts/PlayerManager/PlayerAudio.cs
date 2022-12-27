using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource source;
    private GameManager GM;
    [SerializeField] private AudioClip playerPunchClip;
    [SerializeField] private AudioClip[] playerHitClips;
    [SerializeField] private AudioClip playerDeathClip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GM = GameManager.Instance;
        SetEffectVolume(GM.settingsData.effectVolume);
    }

    public void PlayPunchSound()
    {
        source.PlayOneShot(playerPunchClip);
    }

    public void PlayHitSound()
    {
        AudioClip clip = playerHitClips[Random.Range(0, playerHitClips.Length)];
        source.PlayOneShot(clip);
    }
    public void PlayDeathSound()
    {
        source.PlayOneShot(playerDeathClip);
    }


    public void SetEffectVolume(float volumeInput)
    {
        source.volume = volumeInput;
    }
}
