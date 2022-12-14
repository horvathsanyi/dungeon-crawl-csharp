using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform LookAt;
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    private void Start()
    {
        LookAt = GameObject.Find("Player_0").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // This is to check if we are inside the bounds on the X axis.
        float deltaX = LookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < LookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        // This is to check if we are inside the bounds on the X axis.
        float deltaY = LookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < LookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
