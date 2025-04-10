using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("敌人受伤无敌时间")]
    public float invincibleTime;
    private float invincibleCounter;
    public bool isInvincible = false;
    [Header("敌人HP")]
    public float maxHP;
    public float currentHP;
    private PlayerAttack playerAttack;
    private PlayerStatus playerStatus;
    private EnemyAttack enemyAttack;
    private Transform enemyTransform;
    private Rigidbody2D rb;
    private Transform playerTransform;
    [Header("击退力度")]
    public float knockbackForce;
    private Animator animator;
    private void Awake()
    {
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        playerStatus = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Player Transform Found");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invincibleCounter -= Time.deltaTime;
            if (invincibleCounter <= 0) { }
            {
                isInvincible = false;
            }
        }
            
    }

    private void TriggerInvincible()
    {
        if (isInvincible == false)
        {
            isInvincible = true;
            Debug.Log("Enemy Invincible");
            invincibleCounter = invincibleTime;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.CompareTag("Attack 1")) || (other.CompareTag("Attack 2")) || (other.CompareTag("Attack 3")))
        {
            TakeDamage(other,playerAttack);
            animator.SetBool("hurt",true);
            TriggerInvincible();
        }
        if (other.CompareTag("Player"))
        {
            playerStatus.TakeDamage(enemyAttack, enemyTransform);
        }
    }
    public void ExitHurt()
    {
        if(animator.GetBool("hurt"))
            animator.SetBool("hurt", false);
    }
    public void TakeDamage(Collider2D other,PlayerAttack playerAttack)
    {
        if (other.CompareTag("Attack 1"))
            currentHP -= playerAttack.damage_1;
        else if (other.CompareTag("Attack 2"))
            currentHP -= playerAttack.damage_2;
        else if (other.CompareTag("Attack 3"))
        {
            currentHP -= playerAttack.damage_3;
            Knockback(playerTransform); // 触发击退
        }
        if (currentHP <= 0)
        {
            Debug.Log("Enemy Death");
        }
    }
    public void Knockback(Transform playerTransform)
    {
        Debug.Log("Enemy触发击退");
        // 计算击退方向（朝远离敌人的方向）
        Vector2 knockbackDir = new Vector2((enemyTransform.position.x - playerTransform.position.x), 0).normalized;

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
}
