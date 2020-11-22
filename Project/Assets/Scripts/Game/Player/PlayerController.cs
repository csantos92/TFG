using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static bool playerCreated;
    private const string AXIS_H = "Horizontal", AXIS_V = "Vertical", ATT = "Attacking";
 
    public Vector2 lastMovement, currentMovement;
    public string nextUuid;
    public bool upPressed, downPressed, leftPressed, rightPressed, attackPressed, canMove, isTalking;
    public float attackTime, speed;
    private float attackTimeCounter;
    private bool walking;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public HealthManager _healthManager;
    public ItemsManager _itemsManager;

    // Get player component
    void Start()
    {
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthManager = FindObjectOfType<HealthManager>();
        _itemsManager = FindObjectOfType<ItemsManager>();
        playerCreated = true;
        canMove = true;
        speed = 5.0f;
        lastMovement = Vector2.zero;
        currentMovement = Vector2.zero;
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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) { ButtonAttack(); }
        if (Input.GetKeyDown(KeyCode.C)) { DrinkPotion(); }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { ShiftPressed(); }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { ShiftReleased(); }

        if (attackPressed)
        {
            attackTimeCounter -= Time.deltaTime;

            if (attackTimeCounter < 0)
            {
                attackPressed = false;
                _animator.SetBool(ATT, false);
            }

        }

        if (upPressed)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 1).normalized * speed;
            walking = true;
            lastMovement = new Vector2(0, 1);
            //_animator.SetFloat(AXIS_V, 1);
            currentMovement = new Vector2(0, 1);
        }

        if (downPressed)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -1).normalized * speed;
            walking = true;
            lastMovement = new Vector2(0, -1);
            currentMovement = new Vector2(0, -1);
            //_animator.SetFloat(AXIS_V, -1);
        }

        if (leftPressed)
        {
            _rigidbody.velocity = new Vector2(-1, _rigidbody.velocity.y).normalized * speed;
            walking = true;
            lastMovement = new Vector2(-1, 0);
            currentMovement = new Vector2(-1, 0);
            //_animator.SetFloat(AXIS_H, -1);
        }

        if (rightPressed)
        {
            _rigidbody.velocity = new Vector2(1, _rigidbody.velocity.y).normalized * speed;
            walking = true;
            lastMovement = new Vector2(1, 0);
            currentMovement = new Vector2(1, 0);
            //_animator.SetFloat(AXIS_H, 1);
        }

    }

    //If player it's not moving then set velocity to zero
    private void LateUpdate()
    {

        if (!walking)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        _animator.SetFloat(AXIS_H, currentMovement.x);
        _animator.SetFloat(AXIS_V, currentMovement.y);
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

    public void ShiftPressed()
    {
        speed = 6.0f;
    }

    public void ShiftReleased()
    {
        speed = 5.0f;
    }

    public void ButtonAttack()
    {
        attackTimeCounter = attackTime;
        _rigidbody.velocity = Vector2.zero;
        _animator.SetBool(ATT, true);
        attackPressed = true;
    }

    public void DrinkPotion()
    {
        _itemsManager.UseItem();
    }

}
