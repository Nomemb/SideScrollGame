using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float moveSpeed = 4.0f;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
    }

}
