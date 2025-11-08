using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUp : MonoBehaviour
{
    [Header("Audio")] public AudioSource audioSource;
    [Header("Puntuación")] public int puntosPowerUp = 10;

    public PuntosPowerUp powerUp;


    void Start()
    {
        var rb=GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 0f;
        powerUp = FindFirstObjectByType<PuntosPowerUp>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            powerUp.AddPoints(puntosPowerUp);
            powerUp.MostrarPuntosDinamicos(puntosPowerUp,transform.position);
            puntosPowerUp = 0;
            if (--other.GetComponent<ContadorPowerUps>().powerUps <= 0)
            {
                if (Application.CanStreamedLevelBeLoaded("Menu"))
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                else
                    Debug.LogError($"Escena {"Menu"} no encontrada");
            }
            if (audioSource != null)
            {
                audioSource.Play();
                Destroy(gameObject,audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
