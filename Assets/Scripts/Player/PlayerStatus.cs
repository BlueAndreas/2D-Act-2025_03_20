using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;
    [Header("玩家HP")]
    public float maxHP;
    public float currentHP;
    private bool isInvincible;
    private PlayerAnimation playerAnimation;
    public bool isKnockedBack = false;
    private Rigidbody2D rb;
    private Transform playerTransform;
    [Header("击退力度")]
    public float knockbackForce;
    [Header("击退持续时间")]
    private CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;
    public UnityEvent<Transform> OnTakeDamage;
    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckState();
    }
    public void TakeDamage(EnemyAttack enemyAttack, Transform enemyTransform)
    {
        if (isInvincible)
        {
            return;
        }
        else
        {
            currentHP -= enemyAttack.damage;
            playerAnimation.PlayerHurt();
            Knockback(enemyTransform); // 触发击退
            if (currentHP <= 0)
            {
                currentHP = 0;
                PlayerDie();
            }
        }
    }
    public void TriggerInvincible()
    {
        if (isInvincible==false)
        {
            isInvincible = true;
        }
    }
    public void ExitInvincible()
    {
        if (isInvincible == true)
        {
            isInvincible = false;
        }
    }
    public void Knockback(Transform enemyTransform)
    {
        // 计算击退方向（朝远离敌人的方向）
        Vector2 knockbackDir = new Vector2((playerTransform.position.x - enemyTransform.position.x),0).normalized;

        // 强制确保X方向的击退效果明显
        if (Mathf.Abs(knockbackDir.x) < 0.1f) 
            knockbackDir.x = Mathf.Sign(knockbackDir.x);
        if (Mathf.Abs(knockbackDir.y) < 0.1f) 
            knockbackDir.y = 0.5f; // 适当抬高Y值，防止击退太贴地

        rb.velocity = Vector2.zero; // 先清空速度
        Debug.Log("击退方向：" + knockbackDir * knockbackForce);
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        Debug.Log("施加击退力后速度：" + rb.velocity);
    }
    private void PlayerDie()
    {
        Debug.Log("玩家死亡");
    }
    public void SetKnockbackTrue()
    {
        isKnockedBack = true;
    }
    public void SetKnockbackFalse()
    {
        isKnockedBack = false;
    }
    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
}
