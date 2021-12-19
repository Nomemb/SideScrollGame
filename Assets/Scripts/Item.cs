using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Coin = 0, HP, Run}
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private int scorePoint = 100;

    private PlayerController playerController; 
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Use(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Use(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.Coin:
                playerController.Score += scorePoint;
                break;
            case ItemType.HP:
                player.GetComponent<PlayerHP>().CurrentHP += 10;
                break;
            case ItemType.Run:
                // n초동안 1.5배 가속후 원래 속도로 돌아옴.
                // 배경 및 장애물도 가속시켜야 함.
                //player.GetComponent<Movement2D>().moveSpeed =;
                break;
        }
    }
}
