using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEffect : MonoBehaviour
{

    private float upperBoundPosition;
    private float lowerBoundPosition;
    private bool isMovingUp = true;
    private Vector2 movingDirection = Vector2.up;
    private float offset = 0.05f;

    void Start()
    {
        upperBoundPosition = this.transform.position.y + offset;
        lowerBoundPosition = this.transform.position.y - offset;
    }

    void FixedUpdate()
    {
        if (isMovingUp)
        {
            if (this.transform.position.y >= upperBoundPosition)
            {
                isMovingUp = false;
                movingDirection = Vector2.down;
            }
        }
        else
        {
            if (this.transform.position.y <= lowerBoundPosition)
            {
                isMovingUp = true;
                movingDirection = Vector2.up;
            }
        }

        this.transform.Translate(movingDirection * Time.smoothDeltaTime);
    }  
}
