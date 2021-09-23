using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public bool click;
    public GameObject endCanvas;
    public GameObject restartButton;
    public void EndLevel() {
        if (gameOver == false) {
            gameOver = true;
            Debug.Log("game over");
            endCanvas.SetActive(true);
            Restart();
        }
    }
    public void Restart() {
        if (click == true) {
            OnClick();
        }
    }
    public void OnClick() {
        click = true;
        Debug.Log("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
