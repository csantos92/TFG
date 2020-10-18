using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;

    public bool isTalking;

    public static bool playerCreated;   

    public float speed = 5.0f;
    //public float currentSpeed = 5.0f;
    private const string AXIS_H = "Horizontal", AXIS_V = "Vertical", ATT = "Attacking";
    private bool walking = false;
    public Vector2 lastMovement = Vector2.zero;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    public string nextUuid;

    public float attackTime;
    private float attackTimeCounter;

    public bool isPressed;
    public bool upPressed;
    public bool downPressed;
    public bool leftPressed;
    public bool rightPressed;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();


        playerCreated = true;
    }

    // Update is called once per frame
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

        if (isPressed)
        {
            attackTimeCounter -= Time.deltaTime;

            if (attackTimeCounter < 0)
            {
                isPressed = false;
                _animator.SetBool(ATT, false);
            }

        }

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


        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
        {
            //Vector3 translation = new Vector3(Input.GetAxisRaw(AXIS_H) * speed * Time.deltaTime, 0, 0);
            //this.transform.Translate(translation);

            _rigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), _rigidbody.velocity.y).normalized * speed;
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if(Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            //Vector3 translation = new Vector3(0, Input.GetAxisRaw(AXIS_V) * speed * Time.deltaTime, 0);
            //this.transform.Translate(translation);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Input.GetAxisRaw(AXIS_V)).normalized * speed;
            walking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
        }
        //GoUp();

        /*Arregla la velocidad en diagonal de forma matemática
        if(Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f && Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            currentSpeed = speed / Mathf.Sqrt(2);
        }
        else
        {
            currentSpeed = speed;
        }*/
    }

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
