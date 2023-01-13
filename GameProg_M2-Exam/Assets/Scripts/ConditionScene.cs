using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using TMPro;

public class ConditionScene : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private GameManager _mgr;

    private void Awake() {
        if(_title != null) {
            _title.text = "";
            _mgr = FindObjectOfType<GameManager>();
        }
        
    }

    private void Update()
    {
        // if(_mgr.getGameState() == 0) {
        //     _title.text = "Game Over";
        // }

        // if(_mgr.getGameState() == 2) {
        //     _title.text = "Congratulations";
        // }
        if(_title != null) {
            switch(_mgr.getGameState()) {
            case 0:
                _title.text = "Game Over";
                break;
            case 2:
                _title.text = "Congratulations";
                break;
            default:
                _title.text = "How?";
                break;
            }
        }   
    }

    public void Retry() {
        _mgr.setGameState(1);
        _mgr.LoadScene("TEST WORLD");
        // SceneManager.LoadScene("TEST WORLD", LoadSceneMode.Single);
    }

    public void MainMenu() {
        _mgr.setGameState(1);
        _mgr.LoadScene("Menu");
        // SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
