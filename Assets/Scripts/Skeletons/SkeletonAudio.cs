using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAudio : MonoBehaviour
{
    private AudioSource source;
    private GameManager GM;
    [SerializeField] AudioClip skeletonAttackClip;
    [SerializeField] AudioClip skeletonHitClip;
    [SerializeField] AudioClip skeletonDeathClip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GM = GameManager.Instance;
        SetEffectVolume(GM.settingsData.effectVolume);
    }
    public void SetEffectVolume(float volumeInput)
    {
        source.volume = volumeInput;
    }

    public void PlayHitSound()
    {
        source.PlayOneShot(skeletonHitClip);
    }
    public void PlayAttackSound()
    {
        source.PlayOneShot(skeletonAttackClip);
    }
    public void PlayDeathSound()
    {
        source.PlayOneShot(skeletonDeathClip);
    }
}
