using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class FollowPlayer : MonoBehaviour
{
    GameObject player;
    new Rigidbody rigidbody;
    [SerializeField] float speed = 200;
    [SerializeField] float distance = 10;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        if (player && Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            var direction = player.transform.position - transform.position;
            rigidbody.AddForce(direction.normalized * speed * Time.smoothDeltaTime);
        }
    }
}
