using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _hp, _coin;
    [SerializeField] private GameObject _interact;
    [SerializeField] private GameObject _fill100, _fill75, _fill50, _fill25;
    private float _hpNum = 100f, _maxHP = 100f, _currentCoin = 0f, _maxCoin = 3f;
    private bool _objectiveComplete = false;
    private GameManager _mgr;
    
    private void Start() {
        _hp.text = _hpNum.ToString()+"/"+_maxHP.ToString();
        _coin.text = "Find 3 Coins: "+_currentCoin.ToString()+"/"+_maxCoin.ToString();

        _mgr = FindObjectOfType<GameManager>();
    }

    public void Damage() {
        _hpNum -= 25f;

        if(_hpNum <= 75) {
            _fill100.SetActive(false);
        }
        if(_hpNum <= 50) {
            _fill75.SetActive(false);
        }
        if(_hpNum <= 25) {
            _fill50.SetActive(false);
        }
        if(_hpNum <= 0) {
            _fill25.SetActive(false);
        }

        _hp.text = _hpNum.ToString()+"/"+_maxHP.ToString();
    }

    public void CollectCoin() {
        _currentCoin += 1f;
        _coin.text = "Find 3 Coins: "+_currentCoin.ToString()+"/"+_maxCoin.ToString();
    }

    public void toggleInteractPrompt(bool prompt) {
        _interact.SetActive(prompt);
    }

    private void Update() {
        if(_currentCoin == _maxCoin) {
            _objectiveComplete = true;
        }

        if(_objectiveComplete) {
            _mgr.Win();
        }
    }
    
}
