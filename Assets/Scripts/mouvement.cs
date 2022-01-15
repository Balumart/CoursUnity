using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mouvement : MonoBehaviour
{
    [Tooltip("vitesse de déplacement")]
    public float linearspeed = 6;
    [Tooltip("vitesse de rotation")]
    public float angularspeed = 1;


    private Transform player;
    public Vector3 direction;

    public float life = 100;

    void Start()
    {
    GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
    player = goPlayer.transform;
       
    }

    private void Update()
    {
    
    }
    private void FixedUpdate()
    { 
        Rigidbody rb = GetComponent<Rigidbody>();

        direction = player.position - transform.position;
        direction = direction.normalized;

        float angle = Vector3.SignedAngle(direction, transform.forward, transform.up);

        if (angle < -2)
            rb.AddTorque(transform.up * 20);
        else if (angle > 2)
            rb.AddTorque(transform.up * -20);
        
        if(Mathf.Abs(angle) < 10 && rb.velocity.magnitude < 3)
        {
            rb.AddForce(transform.forward * 40);
        }
        Animator anim = GetComponent<Animator>(); // récup l'animator
        if (anim != null)
        {
            anim.SetFloat("speed", rb.velocity.magnitude);
        }

        /* if (rb != null)
         {
            if (rb.velocity.magnitude < 5)
                if (Input.GetButton("Fire1") && rb.velocity.magnitude < linearspeed) //QD clique gauche
                    rb.AddForce(transform.right * 30); // démarre et multiplie la vitesse par 30

            if (Input.GetButton("Fire2") && rb.angularVelocity.magnitude < angularspeed) //QD clique droit
                rb.AddTorque(transform.up * 30); // démarre et multiplie la vitesse par 30
    
         */
        if (life <= 0)
        Destroy(gameObject);

    }



    private void OnDrawGizmos()
    {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + direction);
    }



    
}