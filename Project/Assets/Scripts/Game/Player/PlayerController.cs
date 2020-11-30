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
    private HealthManager _healthManager;
    private ItemsManager _itemsManager;
    private UIManager _uiManager;
    public DialogueManager _dialogueManager;

    // Get player component
    void Start()
    {
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthManager = FindObjectOfType<HealthManager>();
        _itemsManager = FindObjectOfType<ItemsManager>();
        _uiManager = FindObjectOfType<UIManager>();
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

        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(0, 1);
            currentMovement = new Vector2(0, 1);

            /*if (Input.GetKeyDown(KeyCode.W))
            {
                SFXManager.SharedInstance.LoopSFX(SFXType.SoundType.WALK);
            }*/
        }

        else if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(0, -1);
            currentMovement = new Vector2(0, -1);

           /* if (Input.GetKeyDown(KeyCode.S))
            {
                SFXManager.SharedInstance.LoopSFX(SFXType.SoundType.WALK);
            }*/
        }

        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(1, 0);
            currentMovement = new Vector2(1, 0);

            /*if (Input.GetKeyDown(KeyCode.D))
            {
                SFXManager.SharedInstance.LoopSFX(SFXType.SoundType.WALK);
            }*/
        }

        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(-1, 0);
            currentMovement = new Vector2(-1, 0);

            /*if (Input.GetKeyDown(KeyCode.A))
            {
                SFXManager.SharedInstance.LoopSFX(SFXType.SoundType.WALK);
            }*/
        }

        /*if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            SFXManager.SharedInstance.StopSFX(SFXType.SoundType.WALK);
        }*/

        transform.position = pos;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.SLASH);
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
            speed = 7.0f;
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

        if (_dialogueManager.dialogueActive)
        {
            SFXManager.SharedInstance.StopSFX(SFXType.SoundType.WALK);
        }

        _animator.SetFloat(AXIS_H, currentMovement.x);
        _animator.SetFloat(AXIS_V, currentMovement.y);
        _animator.SetBool("Walking", walking);
        _animator.SetFloat("LastH", lastMovement.x);
        _animator.SetFloat("LastV", lastMovement.y);
    }
}
