using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip gemSound, manaSound, heartSound, hitSound, deathSound, discoverySound, daggerSound,daggerThrowSound, swordSound, slimeHitSound, slimeDeathSound;
    static AudioSource audioSrc;
    void Start()
    {
        gemSound = Resources.Load<AudioClip>("Gem");
        manaSound = Resources.Load<AudioClip>("Mana Potion");
        heartSound = Resources.Load<AudioClip>("Heart");
        hitSound = Resources.Load<AudioClip>("Hit");
        deathSound = Resources.Load<AudioClip>("Death");
        discoverySound = Resources.Load<AudioClip>("Discovery");
        daggerSound = Resources.Load<AudioClip>("Dagger");
        daggerThrowSound = Resources.Load<AudioClip>("DaggerThrow");
        swordSound = Resources.Load<AudioClip>("Sword");
        slimeHitSound = Resources.Load<AudioClip>("SlimeHit");
        slimeDeathSound = Resources.Load<AudioClip>("SlimeDeath");



        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Gem":
                audioSrc.PlayOneShot(gemSound);
                break;
            case "Heart":
                audioSrc.PlayOneShot(heartSound);
                break;
            case "Mana Potion":
                audioSrc.PlayOneShot(manaSound);
                break;
            case "Hit":
                audioSrc.PlayOneShot(hitSound);
                break;
            case "Death":
                audioSrc.PlayOneShot(deathSound);
                break;
            case "Discovery":
                audioSrc.PlayOneShot(discoverySound);
                break;
            case "Dagger":
                audioSrc.PlayOneShot(daggerSound);
                break;
            case "DaggerThrow":
                audioSrc.PlayOneShot(daggerThrowSound);
                break;
            case "Sword":
                audioSrc.PlayOneShot(swordSound);
                break;
            case "SlimeHit":
                audioSrc.PlayOneShot(slimeHitSound);
                break;
            case "SlimeDeath":
                audioSrc.PlayOneShot(slimeDeathSound);
                break;
        }
    }
}
