using System.Net.Mail;
using UnityEngine;

public class MovInimigo : MonoBehaviour
{
    [SerializeField] float forca = 5f;
    [SerializeField] int hp = 1;
    [SerializeField] GameObject dinheiroPrefab;
    private Rigidbody2D _rb;
    Vector2 direcao;
    private Transform alvo;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void AceleraInimigo(Transform alvo)
    {
        Vector2 direcao = (alvo.position - transform.position).normalized;
        _rb.AddForce(direcao * forca, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Player é a munição
        {
            Destroy(other.transform.parent.gameObject);

            if (hp == 1)
            {
                hp -= 1;
                Transform hpFull = transform.Find("HPFull");
                hpFull.gameObject.SetActive(false);
            }

            else if (hp <= 0)
            {
                GameObject dinheiro = Instantiate(dinheiroPrefab, transform.position, Quaternion.identity);
                SpawnReal spawnReal = dinheiro.GetComponent<SpawnReal>();

                if (CompareTag("X"))
                {
                    spawnReal.Spawn2Reais();
                }
                else if (CompareTag("X1"))
                {
                    spawnReal.Spawn5Reais();
                }

                Destroy(transform.root.gameObject);
            }
        }
    }

    public void DanoTouch(GameObject efeito)
    {
        GameObject effect = Instantiate(efeito, transform.position, Quaternion.identity, transform);
        
        Animator anim = effect.GetComponent<Animator>();
        Destroy(effect, 0.3f);

        if (hp == 1)
        {
            hp -= 1;
            Transform hpFull = transform.Find("HPFull");
            hpFull.gameObject.SetActive(false);
        }
        else if (hp <= 0)
        {
            GameObject dinheiro = Instantiate(dinheiroPrefab, transform.position, Quaternion.identity);
            SpawnReal spawnReal = dinheiro.GetComponent<SpawnReal>();

            Destroy(transform.root.gameObject, 0.1f);

            if (CompareTag("X"))
                spawnReal.Spawn2Reais();
            else if (CompareTag("X1"))
                spawnReal.Spawn5Reais();
        }
    }

}
