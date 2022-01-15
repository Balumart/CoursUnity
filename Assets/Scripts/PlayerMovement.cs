using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCam;
    public float jumpForce = 10f;
    public float speed = 50;
    public LayerMask groundLayers;
    public CapsuleCollider col;
    public float life = 100;
    Rigidbody rb;
    
    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerCam == null)
        {
            Camera cam = transform.GetComponentInChildren<Camera>();
            playerCam = cam.transform;
        }
    }

    private void Update()
    {
        rb = GetComponent<Rigidbody>();
        //Pour declock la souris
        if (Input.GetKey(KeyCode.Escape)) 
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //Sauve la rotation
        Quaternion lastRotation = playerCam.rotation;

        //Baisse / leve la tete
        float rot = Input.GetAxis("Mouse Y") * -10;
        Quaternion q = Quaternion.AngleAxis(rot, playerCam.right);
        playerCam.rotation = q * playerCam.rotation;

        //Est ce qu'on a la tete à l'envers ?
        Vector3 forwardCam = playerCam.forward;
        Vector3 forwardPlayer = transform.forward;
        float regardeDevant = Vector3.Dot(forwardCam, forwardPlayer);
        if (regardeDevant < 0.0f)
            playerCam.rotation = lastRotation;

        rot = Input.GetAxis("Mouse X") * 10;
        q = Quaternion.AngleAxis(rot, transform.up);
        transform.rotation = q * transform.rotation;

        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .5f, groundLayers);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        rb = GetComponent<Rigidbody>();
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");

        rb.AddForce(vert * transform.forward * speed);
        rb.AddForce(hori * transform.right * speed);

        float rot = Input.GetAxis("Mouse X") * 10;
        rb.AddTorque(Vector3.up * rot);

        
    }
}