using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private Vector3 targetPosition;
    [SerializeField] private GameObject burnEffect;
    public EnemyPortal portal;

    public EnemyStates currentState;
    public EnemyStates idleState = new EnemyIdle();
    public EnemyStates walkState = new EnemyWalk();
    public EnemyStates attackState = new EnemyAttack();
    public EnemyStates dieState = new EnemyDie();
    [HideInInspector] public Animator animator;

    private void Start()
    {
        SetTarget();
        animator = GetComponent<Animator>();
        targetPosition = new Vector3(0, transform.position.y, target.transform.position.z);
        currentState = idleState;
        currentState.EnterState(this);
        GameManager.DayArrived += Burn;
        GameManager.DayArrived += AutoDestroy;
        GameManager.DestroyedTarget += ChangeTarget;
    }

    private void Update()
    {
        currentState.UpdateState(this);
        transform.LookAt(Vector3.zero);
    }

    public void SwitchState(EnemyStates state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
    }

    public void SetWalkingState()
    {
        if(currentState == idleState)
        {
            currentState = walkState;
            currentState.EnterState(this);
        }
    }

    private void Burn()
    {
        burnEffect.SetActive(true);
        currentState = dieState;
        currentState.EnterState(this);
    }

    public void AutoDestroy()
    {
        portal.EnemyDied();
        Destroy(gameObject, 2);
    }

    private void SetTarget() {
        GameManager gameManager = GameObject.Find("[GameManager]").GetComponent<GameManager>();
        List<GameObject> targets = gameManager.targets;
        List<float> distanceToTargets = new List<float>();
        foreach (GameObject target in targets) {
            distanceToTargets.Add(Vector3.Distance(transform.position, target.transform.position));
        }
        int min = distanceToTargets.IndexOf(distanceToTargets.Min());
        target = gameManager.targets[min];
    }

    private void ChangeTarget() {
        GameManager gameManager = GameObject.Find("[GameManager]").GetComponent<GameManager>();
        List<GameObject> targets = gameManager.targets;
        List<float> distanceToTargets = new List<float>();
        foreach (GameObject target in targets) {
            distanceToTargets.Add(Vector3.Distance(transform.position, target.transform.position));
        }
        int min = distanceToTargets.IndexOf(distanceToTargets.Min());
        target = gameManager.targets[min];
        targetPosition = new Vector3(0, transform.position.y, target.transform.position.z);
        currentState = walkState;
        currentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Burn();
        }
    }
}
