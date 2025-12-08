using UnityEngine;

public class MovBoss : MonoBehaviour
{
    [SerializeField] private float forca = 0.5f;
    private Transform alvo;
    [SerializeField] private Sprite[] barraSprites;
    [SerializeField] private int vidaMax = 8;
    private int vidaAtual;
    private SpriteRenderer barraRenderer;

    [SerializeField] private GameObject dinheiroPrefab;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        vidaAtual = vidaMax;
        Transform barra = transform.Find("BarraBoss");
        barraRenderer = barra.GetComponent<SpriteRenderer>();
        AtualizarSprite();
    }

    public void AceleraBoss(Transform alvo)
    {
        this.alvo = alvo;
        Vector2 direcao = (alvo.position - transform.position).normalized;
        _rb.AddForce(direcao * forca, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TomarDano(1);
            Destroy(other.transform.parent.gameObject);
        }
    }

    private void TomarDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMax);
        AtualizarSprite();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void AtualizarSprite()
    {
        if (barraRenderer != null && barraSprites.Length > vidaAtual)
        {
            barraRenderer.sprite = barraSprites[vidaAtual];
        }
    }

    private void Morrer()
    {

        if (dinheiroPrefab != null)
        {
            GameObject dinheiro = Instantiate(dinheiroPrefab, transform.position, Quaternion.identity);
            SpawnReal spawnReal = dinheiro.GetComponent<SpawnReal>();

            if (CompareTag("Boss"))
                spawnReal.Spawn5Reais(); // lembrar de ajustar o valor, sprite de R$100
        }

        Destroy(transform.root.gameObject, 0.1f);
    }
}