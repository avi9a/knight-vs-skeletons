using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public void EndLevel() {
        if (gameOver == false) {
            gameOver = true;
            Debug.Log("game over");
            Restart();
        }
    }
    public void Restart() {
        //press the space bar and load the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
