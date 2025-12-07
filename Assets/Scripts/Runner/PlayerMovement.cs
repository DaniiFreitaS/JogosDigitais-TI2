using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // --- Config Movement ---
    public float velocidadeMax = 14f;
    public float aceleracao = 1f;
    public float alturaPulo = 6f;
    public float gravidade = 140f;

    private CharacterController cc;
    private Vector3 movimento;
    private float velocidadeAtual = 0f;

    // --- Animation ---
    [Header("Animação")]
    public Animator animator;

    public AnimationClip animacaoCorrida;
    public AnimationClip animacaoLanding;
    public AnimationClip animacaoHit;

    private PlayableGraph graph;
    private AnimationClipPlayable clipPlayable;
    private AnimationPlayableOutput outputPlayable;

    private bool estavaNoChao = false;

    // Mobile
    private bool tapCima = false;
    private bool tapBaixo = false;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        if (animator != null && animacaoCorrida != null)
        {
            graph = PlayableGraph.Create("GraphCorrida");
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

            clipPlayable = AnimationClipPlayable.Create(graph, animacaoCorrida);
            clipPlayable.SetApplyFootIK(true);
            clipPlayable.SetDuration(double.PositiveInfinity);

            outputPlayable = AnimationPlayableOutput.Create(graph, "SaidaAnim", animator);
            outputPlayable.SetSourcePlayable(clipPlayable);

            graph.Play();
        }
    }

    private void Update()
    {
        DetectarInputMobile();
        Mover();
        AtualizarAnimacao();
    }

    // -------------------- Mobile --------------------
    void DetectarInputMobile()
    {
        tapCima = false;
        tapBaixo = false;

        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                float meio = Screen.width * 0.5f;

                if (toque.position.x < meio)
                    tapCima = true;
                else
                    tapBaixo = true;
            }
        }
    }

    // -------------------- Movement --------------------
    public void Mover()
    {
        if (velocidadeAtual < velocidadeMax)
        {
            velocidadeAtual += aceleracao * Time.deltaTime;
            velocidadeAtual = Mathf.Min(velocidadeAtual, velocidadeMax);
        }

        movimento.x = velocidadeAtual;

        if (cc.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) || tapCima)
            {
                movimento.y = Mathf.Sqrt(2 * gravidade * alturaPulo);
            }
            else
            {
                movimento.y = -1f;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || tapBaixo)
                movimento.y -= gravidade * 5f * Time.deltaTime;
            else
                movimento.y -= gravidade * Time.deltaTime;
        }

        cc.Move(movimento * Time.deltaTime);
    }

    // -------------------- Animation Control --------------------
    void TocarAnimacao(AnimationClip clip)
    {
        if (clip == null) return;

        clipPlayable = AnimationClipPlayable.Create(graph, clip);
        clipPlayable.SetApplyFootIK(true);
        clipPlayable.SetDuration(double.PositiveInfinity);

        outputPlayable.SetSourcePlayable(clipPlayable);
        clipPlayable.SetTime(0);
        clipPlayable.SetSpeed(1);
    }

    void AtualizarAnimacao()
    {
        if (animacaoCorrida == null)
            return;

        bool noChao = cc.isGrounded;

        // --- Tocou o chão (LANDING) ---
        if (noChao && !estavaNoChao)
        {
            TocarAnimacao(animacaoLanding ?? animacaoCorrida);
        }

        // --- No chão = animação normal ---
        if (noChao)
        {
            clipPlayable.SetSpeed(1);
        }
        else
        {
            clipPlayable.SetSpeed(0); // congela no ar
        }

        estavaNoChao = noChao;
    }

    // -------------------- Colisão Enemy --------------------
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            TocarAnimacao(animacaoHit);
        }
    }

    // -------------------- Reset --------------------
    public void ResetPos()
    {
        Debug.Log("Reset de posição");
        cc.enabled = false;
        Vector3 reset = transform.position;
        reset.y = 20f;
        transform.position = reset;
        cc.enabled = true;
    }

    private void OnDestroy()
    {
        if (graph.IsValid())
            graph.Destroy();
    }
}
