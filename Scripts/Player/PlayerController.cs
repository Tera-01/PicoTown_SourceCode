using Constants;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform tf;
    public float moveSpeed;
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        JoystickMovement.Instance.OnMove += SetDir;
    }

    void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("bed"))
        {
            GameManager.instance.PauseTime();
            UIManager.instance.GetUI<UISleepInBed>();
        }
    }
    private void Move()
    {
        rb.velocity = new Vector2(JoystickMovement.Instance.joyVec.x * moveSpeed, JoystickMovement.Instance.joyVec.y * moveSpeed);
    }

    private Direction _curDirection = Direction.DOWN;
    public event Action<Direction> OnDirectionChanged;
    
    public Direction CurDirection
    {
        get { return _curDirection; }
        set
        {
            _curDirection = value;
            OnDirectionChanged?.Invoke(_curDirection);
        }
    }

    public void SetDir(int move)
    {
        float inputX = rb.velocity.x;
        float inputY = rb.velocity.y;

        if (inputX == 0 || inputY == 0)
        {
            return;
        }

        float gradient = inputY / inputX;

        if (Math.Abs(gradient) > 2)
        {
            CurDirection = inputY > 0 ? Direction.UP : Direction.DOWN;
        }
        else
        {
            CurDirection = inputX > 0 ? Direction.RIGHT : Direction.LEFT;
        }
    }
}
