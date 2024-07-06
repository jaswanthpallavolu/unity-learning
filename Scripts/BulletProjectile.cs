using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        float speed = 10f;
        rigid.velocity = transform.forward * speed;
    }
    // void Update()
    // {
    //     float speed = 10f;
    //     rigid.velocity = transform.forward * speed;
    // }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Destroy(gameObject);
    }
}
