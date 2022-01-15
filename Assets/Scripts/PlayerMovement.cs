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

    public gameOverScreen gameOverScreen;

    Rigidbody rb;

    //tentative de gestion de score (manquée)
    int kills = 0; 

    void Start()
    {
        //cache le curseur
        Cursor.lockState = CursorLockMode.Locked;

        if (playerCam == null)
        {
            Camera cam = transform.GetComponentInChildren<Camera>();
            playerCam = cam.transform;
        }
    }
    private void Update()
    {
        rb = GetComponent<Rigidbody>();
        //Pour unlock la souris
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

        //Saut
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //Est ce qu'on est mort ?
        if (life <= 0)
        {
            GameOver();
        }
    }

        public void GameOver()
    {
        gameOverScreen.Setup(kills);
    }

   // Est ce qu'on touche le sol ?
    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .5f, groundLayers);

    }

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