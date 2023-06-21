using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected float horizontalSpeed = 0f;
    protected Vector3 leftDirection = new Vector3(0, 180, 0);
    protected Vector3 rightDirection = Vector3.zero;

    public abstract void EnterState(PlayerController player);
    public abstract void UpdateState(PlayerController player);
}

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.animator.SetInteger("State", 0);
    }

    public override void UpdateState(PlayerController player)
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * 10f * Time.deltaTime;

        if(horizontalSpeed != 0)
        {
            player.SwitchState(player.walkingState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.SwitchState(player.meleeAttackState);
        }
    }
}

public class PlayerWalkingState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.animator.SetInteger("State", 1);
    }

    public override void UpdateState(PlayerController player)
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * 10f * Time.deltaTime;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + horizontalSpeed);

        if (horizontalSpeed == 0)
        {
            player.SwitchState(player.idleState);
        }

        else if(horizontalSpeed > 0)
        {
            player.transform.localEulerAngles = rightDirection;
        }
        
        else if(horizontalSpeed < 0)
        {
            player.transform.localEulerAngles = leftDirection;
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.SwitchState(player.meleeAttackState);
        }
    }
}

public class PlayerMeleeAttackState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.animator.SetInteger("State", 2);
    }

    public override void UpdateState(PlayerController player)
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");

        if (horizontalSpeed != 0 && player.animationEnded)
        {
            player.SwitchState(player.walkingState);
        }
    }
}