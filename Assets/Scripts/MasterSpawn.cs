using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterSpawn : MonoBehaviour
{
    enum EstadoSpawn { Spawning, EsperandoBoss, BossAtivo, Finalizado }

    [SerializeField] private List<GameObject> filhos;
    [SerializeField] private Transform alvo;
    [SerializeField] private List<GameObject> bosses;
    [SerializeField] private List<Transform> bossSpawnPoint;
    [SerializeField] private int spawnsOndaInicial = 50;
    [SerializeField] private int maximoOndas = 2;
    [SerializeField] private float intervaloInicial = 1.5f;
    [SerializeField] private float intervaloMinimo = 0.1f;
    [SerializeField] private float reducaoPercentual = 0.95f;
    [SerializeField] private float tempoParaAumentarDificuldade = 5f;
    private EstadoSpawn estadoAtual = EstadoSpawn.Spawning;
    private float proximoSpawn = 0f;
    private float intervaloAtual;
    private float contadorDificuldade = 0f;
    private int ondaAtual = 1;
    private int totalSpawnsDaOnda;
    private int killsNoInicioDaOnda;
    private int killsNecessariasParaBoss;

    void Start()
    {
        intervaloAtual = intervaloInicial;
        totalSpawnsDaOnda = spawnsOndaInicial;
        killsNoInicioDaOnda = GetKillCount();
        killsNecessariasParaBoss = killsNoInicioDaOnda + totalSpawnsDaOnda;
    }

    void Update()
    {
        if (estadoAtual == EstadoSpawn.Finalizado) return;

        switch (estadoAtual)
        {
            case EstadoSpawn.Spawning:
                AtualizarSpawnInimigos();
                VerificarFimDaOnda();
                break;

            case EstadoSpawn.EsperandoBoss:
                SpawnBossDaOnda();
                break;

            case EstadoSpawn.BossAtivo:
                VerificarBossMorto();
                break;
        }
    }
    void AtualizarSpawnInimigos()
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
    void VerificarFimDaOnda()
    {
        int killsAtuais = GetKillCount();

        if (killsAtuais >= killsNecessariasParaBoss)
        {
            estadoAtual = EstadoSpawn.EsperandoBoss;
        }
    }

    void SpawnBossDaOnda()
    {
        if (ondaAtual - 1 < bosses.Count)
        {
            estadoAtual = EstadoSpawn.BossAtivo;
            var x = 0;
            while (x < ondaAtual+1)
            {
                GameObject bossPrefab = bosses[ondaAtual-1];
                GameObject boss = Instantiate(bossPrefab, bossSpawnPoint[x].position, Quaternion.identity);
                MovBoss bossScript = boss.GetComponentInChildren<MovBoss>();
                bossScript.AceleraBoss(alvo);
                x++;
            }
        }
    }

    void VerificarBossMorto()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss == null)
        {
            AvancarParaProximaOnda();
        }
    }

    void AvancarParaProximaOnda()
    {
        ondaAtual++;
        HUDSaldo hud = FindFirstObjectByType<HUDSaldo>();
        hud.AtualizarTextoMortos(ondaAtual);

        if (ondaAtual > maximoOndas)
        {
            estadoAtual = EstadoSpawn.Finalizado;
            SceneManager.LoadScene("Vitoria");
            return;
        }

        totalSpawnsDaOnda *= 2;

        killsNoInicioDaOnda = GetKillCount();
        killsNecessariasParaBoss = killsNoInicioDaOnda + totalSpawnsDaOnda;

        // Resetar variáveis
        intervaloAtual = Mathf.Max(intervaloMinimo, intervaloInicial / (1 + ondaAtual * 0.3f));
        contadorDificuldade = 0f;
        proximoSpawn = 0f;

        estadoAtual = EstadoSpawn.Spawning;
    }
    int GetKillCount()
    {
        return GlobalStats.TotalKills;
    }
}
