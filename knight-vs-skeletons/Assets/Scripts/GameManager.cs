using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timer;
    public void EndLevel(CanvasGroup canvas, bool restart) {
        timer += Time.deltaTime / 2;
        canvas.alpha = timer / 1;
        if (timer > 1) {
            if (restart) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else {
                Application.Quit();
            }
        }
    }
}
