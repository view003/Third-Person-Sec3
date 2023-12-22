using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float speed = 5f;
    Vector3 movement, jump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float inputZ = Input.GetAxis("Jump");
        movement = new Vector3 (inputX, 0, inputY) * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(movement);
        jump = new Vector3 (0, inputZ, 0) * speed * Time.deltaTime;
        transform.position = jump;
        transform.position += movement;
    }
}
