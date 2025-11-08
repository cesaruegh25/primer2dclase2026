using System;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [Header("Ajustes de movimiento")] [SerializeField]
    private int velocidad;

    [Header("Tiempo de vida")] [SerializeField]
    private int tiempoVida;

    private Rigidbody2D rb;


    [Header("Sonidos")] [SerializeField] private AudioSource sonidoDisparo;
    [SerializeField] private AudioSource sonidoExplosion;

    [Header("Efectos")] [SerializeField]    private GameObject efectoImpacto;

    public PuntosPowerUp powerUp;

    void Start()
    {
        if (sonidoDisparo != null)
            sonidoDisparo.Play();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * velocidad;
        powerUp=GetComponent<PuntosPowerUp>();


        Destroy(gameObject, tiempoVida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (efectoImpacto!=null)
                Instantiate(efectoImpacto, other.transform.position, Quaternion.identity);
            if (sonidoExplosion!=null)
                sonidoExplosion.Play();
            powerUp.AddPoints(other.gameObject.GetComponent<EnemyMove>().puntos);
            powerUp.MostrarPuntosDinamicos(other.gameObject.GetComponent<EnemyMove>().puntos,
                other.transform.position);
            Destroy(other.gameObject);
            Destroy(gameObject,3);
        }
    }
}