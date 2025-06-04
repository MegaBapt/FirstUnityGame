using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    private float originalTimeScale = 1f;
    private float originalFixedDeltaTime;
    private Rigidbody2D playerRigidbody;
    private Coroutine slowMotionCoroutine; // Pour gérer la coroutine
    private Animator animator;

    public static float SlowMotionFactor { get; private set; } = 1f; // Accessible pour compenser le mouvement

    void Start()
    {
        animator = GetComponent<Animator>();
        // Sauvegarder les valeurs par défaut
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;

        // Trouver le Rigidbody du joueur
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Aucun Rigidbody trouvé sur le joueur !");
        }
    }

    public void StartSlowMotion(float slowTimeScale, float slowDuration)
    {
        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime * slowTimeScale;

        // Double la vitesse du joueur pour compenser
        if (mouvement_joueur.instance != null)
        {
            mouvement_joueur.instance.moveSpeed /= slowTimeScale;
            mouvement_joueur.instance.jumpForce /= slowTimeScale;
            animator.SetFloat("AnimSpeed", 1/slowTimeScale);
        }
        StopSlowMotion(slowTimeScale, slowDuration);
    }

    public IEnumerator StopSlowMotion(float slowTimeScale, float slowDuration)
    {
        yield return new WaitForSecondsRealtime(slowDuration); // pas affecté par Time.timeScale
        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = originalFixedDeltaTime;

        // Remet la vitesse normale du joueur
        if (mouvement_joueur.instance != null)
        {
            mouvement_joueur.instance.moveSpeed *= slowTimeScale;
            mouvement_joueur.instance.jumpForce *= slowTimeScale;
            animator.SetFloat("AnimSpeed", 1);
        }
    }

    public void AddSpeed(int speedGiven, float speedDuration)
    {
        mouvement_joueur.instance.moveSpeed += speedGiven;
        StartCoroutine(RemoveSpeed(speedGiven, speedDuration));
    }

    private IEnumerator RemoveSpeed(int speedGiven, float speedDuration)
    {
        yield return new WaitForSeconds(speedDuration);
        mouvement_joueur.instance.moveSpeed -= speedGiven;
    }

    public void AddJump(int JumpGiven, float JumpDuration)
    {
        mouvement_joueur.instance.jumpForce += JumpGiven;
        StartCoroutine(RemoveSpeed(JumpGiven, JumpDuration));
    }

    private IEnumerator RemoveJump(int JumpGiven, float JumpDuration)
    {
        yield return new WaitForSeconds(JumpDuration);
        mouvement_joueur.instance.jumpForce -= JumpGiven;
    }
}