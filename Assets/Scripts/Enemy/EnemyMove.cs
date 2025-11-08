using System;
using UnityEngine;
////
public class EnemyMove : MonoBehaviour
{
    [Header("Posiciones")]
    [SerializeField] private Transform puntoA;     // visible en Inspector
    [SerializeField] private Transform puntoB;     // hijo de Grid

    [Header("Velocidad")]
    [SerializeField] private float velocidad = 2f;

    [Header("Puntos")]
    public int puntos;

    [SerializeField] private bool yendoHaciaB = true;

    // Umbral de llegada en plano (X/Y)
    [SerializeField] private float distanciaUmbral = 0.02f;

    private void Awake()
    {
        // Si no hay A, lo creamos en la posición inicial del enemigo
        if (puntoA == null)
        {
            var a = new GameObject("PuntoA_" + name);
            a.transform.position = transform.position;

            // Si B existe y es hijo del Grid, parentamos A al mismo padre para mantener coherencia
            if (puntoB != null)
                a.transform.SetParent(puntoB.parent, true); // true = conserva posición en mundo

            puntoA = a.transform;
        }
    }

    private void Update()
    {
        if (puntoB == null || puntoA == null)
        {
            Debug.LogWarning($"{name}: Falta asignar puntoA/puntoB en EnemyMove");
            return;
        }

        // Destino en mundo, forzando el mismo Z que el enemigo para evitar “no llego nunca”
        Vector3 destino = (yendoHaciaB ? puntoB.position : puntoA.position);
        destino.z = transform.position.z;

        // Movimiento
        Vector3 nuevaPos = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
        transform.position = nuevaPos;

        // Distancia en plano (X/Y) con tolerancia
        Vector2 posXY = new Vector2(transform.position.x, transform.position.y);
        Vector2 destXY = new Vector2(destino.x, destino.y);
        if ((posXY - destXY).sqrMagnitude <= distanciaUmbral * distanciaUmbral)
        {
            yendoHaciaB = !yendoHaciaB;
        }
    }
}
