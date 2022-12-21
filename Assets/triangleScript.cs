using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triangleScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = StartPosition + new Vector3(0f, Mathf.Sin(Time.time * 2.0f) * 0.3f, 0f);
    }
}
