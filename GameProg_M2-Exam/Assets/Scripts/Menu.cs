using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private GameManager _mgr;
    private AudioManager _aud;

    private void Awake() {
        if(_title != null) {
            _title.text = "";
            _mgr = FindObjectOfType<GameManager>();
        }

        _aud = AudioManager.instance;
        
    }

    private void Update()
    {
        if(_title != null) {
            switch(_mgr.getGameState()) {
            case 0:
                _title.text = "Game Over";
                // _aud.Play("lose");
                break;
            case 2:
                _title.text = "Congratulations";
                // _aud.Play("win");
                break;
            default:
                _title.text = "How?";
                break;
            }
        }   
    }

    public void Retry() {
        _mgr.setGameState(1);
        // _mgr.LoadScene("TEST WORLD");
        _mgr.LoadScene("Maze Game");
        doMusic();
        // SceneManager.LoadScene("TEST WORLD", LoadSceneMode.Single);
    }

    public void MainMenu() {
        _mgr.setGameState(1);
        _mgr.LoadScene("Menu");
        doMusic();
        // SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void ExitGame() {
        Application.Quit();
    }

    private void doMusic() {
        if(!_aud.isPlaying("theme")) {
            _aud.Play("theme");
        }
        if(_aud.isPlaying("win")) {
            _aud.Stop("win");
        }
        if(_aud.isPlaying("lose")) {
            _aud.Stop("lose");
        }
    }
}
