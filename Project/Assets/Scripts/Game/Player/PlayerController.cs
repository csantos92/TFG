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
    public bool attackPressed, canMove, isTalking;
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
        Vector3 pos = transform.position;

        if (isTalking)
        {
            walking = false;
            _rigidbody.velocity = Vector2.zero;
            _animator.enabled = false;
           
            return;
        }

        walking = false;

        if (!canMove)
        {
            return;
        }

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(0, 1);
            //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 1).normalized * speed;
            currentMovement = new Vector2(0, 1);
        }

        else if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(0, -1);
            currentMovement = new Vector2(0, -1);
        }

        else if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(1, 0);
            currentMovement = new Vector2(1, 0);
        }

        else if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(-1, 0);
            currentMovement = new Vector2(-1, 0);
        }

        transform.position = pos;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            attackTimeCounter = attackTime;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(ATT, true);
            attackPressed = true;
        }

        if (Input.GetKey("c"))
        {
            _itemsManager.UseItem();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 6.0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5.0f;
        }

        if (attackPressed)
        {
            attackTimeCounter -= Time.deltaTime;

            if (attackTimeCounter < 0)
            {
                attackPressed = false;
                _animator.SetBool(ATT, false);
            }
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
}
