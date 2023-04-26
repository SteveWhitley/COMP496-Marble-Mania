using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    float xRot, yRot = 0f;

    public Rigidbody ball;

    public float rotationSpeed = 5f;

    public float shootPower = 20f;

    public float moveSpeed;
    float horizontalInput;
    float verticalInput;
    bool inMotion = false;
    bool turnTaken = false;
    Vector3 moveDirection;

    public LineRenderer line;
    // Update is called once per frame
    void Start() {
        ball.useGravity = false;
    }

    void Update()
    {
        if (!turnTaken) {
            SpeedControl();
            transform.position = ball.position;
            line.gameObject.SetActive(true);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 4f);
            MyInput();
        } else {
            line.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate() 
    {
        if (!turnTaken) {
            MovePlayer();
            if (!inMotion) {
                freezeControl();
            }    
        }
    }

    private void MyInput() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetMouseButton(1)) {
            xRot += Input.GetAxis("Mouse X")*rotationSpeed;
            yRot += Input.GetAxis("Mouse Y")*rotationSpeed;
            if (yRot < -35f) {
                yRot = -35f;
            }
            transform.rotation = Quaternion.Euler(yRot, xRot, 0f);
        }
        if (Input.GetMouseButtonUp(0)) {
            ball.constraints = RigidbodyConstraints.FreezePosition;
            inMotion = true;
            ball.constraints = RigidbodyConstraints.None;
            ball.useGravity = true;
            ball.velocity=transform.forward * shootPower;
            StartCoroutine(timeout());
        }
    }

    IEnumerator timeout() 
    {
        yield return new WaitForSeconds(5);
        inMotion = false;
        turnTaken = true;
    }

    private void MovePlayer() 
    {
        ball.constraints = RigidbodyConstraints.None;
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        ball.AddForce(moveDirection.normalized * moveSpeed * 7f, ForceMode.Force);
    }

    private void freezeControl() 
    {
        if (horizontalInput == 0.0 && verticalInput == 0.0) {
            ball.constraints = RigidbodyConstraints.FreezePosition;
        } 
    }

    private void SpeedControl() 
    {
        Vector3 flatVel = new Vector3(ball.velocity.x, 0f, ball.velocity.z);

        if (flatVel.magnitude >= moveSpeed && inMotion == false) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            ball.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }
}
