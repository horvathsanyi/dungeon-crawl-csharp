using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    public float[] skullSpeeds = {2.5f, -2.5f};
    public float distance = 0.25f;
    public Transform[] skulls;

    private void Update()
    {
        for (int i = 0; i < skulls.Length; i++)
        {
            skulls[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * skullSpeeds[i]) * distance, Mathf.Sin(Time.time * skullSpeeds[i]) * distance, 0);
        }
    }

}
