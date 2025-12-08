using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TorreManager : MonoBehaviour
{
    [SerializeField] private TorreDetect torre;
    [SerializeField] private AreaDetect areaDetect;
    [SerializeField] private Transform posicoesAtiradores;
    [SerializeField] private HUDSaldo hudSaldo;
    [SerializeField] private List<Transform> posiçoes;
    [SerializeField] private List<GameObject> AtiradorPrefabs;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private AudioClip soundEffectClique;
    [SerializeField] private int custoPorVida = 5;
    [SerializeField] private int custoUpgradeVelocidade = 20;
    [SerializeField] private int custoDouble = 50;
    [SerializeField] private float multiplicadorCusto = 1.5f;
    [SerializeField] private float reducaoPercentual = 0.8f;
    [SerializeField] private float cooldownMinimo = 0.5f;
    [SerializeField] private float tempoCooldownClique = 1f;
    [SerializeField] private Cooldown barraCooldown;
    private int nivelUpgrade = 0;
    private int nivelDouble = 0;

    void Start()
    {
        AtualizarPrecosHUD();
    }

    public void CurarTorreButton()
    {
        int preco = AjustarPreco(custoPorVida);
        if (hudSaldo.TemSaldo(preco) && !torre.TorreCheia())
        {
            hudSaldo.RemoverSaldo(preco);
            torre.RecuperarVida(1);
        }
    }
    public void AumentarVelocidadeDisparoButton()
    {
        int preco = AjustarPreco(custoUpgradeVelocidade);
        if (!hudSaldo.TemSaldo(preco))
            return;

        hudSaldo.RemoverSaldo(preco);

        foreach (Transform filho in posicoesAtiradores)
        {
            if (filho.CompareTag("Atirador"))
            {
                AreaDetect area = filho.GetComponent<AreaDetect>();
                if (area != null)
                    area.ReduzirCooldown(reducaoPercentual, cooldownMinimo);
            }
        }

        nivelUpgrade++;
        custoUpgradeVelocidade = Mathf.CeilToInt(custoUpgradeVelocidade * multiplicadorCusto);
        hudSaldo.AtualizarPrecoCooldown(AjustarPreco(custoUpgradeVelocidade));
    }

    public void OnExit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DoubleButton()
    {
        if (nivelDouble >= 2)
            return;

        int preco = AjustarPreco(custoDouble);
        if (!hudSaldo.TemSaldo(preco))
            return;

        hudSaldo.RemoverSaldo(preco);
        nivelDouble++;
        custoDouble = Mathf.CeilToInt(custoDouble * multiplicadorCusto);

        AtualizarPrecosHUD();
        Transform posInstanciar = posiçoes[nivelDouble - 1];

        Instantiate(AtiradorPrefabs[nivelDouble - 1], posInstanciar.position, Quaternion.identity);
    }

    private void AtualizarPrecosHUD()
    {
        hudSaldo.AtualizarPrecoVida(AjustarPreco(custoPorVida));
        hudSaldo.AtualizarPrecoCooldown(AjustarPreco(custoUpgradeVelocidade));
        hudSaldo.AtualizarPrecoDouble(AjustarPreco(custoDouble));
    }

    private int AjustarPreco(int precoBase)
    {
        return Mathf.CeilToInt(precoBase * (nivelDouble + 1));
    }

    void OnClique(InputValue value)
    {   
        if (barraCooldown.EmCooldown())
        {
            return;
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.tag.StartsWith("X"))
        {
            AudioSource.PlayClipAtPoint(soundEffectClique, Camera.main.transform.position, 0.5f);
            MovInimigo inimigo = hit.collider.GetComponentInParent<MovInimigo>();
            inimigo.DanoTouch(hitEffectPrefab);
            barraCooldown.IniciarCooldown(tempoCooldownClique);
        }
    }
}
