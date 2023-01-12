using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _hand;
    private float _yaw, _pitch;
    public float speed = 5f, sensitivty = 10f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        _rb.gameObject.GetComponent<Rigidbody>();
        _cam.gameObject.GetComponent<Camera>();
    }

    private void Update() {
        Look();

        if(Input.GetKeyDown(KeyCode.E)) {
            Grab();
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

    private void Grab() {
        if(Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 2.5f, ~_playerLayer)) {
            if(hit.transform.CompareTag("Interactable")) {
                Debug.Log("Interacted");
                hit.transform.position = _hand.transform.position;
                hit.transform.rotation = _hand.transform.rotation;
                hit.transform.SetParent(_hand.transform);

                hit.transform.SendMessage("disableRigidBody");

                // hit.transform.gameObject.GetComponent<Rigidbody>().
            }
        }
    }
}
