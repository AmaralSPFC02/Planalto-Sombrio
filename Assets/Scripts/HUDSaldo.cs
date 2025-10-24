using TMPro;
using UnityEngine;

public class HUDSaldo : MonoBehaviour
{
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
}
