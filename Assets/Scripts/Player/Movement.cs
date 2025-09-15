using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    //private Rigidbody2D rb;
    private Vector3 moveAmount;
    private Player player;
    private InputHandler inputHandler;
    public Vector3 MovementVector
    {
        get { return moveAmount; }
    }

    void Start()
    {
        player = GetComponent<Player>();
        inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(inputHandler.moveInputX, inputHandler.moveInputY);
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
