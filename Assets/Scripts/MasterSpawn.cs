using System.Collections.Generic;
using UnityEngine;

public class MasterSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> filhos;
    [SerializeField] private Transform alvo;
    [SerializeField] private float intervaloInicial = 2f;
    [SerializeField] private float intervaloMinimo = 0.5f;
    [SerializeField] private float reducaoPercentual = 0.95f;
    [SerializeField] private float tempoParaAumentarDificuldade = 5f;

    private float proximoSpawn = 0f;
    private float intervaloAtual;
    private float contadorDificuldade = 0f;

    void Start()
    {
        intervaloAtual = intervaloInicial;
    }

    void Update()
    {
        proximoSpawn -= Time.deltaTime;
        contadorDificuldade += Time.deltaTime;

        if (proximoSpawn <= 0f)
        {
            SpawnRandom();
            proximoSpawn = intervaloAtual;
        }

        if (contadorDificuldade >= tempoParaAumentarDificuldade)
        {
            contadorDificuldade = 0f;
            AumentarDificuldade();
        }
    }

    void SpawnRandom()
    {
        int index = Random.Range(0, filhos.Count);
        GameObject filhoSelecionado = filhos[index];

        var script = filhoSelecionado.GetComponent<SpawnInimigo>();
        script.Spw(alvo);
    }

    void AumentarDificuldade()
    {
        intervaloAtual = Mathf.Max(intervaloMinimo, intervaloAtual * reducaoPercentual);
    }
}
