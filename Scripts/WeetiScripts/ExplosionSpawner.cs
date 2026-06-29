using System.Collections;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] float WaitTime;
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] AudioSource Sound;

    private void Start()
    {
        StartCoroutine(ExplosionStart());
        StartCoroutine(SoundEffect());
        
    }

    IEnumerator SoundEffect()
    {
        yield return new WaitForSeconds(0.8f);
        Sound.Play();
    }

    IEnumerator ExplosionStart()
    {
        yield return new WaitForSeconds(WaitTime);
        ExplosionEffect.SetActive(true);
    }
}
