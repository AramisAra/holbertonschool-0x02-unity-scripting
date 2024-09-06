using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    float speedX, speedY;
    [SerializeField]
    private float floatpoint;
    private int score, health;
    private string TeleporterName;
    private bool isTeleport;
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
        if (other.tag == "Trap")
        {
            health -= 1;
            Debug.Log("Health: " + health);
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (other.tag == "Goal" && score > 15)
        {
            Debug.Log("You Win");
            Application.Quit();

            // This is use for in editor testing.
            #if UNITY_EDITOR
            
            UnityEditor.EditorApplication.isPlaying = false;
            
            #endif
        }
        if (other.tag == "Teleporter" && !isTeleport)
        {
            if (other.name == "Teleporter-A")
            {
                transform.position = GameObject.Find("Teleporter-B").transform.position;
                TeleporterName = "Teleporter-B";
            }
            else
            {
                transform.position = GameObject.Find("Teleporter-A").transform.position;
                TeleporterName = "Teleporter-A";
            }
            isTeleport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (TeleporterName == other.name)
            isTeleport = false;
    }
}
