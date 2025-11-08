using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private float tiempoEspera;
    public PlayerMove2 playerMove;
    private PlayerAnimations playerAnimation;
    [SerializeField] private AudioSource audioMuerte;

    private bool inmune = false;

    private VidasJugador playerLifes;

    void Start()
    {
        playerMove = GetComponent<PlayerMove2>();
        playerAnimation = GetComponent<PlayerAnimations>();
        playerLifes = FindFirstObjectByType<VidasJugador>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            if (!inmune)
                StartCoroutine(PararYReiniciar());
        }
    }

    private IEnumerator PararYReiniciar()
    {
        //  Time.timeScale = 0;
        inmune = true;
        Datos.Instance.vidas--;
        playerLifes.LoseLife();
        audioMuerte.Play();
        playerAnimation.AnimacionMuerte();
        yield return new WaitForSecondsRealtime(tiempoEspera);
        inmune = false;
        playerAnimation.AnimacionVida();
        if (Datos.Instance.vidas <= 0)
        {
            Datos.Instance.vidas=3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}