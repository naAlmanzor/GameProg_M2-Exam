using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static int _gameState = 1;
    //Game State: 1 = Active, 0 = Game Over, 2 = Win
    private void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setGameState(int state) {
        _gameState = state;
    }

    public int getGameState() {
        return _gameState;
    }
    
    private void Update() {
        Debug.Log(_gameState);
        // if(_gameState != 1) {
        //     SceneManager.LoadScene("Condition Scene", LoadSceneMode.Single);
        // }
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Win() {
        Cursor.lockState = CursorLockMode.None;
        setGameState(2);
        LoadScene("Condition Scene");
    }

    public void GameOver() {
        Cursor.lockState = CursorLockMode.None;
        setGameState(0);
        LoadScene("Condition Scene");
    }
}
