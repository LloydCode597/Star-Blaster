using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float leftBoundPadding;
    [SerializeField] float rightBoundPadding;
    [SerializeField] float upBoundPadding;
    [SerializeField] float downBoundPadding;


    InputAction moveAction;

    Vector3 moveVector;
    Vector2 minBounds;
    Vector2 maxBounds;


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        InitBounds();
    }

    void Update()
    {
        MovePlayer();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    void MovePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        Vector3 newPos = transform.position + moveVector * moveSpeed * Time.deltaTime;

        newPos.x = Math.Clamp(newPos.x, minBounds.x + leftBoundPadding, maxBounds.x - rightBoundPadding);
        newPos.y = Math.Clamp(newPos.y, minBounds.y + downBoundPadding, maxBounds.y - upBoundPadding);
        transform.position = newPos;
    }

}
