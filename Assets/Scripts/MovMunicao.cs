using Unity.VisualScripting;
using UnityEngine;

public class MovMunicao : MonoBehaviour
{
    [SerializeField] private float forca = 8f;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject Efeitoprefab;

    private Rigidbody2D _rb;
    private Transform alvo;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void AceleraAlvo(Transform alvo)
    {
        Vector2 direcao = (alvo.position - transform.position).normalized;
        _rb.AddForce(direcao * forca, ForceMode2D.Impulse);
    }

    public void ColidiuSound()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
    void OnBecameInvisible()
    {
        Destroy(transform.root.gameObject);
    }
}
