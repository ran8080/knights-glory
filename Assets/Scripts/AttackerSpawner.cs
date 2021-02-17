using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    // Config params 
    [SerializeField] Attacker[] attackers;
    [SerializeField] float minSpawnRate = 1f;
    [SerializeField] float maxSpawnRate = 5f;
    [SerializeField] float spwanerOffsetOnGrid = 0.5f;

    // States
    bool spawn = true;

    [SerializeField]
    int liveAttackersInLane = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemiesEverySecondsRange(minSpawnRate, maxSpawnRate));
    }
    
    public int GetLiveAttackersInLane()
    {
        return liveAttackersInLane;
    }

    public float GetSpwanerOffsetOnGrid()
    {
        return spwanerOffsetOnGrid;
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private IEnumerator SpawnEnemiesEverySecondsRange(float minSpawnRate, float maxSpawnRate) {
        while (spawn) { 
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        var attackerIndex = Random.Range(0, attackers.Length);
        Spawn(attackers[attackerIndex]);
    }

    private void Spawn(Attacker attackerPrefab) {
        Attacker newAttacker = Instantiate(attackerPrefab,
                               transform.position,
                               Quaternion.Euler(0, attackerPrefab.GetDefaultRotation(), 0));

        // Set the attacker parent as current Attacker Spawner
        newAttacker.transform.parent = transform;
    }

}
