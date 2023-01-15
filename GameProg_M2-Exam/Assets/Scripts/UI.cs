using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _hp, _objectiveText, _interactText;
    [SerializeField] private GameObject _interact;
    [SerializeField] private Slider _slider;
    private float _hpNum = 100f, _maxHP = 100f, _currentCoin = 0f, _maxCoin = 3f;
    private GameManager _mgr;
    // private AudioManager _aud;
    
    private void Start() {
        _hp.text = _hpNum.ToString()+"/"+_maxHP.ToString();
        _objectiveText.text = "Find 3 Coins: "+_currentCoin.ToString()+"/"+_maxCoin.ToString();

        _mgr = FindObjectOfType<GameManager>();
        _slider.maxValue = _maxHP;
        _slider.value = _hpNum;

        _interactText.text = "WASD to Move | LSHIFT to Sprint | SPACE to Jump";

        // _aud = AudioManager.instance;
        StartCoroutine(TutorialCoroutine());
    }

    private void setHealth() {
        _slider.value = _hpNum;
    }

    public void Damage(float damage) {
        _hpNum -= damage;

        _hp.text = _hpNum.ToString()+"/"+_maxHP.ToString();
        setHealth();
    }

    public void CollectCoin() {
        _currentCoin += 1f;
        _objectiveText.text = "Find 3 Coins: "+_currentCoin.ToString()+"/"+_maxCoin.ToString();
    }

    public void toggleInteractPrompt(bool prompt) {
        _interact.SetActive(prompt);
    }

    private void Update() {
        if(_currentCoin == _maxCoin && !_mgr.getObjectiveState()) {
            // if(!_mgr.getObjectiveState()) {
            //     _aud.Play("door");
            // }
            _mgr.setObjectiveState(true);
        }

        if(_mgr.getObjectiveState()) {
            _objectiveText.text = "Find the key and exit";
        }
    }

    public void setHPValues(float current, float max) {
        _hpNum = current;
        _maxHP = max;
        _hp.text = _hpNum.ToString()+"/"+_maxHP.ToString();
        _slider.maxValue = _maxHP;
        _slider.value = _hpNum;
    }

    public void setInteract(string text) {
        _interactText.text = "text";
    }

    private IEnumerator TutorialCoroutine() {
        toggleInteractPrompt(true);
        yield return new WaitForSecondsRealtime(5f);
        
        toggleInteractPrompt(false);
        
        _interactText.text = "Press | E | to interact";
        yield break;
    }
    
}
