using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public float speed, walkTime, waitTime;
    public bool isTalking, isWalking;
    private float waitCounter, walkCounter;
    private int currentDirection;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    public BoxCollider2D npcZone;
    public Vector2 directionToMove;
    private Vector2[] walkingDirections =
    {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        speed = 1.5f;
        walkTime = 1.5f;
        waitTime = 4.0f;
        waitCounter = waitTime;
        walkCounter = walkTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTalking)
        {
            StopWalking();
            return;
        }
        else
        {
            StartWalking(currentDirection);
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
