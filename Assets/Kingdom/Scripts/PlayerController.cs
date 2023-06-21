using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkingState walkingState = new PlayerWalkingState();
    public PlayerMeleeAttackState meleeAttackState = new PlayerMeleeAttackState();
    public Animator animator;

    public bool animationEnded = false;

    public delegate void MyDelegate();
    public event MyDelegate Dead;

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
        animationEnded = false;
    }

    public void ReturnToIlde()
    {
        currentState = idleState;
        currentState.EnterState(this);
        animationEnded = true;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Dead();
    }
}
