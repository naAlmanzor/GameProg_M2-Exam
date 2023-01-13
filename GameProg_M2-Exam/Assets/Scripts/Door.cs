using UnityEngine;

public class Door : MonoBehaviour
{
    private Player _player;
    private GameManager _mgr;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _mgr = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision other) {
        if(_player.getEquippedState() && other.gameObject.tag == "Player") {
            Debug.Log("THE DOOR IS ACTIVATED! KEY USED");
            Destroy(_player.getEquipped());

            gameObject.SetActive(false);

            // _mgr.setGameState(1);
            // SceneManager.LoadScene("Condition Scene", LoadSceneMode.Single);
        }
    }
}
