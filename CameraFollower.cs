using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public float bottomBoundY;
    void Update()
    {
        if (transform.position.y > bottomBoundY){
            Vector3 position = player.position;
            position.y += 1.5f;
            position.x = 0;
            position.z = -10.5f;
            transform.position = position;
        }

    }
}
