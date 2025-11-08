using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparo : MonoBehaviour
{
    [Header("Disparo")] [SerializeField] private GameObject prefabBala;
    [SerializeField] private Transform puntoDisoparoDerecha;
    [SerializeField] private Transform puntoDisoparoIzquierda;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerMove2 playerMove;

    private PlayerAnimations playerAnimation;
    void Start()
    {
        playerMove = GetComponent<PlayerMove2>();
        playerAnimation = GetComponent<PlayerAnimations>();
    }

    public void OnDisparar(InputValue valor)
    {
        if (!valor.isPressed)
            return;
        if (prefabBala == null || puntoDisoparoDerecha  == null || puntoDisoparoIzquierda == null)
            return;
        if (playerMove.enSuelo)
            StartCoroutine("CoorDisparo");

    }

    private IEnumerator CoorDisparo()
    {
        playerAnimation.AnimacionDisparo();
        yield return new WaitForSecondsRealtime(0.5f);
        if (playerMove.mirandoDerecha)
            Instantiate(prefabBala,puntoDisoparoDerecha.position,puntoDisoparoDerecha.rotation);
        else
            Instantiate(prefabBala,puntoDisoparoIzquierda.position,puntoDisoparoIzquierda.rotation);
    }

}
