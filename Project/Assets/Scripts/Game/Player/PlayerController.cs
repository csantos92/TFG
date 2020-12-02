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
    private ItemsManager _itemsManager;
    private UIManager _uiManager;
    public DialogueManager _dialogueManager;
    private AttackNotAllowed _attackNotAllowed;

    // Get player component
    public void Start()
    {
        _animator = GameObject.Find("Player").GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _itemsManager = FindObjectOfType<ItemsManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _attackNotAllowed = FindObjectOfType<AttackNotAllowed>();
        playerCreated = true;
        canMove = true;
        speed = 5.0f;
        lastMovement = Vector2.zero;
        currentMovement = Vector2.zero;
    }

    // Player movements
    public void Update()
    {
        Vector3 pos = transform.position;

        if (isTalking)
        {
            walking = false;
            _animator.enabled = false;
            _rigidbody.velocity = Vector2.zero;
        
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
        }

        else if (Input.GetKey(KeyCode.S))
        {
            pos.y -= speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(0, -1);
            currentMovement = new Vector2(0, -1);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(1, 0);
            currentMovement = new Vector2(1, 0);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
            walking = true;
            lastMovement = new Vector2(-1, 0);
            currentMovement = new Vector2(-1, 0);
        }

        transform.position = pos;

        if (Input.GetKey(KeyCode.Mouse1) && (!_dialogueManager.dialogueActive && !_uiManager.inventoryPanel.activeInHierarchy) && !_attackNotAllowed.playerInZone)
        {
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.SLASH);
            attackTimeCounter = attackTime;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool(ATT, true);
            attackPressed = true;
        }

        if (Input.GetKeyDown("c"))
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
    public void LateUpdate()
    {
        if (!walking)
        {
            _rigidbody.velocity = Vector2.zero;
        }

        if (_dialogueManager.dialogueActive)
        {
            _animator.SetBool(ATT, false);
        }

        _animator.SetFloat(AXIS_H, currentMovement.x);
        _animator.SetFloat(AXIS_V, currentMovement.y);
        _animator.SetBool("Walking", walking);
        _animator.SetFloat("LastH", lastMovement.x);
        _animator.SetFloat("LastV", lastMovement.y);
    }
}
