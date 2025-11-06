using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int pass;

    public void Init(float damage, int pass)
    {
        this.damage = damage;
        this.pass = pass;
    }
}
