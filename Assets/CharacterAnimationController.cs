using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAnimationController : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private Animator animator;
    [SerializeField] private float smoothSpeed = 6f;

    private float targetBlend = 0f;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        if (animator == null || animator.runtimeAnimatorController == null) return;

        // Leitura de teclado (Novo Input System)
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.digit1Key.wasPressedThisFrame || keyboard.numpad1Key.wasPressedThisFrame || keyboard.jKey.wasPressedThisFrame)
            {
                targetBlend = 0f;
            }
            else if (keyboard.digit2Key.wasPressedThisFrame || keyboard.numpad2Key.wasPressedThisFrame || keyboard.kKey.wasPressedThisFrame)
            {
                targetBlend = 1f;
            }
            else if (keyboard.digit3Key.wasPressedThisFrame || keyboard.numpad3Key.wasPressedThisFrame || keyboard.lKey.wasPressedThisFrame)
            {
                targetBlend = 2f;
            }
        }

        // Interpolação suave
        float currentBlend = animator.GetFloat("BlendState");
        float nextBlend = Mathf.MoveTowards(currentBlend, targetBlend, Time.deltaTime * smoothSpeed);
        animator.SetFloat("BlendState", nextBlend);
    }

    // Interface gráfica na tela: funciona com clique de mouse independente do teclado
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 240, 210), GUI.skin.box);
        GUILayout.Label("<b>Controle de Animação</b>");

        if (animator != null)
        {
            GUILayout.Label($"BlendState Atual: {animator.GetFloat("BlendState"):F2}");
            GUILayout.Label($"Alvo: {targetBlend}");
        }
        else
        {
            GUILayout.Label("<color=red>Animator não encontrado!</color>");
        }

        GUILayout.Space(10);

        if (GUILayout.Button("1: Crouch To Stand (0)"))
        {
            targetBlend = 0f;
        }
        if (GUILayout.Button("2: Dying (1)"))
        {
            targetBlend = 1f;
        }
        if (GUILayout.Button("3: Reaction (2)"))
        {
            targetBlend = 2f;
        }

        GUILayout.EndArea();
    }
}