using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    float xRot, yRot = 0f;
    public GameObject Marblemania;
    public Rigidbody ball;
    public GameObject marble;


    public float rotationSpeed = 5f;

    public float shootPower = 20f;
    public AbilityManager abilityManager;
    public float moveSpeed;
    float horizontalInput;
    float verticalInput;
    public bool inMotion = false;
    public bool turnTaken = false;
    Vector3 moveDirection;
    public LineRenderer line;

    // Update is called once per frame
    void Start() 
    {
        
        ball.useGravity = false;
        int Player = LayerMask.NameToLayer("Player");
        marble.layer = Player;
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
    // Allows the user to look in a direction by holding Rightclick, and shoot their marble with Leftclick
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
            int Default = LayerMask.NameToLayer("Default");
            marble.layer = Default;
            ball.velocity=transform.forward * shootPower;
            StartCoroutine(timeout());
        }
    }

    // Called to stop the player's turn when the marble is shot (may need tweaking when powers are implemented!)
    IEnumerator timeout() 
    {
        turnTaken = true;
        yield return new WaitForSeconds(5);
        shootPower = 20f;
        ball.constraints = RigidbodyConstraints.FreezePosition;
        marble.GetComponent<MeshRenderer>().material = abilityManager.normalMaterial;
        yield return new WaitForSeconds(1);
        inMotion = false;
        Start();
        PlayerTurn turn = Marblemania.GetComponent<PlayerTurn>();
        turn.TurnSwitch();
        inMotion = false;
        turnTaken = false;
    }

    // Allows the player to move with WASD
    private void MovePlayer() 
    {
        ball.constraints = RigidbodyConstraints.None;
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        ball.AddForce(moveDirection.normalized * moveSpeed * 7f, ForceMode.Force);
    }

    // Stops the marble if the player isn't putting any input (even in midair!)
    private void freezeControl() 
    {
        if (horizontalInput == 0.0 && verticalInput == 0.0) {
            ball.constraints = RigidbodyConstraints.FreezePosition;
        } 
    }

    // Stops the player from going too fast adjusting their shot
    private void SpeedControl() 
    {
        Vector3 flatVel = new Vector3(ball.velocity.x, 0f, ball.velocity.z);

        if (flatVel.magnitude >= moveSpeed && inMotion == false) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            ball.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }
}
