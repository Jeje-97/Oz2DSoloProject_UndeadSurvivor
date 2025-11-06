using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int pass;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int pass, Vector3 dir)
    {
        this.damage = damage;
        this.pass = pass;

        if (pass > -1)
        {
            rigid.velocity = dir * 15f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || pass == -1)
            return;
        pass--;

        if (pass == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
