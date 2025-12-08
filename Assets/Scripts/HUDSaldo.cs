using TMPro;
using UnityEngine;

public class HUDSaldo : MonoBehaviour
{
    public TextMeshProUGUI textoMortos;
    public TextMeshProUGUI saldoText;
    public TextMeshProUGUI precoCooldown;
    public TextMeshProUGUI precoDouble;
    public TextMeshProUGUI precoVida;
    public int saldo = 0;

    void Start()
    {
        AtualizarTexto();
    }

    public void AdicionarSaldo(int valor)
    {
        saldo += valor;
        AtualizarTexto();
    }

    public void AtualizarTexto()
    {
        saldoText.text = "Saldo: " + saldo + " Reais";
    }

    public bool TemSaldo(int valor)
    {
        return saldo >= valor;
    }

    public void RemoverSaldo(int valor)
    {
        saldo -= valor;
        saldo = Mathf.Max(saldo, 0);
        AtualizarTexto();
    }

    public void AtualizarPrecoCooldown(int novoValor)
    {
        precoCooldown.text = "R$ " + novoValor;
    }

    public void AtualizarPrecoVida(int novoValor)
    {
        precoVida.text = "R$ " + novoValor;
    }

    public void AtualizarPrecoDouble(int novoValor)
    {
        precoDouble.text = "R$ " + novoValor;
    }
    
    public void AtualizarTextoMortos(int? onda = null)
    {
        string linhaOnda;

        if (onda == null)
        {
            // ➜ Mantém a linha da onda existente
            string[] linhas = textoMortos.text.Split('\n');

            // Caso o texto exista e tenha a linha da onda
            if (linhas.Length > 0)
                linhaOnda = linhas[0];
            else
                linhaOnda = "Onda 1"; // fallback
        }
        else
        {
            linhaOnda = $"Onda {onda}";
        }

        // Atualiza o texto final
        textoMortos.text = $"{linhaOnda}\nMortos: {GlobalStats.TotalKills}";
    }
}
