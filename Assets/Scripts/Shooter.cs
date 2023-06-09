using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General Functionality")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = .2f;
    [Header("AI Functionality")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = .1f;
    [HideInInspector] public bool isFiring;
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuosly()
    {
        while(true)
        {
            
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);
            
            float randTime = RandomShootTime();

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(randTime);
        }
    }
    float RandomShootTime()
    {
        float shootTime = UnityEngine.Random.Range(baseFiringRate - firingRateVariance,
                                        baseFiringRate + firingRateVariance);
        return Mathf.Clamp(shootTime, minimumFiringRate, float.MaxValue);
    }
}
