using System.Collections;
using TMPro;
using UnityEngine;

public class PuntosPowerUp : MonoBehaviour
{
    [Header("UI")] public Canvas canvas;
    public TextMeshProUGUI puntosText;
    public TextMeshProUGUI puntosDinamicos;
    private void ActualizarDatos()
    {
        if (puntosText)
            puntosText.text = "Puntos: " + Datos.Instance.puntos.ToString();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void MostrarPuntosDinamicos(int puntos, Vector3 puntosMundoPowerUp)
    {
        if (!puntosDinamicos)
            return;
        puntosDinamicos.text = "+" + puntos;
        //Convertir mundo -> Pantalla
        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(puntosMundoPowerUp + Vector3.up * 0.5f + Vector3.right * 1.5f);
        // si el objeto está detrás de la cámara, no lo mostramos
        if (screenPos.z < 0f)
        {
            puntosDinamicos.gameObject.SetActive(false);
            return;
        }

        RectTransform rt = puntosDinamicos.rectTransform;
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            rt.position = screenPos;
        }
        else
        {
            RectTransform canvasRT = canvas.transform as RectTransform;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRT,
                screenPos,
                canvas.worldCamera, // IMPORTANTE: asigna la World Camera en el Canvas
                out localPoint
            );
            rt.anchoredPosition = localPoint;
        }

        puntosDinamicos.gameObject.SetActive(true);
        StartCoroutine(AnimarPuntosUI(rt));
    }


    private IEnumerator AnimarPuntosUI(RectTransform rt)
    {
        float duracion = 1f;
        float t = 0f;
        Vector2 start = rt.anchoredPosition;
        while (t < duracion)
        {
            t += Time.deltaTime;
            rt.anchoredPosition = start + new Vector2(0, t * 40f);
            yield return null;
        }

        puntosDinamicos.gameObject.SetActive(false);
    }

    public void AddPoints(int points)
    {
        Datos.Instance.puntos += points;
        ActualizarDatos();
    }

    void Start()
    {
        if (puntosDinamicos)
            puntosDinamicos.gameObject.SetActive(false);
        ActualizarDatos();
    }
}
