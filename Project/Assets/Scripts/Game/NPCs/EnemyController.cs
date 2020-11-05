﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.5f;
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    public bool isWalking = false;
    public bool isTalking;
    private bool playerInTheZone;

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
    public BoxCollider2D enemyZone;
    public CircleCollider2D playerInRange;
    private GameObject player;

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
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInTheZone)
        {
            if (transform.position.x < player.transform.position.x)
            {
                walkTime = 30;
                StartWalking(3);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                //EnemyFix();
            }

            if (transform.position.x > player.transform.position.x)
            {
                walkTime = 30;
                StartWalking(2);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                //EnemyFix();
            }

            if (transform.position.y < player.transform.position.y)
            {
                walkTime = 30;
                StartWalking(0);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
               // EnemyFix();
            }

            if (transform.position.y > player.transform.position.y)
            {
                walkTime = 30;
                StartWalking(1);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
               // EnemyFix();
            }
        }

            if (isTalking)
        {
            //isTalking = dialogueManager.dialogueActive;
            StopWalking();
            return;
        }

        if (isWalking)
        {
            if (this.transform.position.x < enemyZone.bounds.min.x ||
                 this.transform.position.x > enemyZone.bounds.max.x ||
                 this.transform.position.y < enemyZone.bounds.min.y ||
                 this.transform.position.y > enemyZone.bounds.max.y)
            {
                GoToTheOpposite();
            }

            _rigidBody.velocity = walkingDirections[currentDirection] * speed;
            walkCounter -= Time.fixedDeltaTime;
            if (walkCounter < 0)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = false;
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
        if (this.transform.position.x < enemyZone.bounds.min.x)
        {
            StartWalking(3);
            directionToMove = new Vector2(1, 0);
        }
        else if (this.transform.position.x > enemyZone.bounds.max.x)
        {
            StartWalking(2);
            directionToMove = new Vector2(-1, 0);
        }
        else if (this.transform.position.y < enemyZone.bounds.min.y)
        {
            StartWalking(0);
            directionToMove = new Vector2(0, 1);
        }
        else if (this.transform.position.y > enemyZone.bounds.max.y)
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

    public void EnemyFix()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            StopWalking();
        }
    }
}