using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //public으로 있으므로 다른 곳에서도 끌어서 쓸 수 있다는 것을 알고 있어야 함
    public float damage = 20f;
    public float speed = 1000f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
