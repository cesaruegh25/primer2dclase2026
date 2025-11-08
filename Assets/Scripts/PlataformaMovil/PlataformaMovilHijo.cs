using System;
using UnityEngine;

public class PlataformaMovilHijo : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        other.transform.SetParent(transform);
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        other.transform.SetParent(null);
    }
}
