using UnityEngine;

public class Door : MonoBehaviour
{
    private Player _player;
    private GameManager _mgr;
    private AudioManager _aud;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _mgr = FindObjectOfType<GameManager>();
        _aud = AudioManager.instance;
    }

    private void OnCollisionEnter(Collision other) {
        if(_player.getEquippedState() && other.gameObject.tag == "Player") {
            Debug.Log("THE DOOR IS ACTIVATED! KEY USED");
            Destroy(_player.getEquipped());
            _aud.Play("door");
            gameObject.SetActive(false);
            
        }
    }

    private void Update() {
        if(gameObject.tag == "Key Door" && _mgr.getObjectiveState()) {
            _aud.Play("door");
            gameObject.SetActive(false);
        }
    }
}
