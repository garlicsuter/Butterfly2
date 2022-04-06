using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Butterfly : MonoBehaviour
{
    private Rigidbody rb;
    public float flySpeed = 3f;
    public float rotateSpeed = 2.0f;
    bool isAlive = true;
    int currentLevel;
    public ParticleSystem dustFX;
    public GameObject explodeFX;
    AudioSource myAudio;
    public AudioClip explodeSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            //flying movement
            Fly();

            //rotation movement
            Rotate();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bad")
        {
            //Explosion
            Instantiate(explodeFX, transform.position, transform.rotation);
            myAudio.PlayOneShot(explodeSound);
            //Die
            isAlive = false;
            
            //Reset
            Invoke("ResetScene", 2f); 
        }

        if (collision.gameObject.tag == "win")
        {

            Debug.Log("hit win");
            SceneManager.LoadScene(currentLevel + 1);
           
        }
    }

    void Fly()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * flySpeed);
            dustFX.Play();
        }

        else
        {
            dustFX.Play();
        }
    }

    private void Rotate()
    {
        rb.freezeRotation = true;
        float inputAxis = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * inputAxis * rotateSpeed);
        rb.freezeRotation = false;
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(currentLevel);
    }
}
