using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed = 1.0f;
    public CharacterController charController;
    public bool invincible;
    // Start is called before the first frame update

    //Jump variables
    public float maxJump = 2.0f;
    private float gravityY = 0.0f;
    public float mass = 1.0f;
    public int lives;
    public bool canFly;
    //Camera Variables
    public Camera camera;
    public Cinemachine.CinemachineFreeLook cine;
    public float flyTimer;
    public float invTime;
    public TextMeshProUGUI endText;
    Color backup;
    Object kawaii;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        canFly = false;
        flyTimer = 0.0f;
        invTime = 0;
        lives = 3;
        invincible = false;
        kawaii = this.transform.Find("KawaiiBat").gameObject;
        backup = kawaii.GetComponent<Renderer>().material.color;


    }

    // Update is called once per frame
    void Update()
    {
        float dX = Input.GetAxis("Horizontal");
        float dY = Input.GetAxis("Vertical");

        Vector3 nonNormalizedMovementVector = new Vector3(dX, 0, dY);

        Vector3 movementVector = nonNormalizedMovementVector;
        movementVector = Quaternion.AngleAxis(camera.transform.eulerAngles.y, Vector3.up) * movementVector;

        movementVector.Normalize();
        movementVector = movementVector * maxSpeed;


        gravityY += Physics.gravity.y * mass * Time.deltaTime;
        if (charController.isGrounded)
        {

            //Notice that isGrounded is buggy, and needs a downwards vector of a certain force to detect
            //Collision.
            gravityY = -0.5f;

        }

        if (Input.GetKey(KeyCode.Space) && canFly)
        {
            gravityY = maxJump;
            if (canFly)
            {
                flyTimer -= Time.deltaTime;
                if (flyTimer <= 0)
                {
                    canFly = false;
                    flyTimer = 0.0f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            maxSpeed *= 3;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            maxSpeed /= 3;

        if (invincible)
        {
            kawaii.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            invTime -= Time.deltaTime;
            if (invTime <= 0.0f)
            {
                invincible = false;
                invTime = 0.0f;
            }
        }
        else
            kawaii.GetComponent<Renderer>().material.color = backup;


        Vector3 newMoveVector = movementVector;
        newMoveVector.y = gravityY;

        Physics.SyncTransforms();
        charController.Move(newMoveVector * Time.deltaTime);

        if (movementVector != Vector3.zero)
        {
            Quaternion rotationDirection = Quaternion.LookRotation(movementVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationDirection, 360 * Time.deltaTime);
        }

    }
    public void Respawn()
    {
        cine.gameObject.SetActive(false);
        canFly  = false;
        flyTimer = 0.0f;
        this.gameObject.transform.position = new Vector3(480, 7, 65);
        this.gameObject.transform.forward = Vector3.forward;
        cine.gameObject.SetActive(true);

    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        else
            UnityEngine.Cursor.lockState = CursorLockMode.None;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "redbull")
        {
            Stats.redbullsCollected++;
            canFly = true;
            flyTimer += 5.0f;
            if (flyTimer >= 20.0f)
                flyTimer = 20.0f;
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.name == "star")
        {
            Stats.pickedStars++;
            invincible = true;
            invTime = 10.0f;
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.name == "Umbrella")
        {
            Win();
        }
    }

    public void UpdateStats()
    {
        endText.text += "\n";
        endText.text += "Total Meteors: " + Stats.totalMeteors + "\n";
        endText.text += "Destroyed Meteors: " + Stats.destroyedMeteors + "\n\n";
        endText.text += "Total Redbulls: " + Stats.totalRedbulls + "\n";
        endText.text += "Destroyed Redbulls: " + Stats.redbullsDestroyed + "\n";
        endText.text += "Collected Redbulls: " + Stats.redbullsCollected + "\n\n";

        endText.text += "Total Stars: " + Stats.stars+ "\n";
        endText.text += "Collected Stars: " + Stats.pickedStars+ "\n\n";


    }
    public void Win()
    {
        gameObject.SetActive(false);
        endText.text = "VICTORY !!";
        UpdateStats();
    }

    public void Lose()
    {
        if (lives <= 0)
        {
            gameObject.SetActive(false);
            endText.text = "YOU LOST !!";
            UpdateStats();
        }
    }
}
