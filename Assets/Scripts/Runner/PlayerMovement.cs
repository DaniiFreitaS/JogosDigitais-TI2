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

    // --- Animator ---
    [Header("Animação")]
    public Animator animator;

    public AnimationClip animacaoLanding;
    public AnimationClip animacaoHit;

    private PlayableGraph graph;
    private AnimationClipPlayable clipPlayable;
    private AnimationPlayableOutput outputPlayable;

    private bool estavaNoChao = false;

    // Mobile
    private bool tapCima = false;
    private bool segurarBaixo = false;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();

        // PlayableGraph apenas para Landing/Hit
        if (animator != null)
        {
            graph = PlayableGraph.Create("GraphEventos");
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            outputPlayable = AnimationPlayableOutput.Create(graph, "SaidaAnim", animator);
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
        segurarBaixo = false;

        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            float meio = Screen.width * 0.5f;

            // TAP para pular
            if (toque.phase == TouchPhase.Began && toque.position.x < meio)
                tapCima = true;

            // SEGURAR na direita para descer
            if (toque.position.x > meio &&
                (toque.phase == TouchPhase.Stationary || toque.phase == TouchPhase.Moved))
                segurarBaixo = true;
        }
    }

    // -------------------- Movement --------------------
    public void Mover()
    {
        // Aceleração horizontal
        if (velocidadeAtual < velocidadeMax)
        {
            velocidadeAtual += aceleracao * Time.deltaTime;
            velocidadeAtual = Mathf.Min(velocidadeAtual, velocidadeMax);
        }
        movimento.x = velocidadeAtual;

        // Movimento vertical
        if (cc.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) || tapCima)
                movimento.y = Mathf.Sqrt(2 * gravidade * alturaPulo);
            else
                movimento.y = -1f; // manter contato com o chão
        }
        else
        {
            // descida rápida
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || segurarBaixo)
                movimento.y -= gravidade * 5f * Time.deltaTime;
            else
                movimento.y -= gravidade * Time.deltaTime;
        }

        cc.Move(movimento * Time.deltaTime);
    }

    // -------------------- Animation Control --------------------
    void AtualizarAnimacao()
    {
        if (animator == null) return;

        bool noChao = cc.isGrounded;

        // Corrida automática em loop: não precisamos mexer no Speed
        // Apenas Landing via PlayableGraph quando toca o chão
        if (noChao && !estavaNoChao && animacaoLanding != null)
            TocarAnimacao(animacaoLanding);

        estavaNoChao = noChao;
    }

    void TocarAnimacao(AnimationClip clip)
    {
        if (clip == null || !graph.IsValid()) return;

        clipPlayable = AnimationClipPlayable.Create(graph, clip);
        clipPlayable.SetApplyFootIK(true);
        clipPlayable.SetDuration(double.PositiveInfinity);
        outputPlayable.SetSourcePlayable(clipPlayable);

        clipPlayable.SetTime(0);
        clipPlayable.SetSpeed(1);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy") && animacaoHit != null)
            TocarAnimacao(animacaoHit);
    }

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
