using System.Runtime.CompilerServices;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public bool ShootPressed { get; private set; }
    public float moveInputX { get; set; }
    public float moveInputY { get; set; }

    private Vector2 aimDirection;
    private Camera mainCamera;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        InputManagement();
        UpdateActions();
    }

    private void InputManagement()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");
    }
    private void UpdateActions()
    {
        ShootPressed = Input.GetMouseButtonDown(0);
    }

    public Vector2 GetAimDirection(Vector3 playerPos)
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = playerPos.z;
        return (mouseWorldPos - playerPos).normalized;
    }
}
