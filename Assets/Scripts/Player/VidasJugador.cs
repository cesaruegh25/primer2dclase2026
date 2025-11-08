using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasJugador : MonoBehaviour
{
    [Header("Imágenes")] [SerializeField] private List<Image> hearts = new();

    [Header("Config")] [SerializeField] private int maxLives = 3;

    [SerializeField] private int currentLives;

    private void Awake()
    {
        SetLives(maxLives);
    }

    public void SetLives(int lives)
    {
        currentLives = lives;
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i >= maxLives)
            {
                hearts[i].gameObject.SetActive(false);
            }
            else
            {
                hearts[i].gameObject.SetActive(i<currentLives);
            }
        }
    }

    public void LoseLife(int amount = 1)
    {
        currentLives-=amount;
        SetLives(currentLives);
    }


    public void GainLife(int amount = 1)
    {
        currentLives+=amount;
        SetLives(currentLives);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}