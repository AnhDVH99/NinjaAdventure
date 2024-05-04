using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class PlayerMovement : MonoBehaviour
{
    [Header("Config")] [SerializeField] private float speed;

    public Vector2 MoveDirection => moveDirection;

    private Player player;
    private PlayerAnimations playerAnimations;
    private PlayerActions actions;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;
    public bool isPaused;
    

    private void Awake()
    {
        player = GetComponent<Player>();
        actions = new PlayerActions();
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
        actions.GamePlay.Pause.performed += ctx => TogglePause();
    }


    private void Update()
    {
        if (!isPaused)
        {
            ReadMovement();
        }
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            Move();
        }
    }

    private void Move()
    {
        if (player.Stats.Health <= 0f) return;
        rb2D.MovePosition(rb2D.position + moveDirection * (speed * Time.fixedDeltaTime));
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;

        if (moveDirection == Vector2.zero)
        {
            playerAnimations.SetMoveBoolTransitions(false);
            return;
        }
        playerAnimations.SetMoveBoolTransitions(true);
        playerAnimations.SetMoveAnimation(moveDirection);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            moveDirection = Vector2.zero;
        }
    }
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}