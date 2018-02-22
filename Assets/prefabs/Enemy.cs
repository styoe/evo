using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    // NavMeshAgent agent;
    GameObject player;
    new Rigidbody rigidbody;
    [SerializeField] float speed = 100;

    void Awake()
    {
        // agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player && Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            //     agent.SetDestination(player.transform.position);
            var direction = player.transform.position - transform.position;
            rigidbody.AddForce(direction.normalized * speed * Time.smoothDeltaTime);
        }

    }
}
