using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    // Movement
    private Rigidbody2D spy;
    public float playerSpeed = 5f;
    public float jumpForce = 250f;
    private bool inAir = false;
    private Vector2 velocity = Vector2.zero;

    //Coyote Movement
    private float coyoteTime = 0.3f;
    private float coyoteTimeCounter;

    // Level Completion
    public bool hasKey = false; 
    public string NextSceneName; 

    // Juice Effects
    [SerializeField] private AudioSource jumpSound;
    private Shake shake;
    [SerializeField] private AudioSource collectGemSound;
    [SerializeField] private AudioSource gameOverSound;

    void Start()
    {
        
        coyoteTimeCounter = coyoteTime;
        spy = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    void FixedUpdate()
    {
        // Movement
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector2.left * Time.deltaTime * playerSpeed);
            velocity = Vector2.left * Time.deltaTime * playerSpeed;

            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector2.right * Time.deltaTime * playerSpeed);
            velocity = Vector2.right * Time.deltaTime * playerSpeed;

            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            velocity = Vector2.zero;
        }

        // Jump
        if (Input.GetKey(KeyCode.Space)) {
            
            if (coyoteTimeCounter > 0f)
            {
                coyoteTimeCounter = 0f;
                jumpSound.Play();
                spy.AddForce(velocity);
                spy.AddForce(Vector2.up * jumpForce);
                inAir = true;
            }

        }
        if (inAir){
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (transform.position.y < -12)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
        
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "platform")
        {
            inAir = false;
            coyoteTimeCounter = coyoteTime;
        }

        else if (col.gameObject.tag == "obstacle")
        {
            gameOverSound.Play();
            shake.camShake();

            transform.localScale *= 0;
            FindObjectOfType<GameManager>().EndGame(3f);
        }

        else if (col.gameObject.tag == "key")
        {
            collectGemSound.Play();
            hasKey = true; 
            Destroy(col.gameObject);
        }

        else if ((col.gameObject.tag == "door") && (hasKey == true))
        {
            Invoke("ChangeLevels", 0.5f);
        }
    }


    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "platform")
        {
            inAir = true;
        }
    }

    public void ChangeLevels()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}




