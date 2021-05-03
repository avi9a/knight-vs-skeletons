using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float repeatRate = 10;
    private bool isGameActive;
    void Start() {
        StartGame();
    }
    IEnumerator SpawnTarget() {
        while (isGameActive) {
            yield return new WaitForSeconds(repeatRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void StartGame() {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
    }
}
