using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _hand;
    private GameObject _item;
    private float _yaw, _pitch;
    private bool _equipped;
    public float speed = 5f, sensitivty = 10f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        _rb.gameObject.GetComponent<Rigidbody>();
        _cam.gameObject.GetComponent<Camera>();
    }

    private void Update() {
        Look();

        if(_item != null) {
            _equipped = true;
        } else {
            _equipped = false;
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            CheckRay();
        }

        if(Input.GetKeyDown(KeyCode.Q) && _equipped) {
            Drop();
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void Look() {
        _pitch -= Input.GetAxisRaw("Mouse Y") * sensitivty;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        _yaw += Input.GetAxisRaw("Mouse X") * sensitivty;
        _cam.transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private void Move() {
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * speed;
        Vector3 forward = new Vector3(-_cam.transform.right.z, 0f, _cam.transform.right.x);
        Vector3 wish = (forward * axis.x + _cam.transform.right * axis.y + Vector3.up * _rb.velocity.y);
        _rb.velocity = wish;
    }

    private void CheckRay() {
        if(Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 2.5f, ~_playerLayer)) {
            if(!_equipped) {
                Grab(hit);
            }
        }
    }

    private void Grab(RaycastHit hit) {
        if(hit.transform.CompareTag("Interactable")) {
            hit.transform.SendMessage("disableRigidBody");
            hit.transform.position = _hand.transform.position;
            hit.transform.rotation = _hand.transform.rotation;
            hit.transform.SetParent(_hand.transform);

            _item = hit.transform.gameObject;
        }
    }

    private void Drop() {
        _item.GetComponent<InteractableItem>().Drop(_rb, _cam);
        _item.transform.SetParent(null);
        _item = null;
    }
}
