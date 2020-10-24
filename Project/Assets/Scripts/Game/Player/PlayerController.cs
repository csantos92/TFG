using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static bool playerCreated;   
    public float speed = 5.0f;
    private const string AXIS_H = "Horizontal", AXIS_V = "Vertical", ATT = "Attacking";
    private bool walking;
    public Vector2 lastMovement = Vector2.zero;
    public string nextUuid;

    private Rigidbody2D _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        playerCreated = true;
    }

    // Update is called once per frame
    void Update()
    {
        walking = false;

        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
        {
            _rigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), _rigidbody.velocity.y).normalized * speed;
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if(Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Input.GetAxisRaw(AXIS_V)).normalized * speed;
            walking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
        }
    }

    private void LateUpdate()
    {

        if (!walking)
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }


}
