using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Score += (int) collision.GetComponent<PlayerHP>().CurrentHP * 100;
            collision.GetComponent<PlayerController>().OnDie();
        }
    }
}
