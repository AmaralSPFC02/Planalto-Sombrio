using UnityEngine;
using UnityEngine.SceneManagement;

public class TorreDetect : MonoBehaviour
{
    [SerializeField] private Sprite[] barraSprites;
    [SerializeField] private int vidaMax = 6;

    private int vidaAtual;
    private SpriteRenderer barraRenderer;

    void Start()
    {
        vidaAtual = vidaMax;

        Transform barra = transform.Find("BarraVida");
        barraRenderer = barra.GetComponent<SpriteRenderer>();
        AtualizarSprite();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.StartsWith("X"))
        {
            TomarDano(1);
            Destroy(other.transform.parent.gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            TomarDano(5);
            Destroy(other.transform.parent.gameObject);
        }
    }

    void TomarDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMax);
        AtualizarSprite();

        if (vidaAtual <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RecuperarVida(int valor)
    {
        if (vidaAtual >= vidaMax)
            return;

        vidaAtual += valor;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMax);
        AtualizarSprite();
    }

    void AtualizarSprite()
    {
        if (barraRenderer != null && barraSprites.Length >= vidaMax + 1)
        {
            barraRenderer.sprite = barraSprites[vidaAtual];
        }
    }

    public bool TorreCheia()
    {
        return vidaAtual >= vidaMax;
    }
}