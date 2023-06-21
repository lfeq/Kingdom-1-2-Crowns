using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStates
{
    public abstract void EnterState(EnemyController enemy);

    public abstract void UpdateState(EnemyController enemy);
}

public class EnemyIdle : EnemyStates
{
    public override void EnterState(EnemyController enemy)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyController enemy)
    {
        //throw new System.NotImplementedException();
    }
}

public class EnemyWalk : EnemyStates
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.animator.SetInteger("State", 1);
    }

    public override void UpdateState(EnemyController enemy)
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.GetTargetPosition(), Time.deltaTime * 3);

        float distance = Vector3.Distance(enemy.transform.position, enemy.GetTargetPosition());

        if (distance <= 1.3f)
        {
            enemy.SwitchState(enemy.attackState);
        }
    }
}

public class EnemyAttack : EnemyStates
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.animator.SetInteger("State", 2);
    }

    public override void UpdateState(EnemyController enemy)
    {
        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.GetTargetPosition(), Time.deltaTime * 3); ;
    }
}

public class EnemyDie : EnemyStates
{
    public override void EnterState(EnemyController enemy)
    {
        enemy.animator.SetInteger("State", 3);
    }

    public override void UpdateState(EnemyController enemy)
    {
        Debug.Log("Muerto");
    }
}
