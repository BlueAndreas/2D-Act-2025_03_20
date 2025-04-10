using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    [Header("基础移动速度")]
    public float normalSpeed;
    [Header("加速移动速度")]
    public float chaseSpeed;
    [Header("当前移动速度")]
    public float currentSpeed;
    [Header("当前朝向")]
    public Vector3 faceDir;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        Move();
    }
    public virtual void Move()
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, rb.velocity.y);
    }
}
