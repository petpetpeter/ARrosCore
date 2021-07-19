using UnityEngine;

public class CameraMover : MonoBehaviour
{

    private float moveSpeed = 15.0f;
    private Vector3 moveVector;

    void Start()
    {
        moveVector = new Vector3(0, 0, 0);
    }

    void Update()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.position += moveSpeed * moveVector * Time.deltaTime;
        }

    }
}