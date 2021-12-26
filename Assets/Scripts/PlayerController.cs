using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private int jumpMaxCount = 2; // 한번에 가능한 점프 개수
    [SerializeField]
    private float jump = 1.0f; // 점프력
    [SerializeField]
    private float jumpTime = 1.2f; // 점프하는 동안 layer처리 막기 위한 시간
    private float timer = 0;
    private int jumpCurrentCount;
    public bool isGround = true;
    private bool isDie = false;
    private bool isJump = false;
    private bool isSlide = false;


    private int score;
    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;
    Animator animator;
    private int playerLayer, groundLayer;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        jumpCurrentCount = jumpMaxCount;
        playerLayer = LayerMask.NameToLayer("player");
        groundLayer = LayerMask.NameToLayer("ground");
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 0.2f, new Color(0, 1, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), 0.2f, LayerMask.GetMask("ground"));
        if (transform.position.y < -7) GetComponent<PlayerHP>().CurrentHP = 0;
        // 점프
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (jumpCurrentCount > 0 && isSlide == false)
            {
                isJump = true;
                animator.SetBool("isJump", true);
                rigidbody2D.velocity = Vector2.up * jump;
                timer = jumpTime;
                Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
                jumpCurrentCount--;
            }
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isJump = false;
            animator.SetBool("isJump", false);
            if (hit.collider != null)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
            }
        }
        // 슬라이드
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump == false)
            {
                isSlide = true;
                animator.SetBool("isSlide", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isSlide = false;
            animator.SetBool("isSlide", false);
        }
    }


    public void OnDie()
    {
        // 플레이어 사망시 씬 전환
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(nextSceneName);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCurrentCount = jumpMaxCount;
        }
    }

}
