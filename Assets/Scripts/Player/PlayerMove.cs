using System;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove2 : MonoBehaviour
{
    [Header("Movimiento")] [SerializeField]
    private float velocidadMovimiento = 6f;

    private Vector2 entradaMovimiento;

    public Rigidbody2D rb;

    private SpriteRenderer sprite;

    public bool mirandoDerecha = true;

    [Header("Salto")] [SerializeField] private float fuerzaSalto = 7f;

    public bool enSuelo = true;


    public float y;


    [Header("Sonidos")] [SerializeField] private AudioSource sonidoSalto;
    [SerializeField] private AudioSource sonidoAndar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();
    }


    public bool EnSuelo()
    {
        return enSuelo;
    }

    public void OnJump(InputValue valor)
    {
        if (!enSuelo)
            return;
        if (!sonidoSalto.isPlaying)
            sonidoSalto.Play();
        var v = rb.linearVelocity;
        v.y = 0f;
        rb.linearVelocity = v;
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }

    public void OnMove(InputValue valor)
    {
        entradaMovimiento = valor.Get<Vector2>();
        if (entradaMovimiento.x > 0 && !mirandoDerecha)
            Girar(false);
        else if (entradaMovimiento.x < 0 && mirandoDerecha)
            Girar(true);
    }


    private void FixedUpdate()
    {
        var v = rb.linearVelocity;
        v.x = entradaMovimiento.x * velocidadMovimiento;
        rb.linearVelocity = v;
    }

    // Update is called once per frame
    void Update()
    {
        bool estaAndando = Mathf.Abs(entradaMovimiento.x) > 0.1 && enSuelo;
        if (estaAndando)
        {
            if (!sonidoAndar.isPlaying)
                sonidoAndar.Play();
        }
        else
        {
            if (sonidoAndar.isPlaying)
                sonidoAndar.Stop();
        }
    }


    private void Girar(bool aIzquierda)
    {
        mirandoDerecha = !mirandoDerecha;
        if (sprite)
            sprite.flipX = aIzquierda;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var v = rb.linearVelocity;
        if (other.gameObject.CompareTag("Suelo") && v.y > 1)
        {
            enSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var v = rb.linearVelocity;
        if (other.gameObject.CompareTag("Suelo") && v.y < 1)
        {
            enSuelo = true;
        }
    }
}