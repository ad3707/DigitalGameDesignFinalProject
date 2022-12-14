using UnityEngine;

public class Wizard : MonoBehaviour
{
    private Vector2 movingDirection = Vector2.left;
    public GameObject groundRayObject;
    public System.Single rightBoundX;
    public System.Single leftBoundX;
    public float guardSensingDistance;
    public Sprite spriteImage;

    void Start()
    {
    }

    void Update()
    {
        UpdateMovement();
        TrySensing();
    }

    void UpdateMovement()
    {
        if (this.transform.position.x > rightBoundX)
        {
            movingDirection = Vector2.left;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (this.transform.position.x < leftBoundX) 
        {
            movingDirection = Vector2.right;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        this.transform.Translate(movingDirection * Time.smoothDeltaTime);
    }

    void TrySensing()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundRayObject.transform.position, movingDirection, guardSensingDistance);
        Debug.DrawRay(groundRayObject.transform.position, movingDirection * guardSensingDistance, Color.blue);

        if (hit && hit.collider.tag == "spy")
        {
            Debug.Log("player got caught by the guard!");
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteImage;
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}