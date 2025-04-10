using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测球位移校正")]
    public Vector2 bottomOffset;
    [Header("是否在地面")]
    public bool isGround;
    [Header("是否在空中")]
    public bool isAir;
    [Header("是否在攀爬")]
    public bool isClimb;
    [Header("检测球半径")]
    public float checkRadius;
    [Header("能否二段跳")]
    public bool canDoubleJump;
    public LayerMask platformLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Check();
    }
    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, platformLayer);
        if (isGround)
        {
            isAir = false;
            canDoubleJump = true;
        }
        else
        {
            isAir = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius); 
    }
}
