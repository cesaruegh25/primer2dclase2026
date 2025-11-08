using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{

    [Header("Componentes")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private PlayerMove2 playerMove;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove2>();
    }

    public void AnimacionMuerte()
    {
        animator.SetTrigger("Muerte");
    }

    public void AnimacionVida()
    {
        animator.SetTrigger("VidaNueva");
    }


    public void AnimacionDisparo()
    {
        animator.ResetTrigger("Disparar");
        animator.SetTrigger("Disparar");
    }
    public void OnMove(InputValue value)
    {
        animator.SetFloat("x",value.Get<Vector2>().x);
        Debug.Log(value.Get<Vector2>().x);
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("y",rb.linearVelocity.y);
        animator.SetBool("enSuelo",playerMove.EnSuelo());
    }
}
