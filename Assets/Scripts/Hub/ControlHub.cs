using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlHub : MonoBehaviour
{

    public GameObject panelPrincipal;
    public GameObject panelCreditos;
    public GameObject panelIntermedio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI puntos;
    void Start()
    {
        if (Datos.Instance == null)
        {
            panelPrincipal.SetActive(true);
            panelCreditos.SetActive(false);
            panelIntermedio.SetActive(false);
        }
        else
        {
            panelPrincipal.SetActive(false);
            panelCreditos.SetActive(false);
            panelIntermedio.SetActive(true);
            Debug.Log(Datos.Instance.puntos.ToString());
            puntos.text =$"Puntos: {Datos.Instance.puntos.ToString()}";
        }
    }

    public void SiguienteNivel()
    {
        Load("Nivel2");
    }

    public void Creditos()
    {
        panelPrincipal.SetActive(false);
        panelCreditos.SetActive(true);
        panelIntermedio.SetActive(false);
    }


    public void SalirCreditos()
    {
        panelPrincipal.SetActive(true);
        panelCreditos.SetActive(false);
        panelIntermedio.SetActive(false);
    }
    public void Load(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        else
            Debug.LogError($"Escena {sceneName} no encontrada");
    }

    public void SalirJuego()
    {
        Debug.Log("Salir Juego");
        Application.Quit();
    }
}
