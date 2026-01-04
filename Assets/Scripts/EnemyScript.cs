using UnityEngine;
using UnityEngine.AI;
public class EnemyScript : MonoBehaviour
{
    //damage functionailty is added later.... for the user
    // added in a seperate script
    //for enemy damage functionailty is added.
    public Transform firepoint;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //patrolling
    public Vector3 walkpoint;
    bool walkpointSet;
    public float walkpointRange;
    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void Patrolling()
    {
        if(!walkpointSet) SearchWalkPoint();
        if(walkpointSet)
        {
            agent.SetDestination(walkpoint);
        }
        Vector3 distanceToWalkpoint = transform.position - walkpoint;
        //walkpoint reached
        if(distanceToWalkpoint.magnitude < 1f)
        {
            walkpointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkpointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

    }
    private void AttackPlayer()
    {
        //make sure enemy dosent move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if(!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(projectile, firepoint.position /*+ transform.forward*/, firepoint.rotation).GetComponent<Rigidbody>();
            rb.GetComponent<BulletScript>().Targettag = "Player";
            Vector3 dir = (player.position - firepoint.position).normalized;
            rb.AddForce(dir * 32f, ForceMode.Impulse);

            Debug.Log("Attack Player");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
