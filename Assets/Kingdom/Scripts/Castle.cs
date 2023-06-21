using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    private float health = 100;
    public delegate void MyDelegate();
    public static event MyDelegate Destroyed;
    public Material sharedMaterial;
    public Image healthBarImage;
    public Volume postPorcessing;
    public VolumeProfile profile;
    public AnimationCurve curve;
    public AudioSource heartBeatAudioSource;
    private Vignette vignette = null;

    private void Start()
    {
        sharedMaterial.SetFloat("_DamageAmount", 0);
        profile.TryGet(out vignette);
        vignette.intensity.value = 0;
    }

    public void Damage()
    {
        print("Recibiendo daño");
        health -= 10;

        float damageAmount = 1 - (health * 0.01f);
        healthBarImage.fillAmount = health * 0.01f;
        vignette.intensity.value = damageAmount;

        float pitch = curve.Evaluate(damageAmount);
        heartBeatAudioSource.pitch = pitch;
        sharedMaterial.SetFloat("_DamageAmount", damageAmount);

        if(health <= 0)
            Destroyed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
            Damage();
    }
}
