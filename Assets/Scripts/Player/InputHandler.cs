using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float moveInputX { get; set; }
    public float moveInputY { get; set; }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void InputManagement()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");
    }
}
