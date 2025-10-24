using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sr;

    private float tempoCooldown = 0f;
    private float tempoRestante = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (tempoRestante > 0f)
        {
            tempoRestante -= Time.deltaTime;
            float progresso = 1f - (tempoRestante / tempoCooldown);
            AtualizarSprite(progresso);
        }
    }
    public void IniciarCooldown(float cooldown)
    {
        tempoCooldown = cooldown;
        tempoRestante = cooldown;
        AtualizarSprite(0f);
    }

    private void AtualizarSprite(float progresso)
    {
        if (sprites.Length == 0 || sr == null) return;

        int index = Mathf.Clamp(Mathf.FloorToInt(progresso * (sprites.Length - 1)), 0, sprites.Length - 1);
        sr.sprite = sprites[index];
    }

    public bool EmCooldown() => tempoRestante > 0f;
}
