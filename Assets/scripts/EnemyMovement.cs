using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Reference to the player
    private Transform player;

    // NavMeshAgent component
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        // Get NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Automatically find the player using the Player tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If player exists, chase them
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}