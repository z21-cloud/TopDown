using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    //private Rigidbody2D rb;
    private Vector3 moveAmount;
    private Player player;
    private Dash dash;
    //private InputHandler inputHandler;
    public Vector3 MovementVector
    {
        get { return moveAmount; }
    }

    void Start()
    {
        player = ServiceLocator.Get<Player>();
        dash = GetComponent<Dash>();
        //inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dash != null && dash.IsDashing)
        {
            return;
        }
        PlayerMovement(InputManager.Instance.moveInputX, InputManager.Instance.moveInputY);
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    private void PlayerMovement(float x, float y)
    {
        GroundMovement(x, y);
    }

    private void GroundMovement(float x, float y)
    {
        Vector2 move = new Vector2(x, y);
        moveAmount = move.normalized * player.Speed;
        transform.position = transform.position + moveAmount * Time.deltaTime;
    }

    
}
