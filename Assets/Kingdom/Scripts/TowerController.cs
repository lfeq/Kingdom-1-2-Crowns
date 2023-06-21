using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [HideInInspector] public GameObject moduleParent;
    [SerializeField] private float health;

    private void OnDestroy() {
        moduleParent.SetActive(true);
        GameManager gameManager = GameObject.Find("[GameManager]").GetComponent<GameManager>();
        gameManager.targets.Remove(gameObject);
        gameManager.InvokeDestroyTarget();
    }

    public void Damage() {
        print("Recibiendo daño");
        health -= 25;

        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
            Damage();
    }
}
