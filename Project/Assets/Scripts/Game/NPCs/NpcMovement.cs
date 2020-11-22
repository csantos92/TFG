using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{

    public float speed = 1.5f;
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    public bool isWalking = false;
    public bool isTalking;

    public float walkTime = 1.5f;
    private float walkCounter;

    public float waitTime = 4.0f;
    private float waitCounter;

    private Vector2[] walkingDirections =
    {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };
    private int currentDirection;

    public Vector2 directionToMove;


    public BoxCollider2D npcZone;
    //private int forbiddenDirection;

    //private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        isTalking = false;
        //dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isTalking)
        {
            //isTalking = dialogueManager.dialogueActive;
            StopWalking();
            return;
        }

        if (isWalking)
        {
            if(this.transform.position.x < npcZone.bounds.min.x ||
                 this.transform.position.x > npcZone.bounds.max.x ||
                 this.transform.position.y < npcZone.bounds.min.y ||
                 this.transform.position.y > npcZone.bounds.max.y)
            {
                 GoToTheOpposite();
            }
        
            _rigidBody.velocity = walkingDirections[currentDirection] * speed;
            walkCounter -= Time.fixedDeltaTime;
            if(walkCounter < 0)
            {
                StopWalking();
            }
            
        }
        else
        {
            _rigidBody.velocity = Vector2.zero;
            waitCounter -= Time.fixedDeltaTime;
            if (waitCounter < 0)
            {
                directionToMove = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));

                StartWalking(Random.Range(0, walkingDirections.Length));
            }
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("Walking", isWalking);
        _animator.SetFloat("Horizontal", walkingDirections[currentDirection].x);
        _animator.SetFloat("Vertical", walkingDirections[currentDirection].y);
        _animator.SetFloat("Last_H", directionToMove.x);
        _animator.SetFloat("Last_V", directionToMove.y);

    }

    public void GoToTheOpposite()
    {
        if(this.transform.position.x < npcZone.bounds.min.x)
        {
            StartWalking(3);
            directionToMove = new Vector2(1, 0);
        }else if(this.transform.position.x > npcZone.bounds.max.x)
        {
            StartWalking(2);
            directionToMove = new Vector2(-1, 0);
        }else if(this.transform.position.y < npcZone.bounds.min.y)
        {
            StartWalking(0);
            directionToMove = new Vector2(0, 1);
        }
        else if (this.transform.position.y > npcZone.bounds.max.y)
        {
            StartWalking(1);
            directionToMove = new Vector2(0, -1);
        }
    }

    public void StartWalking(int direction)
    {
        currentDirection = direction; /*Random.Range(0, walkingDirections.Length);*/
        isWalking = true;
        walkCounter = walkTime;
    }

    public void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
        _rigidBody.velocity = Vector2.zero;
    }
}
