using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Íæ¼ÒÉËº¦")]
    public int damage_1;
    public int damage_2;
    public int damage_3;
    [Header("Íæ¼Ò¹¥»÷·¶Î§")]
    public float attackRange;
    [Header("Íæ¼Ò¹¥»÷ÆµÂÊ")]
    public float attackRate;
    private PlayerAnimation playerAnimation;
    [Header("Íæ¼ÒÊÇ·ñ¹¥»÷")]
    public bool isAttack;

    private PlayerInputController inputController;
    // Start is called before the first frame update
    void Awake()
    {
        inputController = new PlayerInputController();
        inputController.Gameplay.Attack.started += Attack;
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        inputController.Enable();
    }
    private void OnDisable()
    {
        inputController.Disable();
    }
    private void Attack(InputAction.CallbackContext obj)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }
}
