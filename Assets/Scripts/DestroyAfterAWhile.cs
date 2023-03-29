using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAWhile : MonoBehaviour
{
    public float fadeDuration = 4.5f; // Duration of fade effect in seconds.

    //private Renderer renderer;
    private Material material;
    private float fadeTimer;
    private bool isFading;

    private void Start()
    {
        //renderer = GetComponent<Renderer>();
        material = GetComponent<Renderer>().material;
        fadeTimer = fadeDuration;
    }

    private void Update()
    {
        if (isFading)
        {
            // Calculate alpha value based on time remaining for fade effect.
            float alpha = fadeTimer / fadeDuration;
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

            // Decrease fade timer.
            fadeTimer -= Time.deltaTime;

            // If fade timer runs out, destroy the object.
            if (fadeTimer <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartFading()
    {
        isFading = true;
    }

}
