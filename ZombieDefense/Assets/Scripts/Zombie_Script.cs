using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Script : MonoBehaviour
{
    private NavMeshAgent zombie;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        zombie = this.GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        zombie.SetDestination(player.transform.position);
    }
}
