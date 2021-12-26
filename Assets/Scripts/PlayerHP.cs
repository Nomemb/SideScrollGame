using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100;
    private float currentHP;
    [SerializeField]
    private float tickDamage = 1.0f;
    [SerializeField]
    private float timeInvincible = 2.0f; // 데미지를 받았을 때 무적시간
    private bool isInvincible = false;
    private float invincibleTimer;
    private SpriteRenderer spriteRenderer;
    private PlayerController player;

    public float MaxHP => maxHP;
    public float CurrentHP
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (currentHP <= 0)
        {
            player.OnDie();
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        TickDamage(tickDamage);
    }

    public void TickDamage(float tickDamage)
    {
        currentHP -= Time.deltaTime * tickDamage;
    }
    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            return;
        }
        isInvincible = true;
        invincibleTimer = timeInvincible;

        currentHP -= damage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");
    }

    private IEnumerator HitColorAnimation()
    {
        // 플레이어 색상을 빨간색으로 변경
        spriteRenderer.color = Color.red;
        // 0.1 초 대기
        yield return new WaitForSeconds(0.1f);
        // 플레이어 색상을 원래 색으로 다시 변경
        spriteRenderer.color = Color.white;
    }
}
