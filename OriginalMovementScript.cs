using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoyoteMovement : MonoBehaviour
{
    // Movement
    public Rigidbody2D spy;
    public float playerSpeed = 10f;
    public float jumpForce = 1700f;
    private bool inAir = false;
    private Vector2 velocity = Vector2.zero;

    // Level Completion
    public bool has_key = false; 
    public string sceneName; 

    // Juice Effects
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource collectGemSound;
    [SerializeField] private AudioSource gameOverSound;
    private Shake shake;

    void Start()
    {
        spy = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    void FixedUpdate()
    {
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

        if (Input.GetKey(KeyCode.Space)) {
            
            if (!inAir)
            {
                jumpSound.Play();
                spy.AddForce(velocity);
                spy.AddForce(Vector2.up * jumpForce);
                inAir = true;
            }

        }
        
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "platform")
        {
            inAir = false;
        }

        else if (col.gameObject.tag == "obstacle")
        {
            gameOverSound.Play();
            shake.camShake();
            transform.localScale *= 0;
            FindObjectOfType<GameManager>().EndGame();
        }

        else if (col.gameObject.tag == "key")
        {
            collectGemSound.Play();
            has_key = true; 
            Destroy(col.gameObject);
        }

        else if ((col.gameObject.tag == "door") && (has_key == true))
        {
            has_key = false; 
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "platform")
        {
            inAir = true;
        }
    }
}
