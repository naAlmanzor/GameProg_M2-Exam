using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static int _gameState = 1;
    private bool _objectiveComplete = false;
    private AudioManager _aud;
    //Game State: 1 = Active, 0 = Game Over, 2 = Win
    private void Awake() {
        _aud = AudioManager.instance;
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

    public void setObjectiveState(bool state) {
        _objectiveComplete = state;
    }

    public bool getObjectiveState() {
        return _objectiveComplete;
    }
    
    private void Update() {
        // Debug.Log(_gameState);
        // Debug.Log(_objectiveComplete);
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Win() {
        Cursor.lockState = CursorLockMode.None;
        _gameState = 2;
        LoadScene("Condition Scene");
        doSound();
    }

    public void GameOver() {
        Cursor.lockState = CursorLockMode.None;
        _gameState = 0;
        LoadScene("Condition Scene");
        doSound();
    }

    private void doSound() {
        if(_aud.isPlaying("theme")) {
            _aud.Stop("theme");
        }
        if(_gameState == 2) { _aud.Play("win");}
        if(_gameState == 0) { _aud.Play("lose");}
    }
}
