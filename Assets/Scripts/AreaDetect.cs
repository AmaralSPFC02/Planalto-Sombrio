using System.Collections.Generic;
using UnityEngine;

public class AreaDetect : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<GameObject> municaoPrefabs;
    [SerializeField] private float cooldownTime = 2f;

    private float nextSpawnTime = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.StartsWith("X") && Time.time >= nextSpawnTime)
        {
            Transform alvoMaisProximo = GetAlvoMaisProximo();

            if (alvoMaisProximo != null)
            {
                SpawnMuni(alvoMaisProximo);
            }
        }
    }

    private Transform GetAlvoMaisProximo()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<Collider2D>().bounds.size.x / 2);
        Transform alvoMaisProximo = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (collider.tag.StartsWith("X"))
            {
                float distancia = Vector2.Distance(transform.position, collider.transform.position);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    alvoMaisProximo = collider.transform;
                }
            }
        }

        return alvoMaisProximo;
    }

    private void SpawnMuni(Transform alvo)
    {
        nextSpawnTime = Time.time + cooldownTime;

        int randomIndex = Random.Range(0, municaoPrefabs.Count);
        GameObject muni = Instantiate(municaoPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        MovMunicao muniScript = muni.GetComponentInChildren<MovMunicao>();
        muniScript.AceleraAlvo(alvo);
        muniScript.ColidiuSound();
    }

    public float GetCooldown()
    {
        return cooldownTime;
    }

    public void ReduzirCooldown(float percentual, float minimo)
    {
        cooldownTime = Mathf.Max(minimo, cooldownTime * percentual);
    }
}
