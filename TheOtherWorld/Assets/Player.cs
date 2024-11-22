using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    private Rigidbody2D rb;      // Rigidbody2D�� ����
    private Vector2 movement;   // �̵� ���� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. �Է� �ޱ� (Input Manager ���)
        movement.x = Input.GetAxisRaw("Horizontal"); // A, D �Ǵ� ��/�� ����Ű
        movement.y = Input.GetAxisRaw("Vertical");   // W, S �Ǵ� ��/�Ʒ� ����Ű

        // 2. ���� ����ȭ
        movement = movement.normalized; // �밢�� �̵� �ӵ� ����
    }

    void FixedUpdate()
    {
        // 3. ���� ��� �̵� ó��
        rb.velocity = movement * moveSpeed;
    }
}
