using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _hand;
    private GameObject _item;
    private UI _ui;
    private GameManager _mgr;
    private AudioManager _aud;
    private float _yaw, _pitch;
    private bool _equipped, _collidingObstacle = false;
    private float _coins, _sprint = 1f;
    public float speed = 5f, sprintMultiplier = 1.5f, sensitivty = 10f, jump = 5f, hp = 100f, damage = 1f, interval = 0.5f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        _rb.gameObject.GetComponent<Rigidbody>();
        _cam.gameObject.GetComponent<Camera>();

        _ui = FindObjectOfType<UI>();
        _mgr = FindObjectOfType<GameManager>();
        _aud = AudioManager.instance;

        _ui.setHPValues(hp, hp);
    }

    private void Update() {
        Look();
        CheckRay();

        if(_item == null) {
            _equipped = false;
        }

        // if(Input.GetKeyDown(KeyCode.Q) && _equipped) {
        //     Drop();
        // }

        if(Input.GetKey(KeyCode.LeftShift)) {
            _sprint = sprintMultiplier;
        } else {_sprint = 1f;}

        if(hp == 0) {
            _mgr.GameOver();
        }
    }

    private void FixedUpdate() {
        Move();
        Jump();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Obstacle") {
            // Debug.Log("Collided");
            _collidingObstacle = true;
            StartCoroutine(Damage(damage, interval));
        }

        if(other.gameObject.tag == "Finish") {
            _mgr.Win();
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Obstacle") {
            // Debug.Log("Exiting");
            _collidingObstacle = false;
        }
    }

    // private void OnCollisionStay(Collision other) {
    //     if(other.gameObject.tag == "Obstacle") {
    //         // Damage(0.5f);
    //         _collidingObstacle = true;
    //         StartCoroutine(Damage(0.5f));
    //     } else {_collidingObstacle = false;}
    // }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "coin") {
            CollectCoin();
            Destroy(other.gameObject);
        }
    }

    private void Look() {
        _pitch -= Input.GetAxisRaw("Mouse Y") * sensitivty;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        _yaw += Input.GetAxisRaw("Mouse X") * sensitivty;
        _cam.transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private void Move() {
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed * _sprint;
        Vector3 forward = new Vector3(-_cam.transform.right.z, 0f, _cam.transform.right.x);
        Vector3 wish = (forward * axis.x + _cam.transform.right * axis.y + Vector3.up * _rb.velocity.y);
        _rb.velocity = wish;
    }

    private void Jump() {
        if(Input.GetKey(KeyCode.Space) && Grounded()) {
            _rb.velocity = new Vector3(_rb.velocity.x, jump, _rb.velocity.y);
        }
    }

    private void CheckRay() {
        if(Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 2.5f, ~_playerLayer)) {
            if(hit.transform.CompareTag("Interactable")) {
                _item = hit.transform.gameObject;
                _item.GetComponent<Outline>().enabled = true;

                _ui.toggleInteractPrompt(true);

                if(Input.GetKeyDown(KeyCode.E) && !_equipped) {
                    Grab();
                }
            } else if(!hit.transform.CompareTag("Interactable") && !_equipped) {
                if(_item != null) {
                    _item.GetComponent<Outline>().enabled = false;
                    _item = null;

                    _ui.toggleInteractPrompt(false);
                }
            }
        }
    }

    private void Grab() {
        _item.SendMessage("disableRigidBody");
        _item.GetComponent<Outline>().enabled = false;
        _item.transform.position = _hand.transform.position;
        _item.transform.rotation = _hand.transform.rotation;
        _item.transform.SetParent(_hand.transform);

        _ui.toggleInteractPrompt(false);

        // _item = hit.transform.gameObject;
        _equipped = true;

        _aud.Play("equip");
    }

    private void Drop() {
        _item.GetComponent<InteractableItem>().Drop(_rb, _cam);
        _item.GetComponent<Outline>().enabled = false;
        _item.transform.SetParent(null);
        _item = null;
        _equipped = false;
    }

    private bool Grounded() {
        if(Physics.Raycast(_rb.transform.position, Vector3.down, 1 + 0.001f)) {
            return true;
        } else { return false; }
    }

    private IEnumerator Damage(float damage, float interval) {
        // Debug.Log("Coroutine Started");
        // Debug.Log("_collidingObstacle: " + _collidingObstacle);

        while(_collidingObstacle) {
            // Debug.Log("Taking Damage");
            hp -= damage;
            _ui.Damage(damage);
            _aud.Play("lava");
            yield return new WaitForSeconds(interval);
        }

        yield break;
    }

    private void CollectCoin() {
        _ui.CollectCoin();
        _aud.Play("coin");
    }

    public bool getEquippedState() {
        return _equipped;
    }

    public GameObject getEquipped() {
        return _item.gameObject;
    }
}
