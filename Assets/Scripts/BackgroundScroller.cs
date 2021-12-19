using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    // 두 개의 배경이 서로가 타겟임
    [SerializeField]
    private float scrollRange = 38.9f;
    [SerializeField]
    private float moveSpeed = 4.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.left;


    private void Update()
    {
        // 배경이 moveDirection 방향으로 moveSpeed 속도만큼 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 배경이 설정된 범위를 벗어나면 위치 재설정
        if (transform.position.x <= -scrollRange)
        {
            transform.position = target.position + Vector3.right * scrollRange;
        }

    }

}
