using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour
{
    [SerializeField] List<GameObject> inimigoPrefabs;
    public void Spw(Transform alvo)
    {
        int randomIndex = Random.Range(0, inimigoPrefabs.Count);
        GameObject ini = Instantiate(inimigoPrefabs[randomIndex], gameObject.transform.position, Quaternion.identity);
        MovInimigo iniScript = ini.GetComponentInChildren<MovInimigo>();
        iniScript.AceleraInimigo(alvo);
    }
}
