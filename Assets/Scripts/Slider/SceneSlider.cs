using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSlider : MonoBehaviour
{
    [Header("Escena a cargar")]
    public string sceneName;        // Nombre de la escena a la que quieres ir

    [Header("Referencia al Slider")]
    public Slider slider;          // Arrastra aquí el Slider desde el inspector

    [Header("Umbral para cambiar de escena")]
    [Range(0f, 1f)]
    public float threshold = 0.99f;  // Valor a partir del cual cambia de escena

    bool sceneLoaded = false;

    void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        // Empieza desde 0
        slider.value = 0f;

        // Escuchar cambios de valor del slider
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        if (!sceneLoaded && value >= threshold)
        {
            sceneLoaded = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}

