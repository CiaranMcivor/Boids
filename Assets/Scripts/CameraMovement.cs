using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private float maxMovementSpeed;
    [SerializeField] private float maxRotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    void handleInput()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward * maxMovementSpeed * Input.GetAxis("Vertical"));
        }

        else if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * maxMovementSpeed * Input.GetAxis("Horizontal"));
        }

        if(Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * maxRotationSpeed, 0);
        }

        if (Input.GetAxis("Mouse Y") != 0)
        {
            transform.Rotate(-Input.GetAxis("Mouse Y") * maxRotationSpeed, 0,0);

        }

    }
    // Update is called once per frame
    void Update()
    {
        handleInput();
        transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
    }
}
