using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    float speedX, speedY;
    [SerializeField]
    private float floatpoint;
    private int score, health;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        score = 0;
        health = 5;
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        speedX = Input.GetAxis("Horizontal") * moveSpeed;
        speedY = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 playerVelocity = new Vector3(speedX, 0, speedY);
        rb.velocity = Vector3.Lerp(rb.velocity, playerVelocity, floatpoint);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score += 1;
            other.isTrigger = false;
            Destroy(other.gameObject);
            Debug.Log("Score: " + score);
        }
        
    }
}
