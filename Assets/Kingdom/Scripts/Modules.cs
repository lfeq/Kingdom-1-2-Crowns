using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modules : MonoBehaviour
{
    public GameObject module;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject instance = Instantiate(module, transform.position, Quaternion.identity);
            gameManager.targets.Add(instance);
            instance.GetComponent<TowerController>().moduleParent = gameObject;
            gameObject.SetActive(false);
        }
    }
}
