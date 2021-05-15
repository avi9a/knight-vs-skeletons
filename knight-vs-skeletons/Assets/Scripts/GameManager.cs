using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
}
