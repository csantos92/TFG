using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static bool playerCreated;
    public bool canMove = true;
    public bool isTalking;
    public float speed = 5.0f;
    private const string AXIS_H = "Horizontal", AXIS_V = "Vertical", ATT = "Attacking";
    private bool walking;
    public Vector2 lastMovement = Vector2.zero;
    public string nextUuid;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public bool upPressed;
    public bool downPressed;
    public bool leftPressed;
    public bool rightPressed;
    public HealthManager _healthManager;

    // Get player component
    void Start()
    {
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthManager = FindObjectOfType<HealthManager>();

        playerCreated = true;
    }

    // Player movements
    void Update()
    {
        if (isTalking)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.enabled = false;
            return;
        }

        walking = false;

        if (!canMove)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) { ButtonUpPressed(); }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) { ButtonUpReleased(); }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) { ButtonDownPressed(); }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) { ButtonDownReleased(); }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) { ButtonLeftPressed(); }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) { ButtonLeftReleased(); }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) { ButtonRightPressed(); }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) { ButtonRightReleased(); }

        if (upPressed)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 1).normalized * speed;
            walking = true;
            lastMovement = new Vector2(0, 1);
            _animator.SetFloat(AXIS_V, 1);
        }

        if (downPressed)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -1).normalized * speed;
            walking = true;
            lastMovement = new Vector2(0, -1);
            _animator.SetFloat(AXIS_V, -1);
        }

        if (leftPressed)
        {
            _rigidbody.velocity = new Vector2(-1, _rigidbody.velocity.y).normalized * speed;
            walking = true;
            lastMovement = new Vector2(-1, 0);
            _animator.SetFloat(AXIS_H, -1);
        }

        if (rightPressed)
        {
            _rigidbody.velocity = new Vector2(1, _rigidbody.velocity.y).normalized * speed;
            walking = true;
            lastMovement = new Vector2(1, 0);
            _animator.SetFloat(AXIS_H, 1);
        }
    }

    //If player it's not moving then set velocity to zero
    private void LateUpdate()
    {

        if (!walking)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetBool("Walking", walking);
        _animator.SetFloat("LastH", lastMovement.x);
        _animator.SetFloat("LastV", lastMovement.y);

    }

    public void ButtonUpPressed()
    {
        upPressed = true;
    }

    public void ButtonUpReleased()
    {
        upPressed = false;
    }

    public void ButtonDownPressed()
    {
        downPressed = true;
    }

    public void ButtonDownReleased()
    {
        downPressed = false;
    }
    public void ButtonLeftPressed()
    {
        leftPressed = true;
    }

    public void ButtonLeftReleased()
    {
        leftPressed = false;
    }
    public void ButtonRightPressed()
    {
        rightPressed = true;
    }

    public void ButtonRightReleased()
    {
        rightPressed = false;
    }

}
