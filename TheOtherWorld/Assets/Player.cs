using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    private Rigidbody2D rb;      // Rigidbody2D를 참조
    private Vector2 movement;   // 이동 방향 저장

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. 입력 받기 (Input Manager 사용)
        movement.x = Input.GetAxisRaw("Horizontal"); // A, D 또는 왼/오 방향키
        movement.y = Input.GetAxisRaw("Vertical");   // W, S 또는 위/아래 방향키

        // 2. 방향 정규화
        movement = movement.normalized; // 대각선 이동 속도 보정
    }

    void FixedUpdate()
    {
        // 3. 물리 기반 이동 처리
        rb.velocity = movement * moveSpeed;
    }
}
