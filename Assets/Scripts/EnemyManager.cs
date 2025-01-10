using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public Wave[] waves;
    public Button waveButton;
    int currentWaveIndex;
    bool isWaveInProgress;

    void Start()
    {
        if (waveButton != null)
        {
            waveButton.onClick.AddListener(StartNextWave);
        }
        if (waves.Length > 0)
        {
            CheckButton();
        }
    }

    void StartNextWave()
    {
        if (!isWaveInProgress && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
        CheckButton();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isWaveInProgress = true;
        int totalToSpawn = wave.enemies.Sum(e => e.quantity);

        for (int i = 0; i < totalToSpawn; i++)
        {
            WeightedEnemy chosen = GetRandomEnemy(wave.enemies);
            if (chosen != null)
            {
                chosen.quantity--;
                GameObject spawned = Instantiate(chosen.enemyPrefab, wave.spawnPoint.position, Quaternion.identity);
                Enemy e = spawned.GetComponent<Enemy>();
                if (e != null)
                {
                    e.SetWaypoints(wave.waveWaypoints);
                }
            }
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        isWaveInProgress = false;
    }

    WeightedEnemy GetRandomEnemy(WeightedEnemy[] enemies)
    {
        var valid = enemies.Where(x => x.quantity > 0).ToList();
        if (valid.Count == 0) return null;
        return valid[Random.Range(0, valid.Count)];
    }

    void CheckButton()
    {
        if (waveButton == null) return;
        if (currentWaveIndex >= waves.Length)
        {
            waveButton.interactable = false;
        }
        else
        {
            waveButton.interactable = true;
        }
    }
}
