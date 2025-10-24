using UnityEngine;

public class SpawnReal : MonoBehaviour
{
    private HUDSaldo hudSaldo;
    void Awake()
    {
        GameObject saldoObj = GameObject.FindGameObjectWithTag("HUD");
        hudSaldo = saldoObj.GetComponent<HUDSaldo>();
    }
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

    public void Spawn2Reais()
    {
        hudSaldo.AdicionarSaldo(2);
    }

    public void Spawn5Reais()
    {
        hudSaldo.AdicionarSaldo(5);
    }

}