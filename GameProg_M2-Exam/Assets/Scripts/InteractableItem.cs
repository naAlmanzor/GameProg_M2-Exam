using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public string itemType;
    public float fwdForce, upForce;
    private Rigidbody _rb;

    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    public void disableRigidBody() {
        _rb.isKinematic = true;
        _rb.detectCollisions = false;
        _rb.useGravity = false;
    }

    public void enableRigidBody() {
        _rb.isKinematic = false;
        _rb.detectCollisions = true;
        _rb.useGravity = true;
    }

    public void Drop(Rigidbody player, Camera cam) {
        enableRigidBody();
        _rb.velocity = player.velocity;

        _rb.AddForce(cam.transform.forward * fwdForce, ForceMode.Impulse);
        _rb.AddForce(cam.transform.up * upForce, ForceMode.Impulse);
    }
}
