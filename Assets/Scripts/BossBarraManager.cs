using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossBarraManager : MonoBehaviour
{
    [SerializeField] private Sprite[] barraBossSprites;
    [SerializeField] private int vidaMax = 8;
    private int vidaAtual;
    private SpriteRenderer barraRenderer;
    [SerializeField] private SpriteRenderer barra;

    void Start()
    {
        vidaAtual = vidaMax;
        barraRenderer = barra.GetComponent<SpriteRenderer>();
        AtualizarSprite();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TomarDano(1);
            Destroy(other.transform.parent.gameObject);
        }
    }

    void TomarDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMax);
        AtualizarSprite();
    }

    void AtualizarSprite()
    {
        if (barraRenderer != null && barraBossSprites.Length >= vidaMax + 1)
        {
            barraRenderer.sprite = barraBossSprites[vidaAtual];
        }
    }
}