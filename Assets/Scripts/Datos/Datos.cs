using System.Collections;
using TMPro;
using UnityEngine;

public class Datos : MonoBehaviour
{
    public int vidas;
    public static Datos Instance;
    public int puntos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}