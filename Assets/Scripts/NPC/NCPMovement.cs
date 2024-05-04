using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NCPMovement : MonoBehaviour
{
    [Header("Config")] [SerializeField] private float moveSpeed;

    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");
    private Animator animator;
    private Waypoint wayPoint;
    private Vector3 previousPos;
    private int currentPointIndex;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        wayPoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        Vector3 nextPos = wayPoint.GetPosition(currentPointIndex);
        UpdateMovePosition(nextPos);
        transform.position =
            Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPos) < 0.2f)
        {
            previousPos = nextPos;
            currentPointIndex = (currentPointIndex + 1) % wayPoint.Points.Length;
        }
    }

    private void UpdateMovePosition(Vector3 nextPos)
    {
        Vector2 dir = Vector2.zero;
        if (previousPos.x < nextPos.x) dir = new Vector2(1f, 0f);
        if (previousPos.x > nextPos.x) dir = new Vector2(-1f, 0);
        if (previousPos.y < nextPos.y) dir = new Vector2(0f, 1f);
        if (previousPos.y > nextPos.y) dir = new Vector2(0f, -1f);
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }
}