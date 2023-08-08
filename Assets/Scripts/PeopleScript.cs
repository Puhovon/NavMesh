using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PeopleScript : MonoBehaviour
{
    private NavMeshAgent agent;

    private GameObject[] targets;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Target");
        agent.destination = targets[Random.Range(0, targets.Length)].transform.position;
    }

    private void Update()
    {
        if(agent.velocity.magnitude > 0.1)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

        if (agent.isOnOffMeshLink)
            OnMeshLink();
        
        if (agent.remainingDistance < 1f)
        {
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        int index = Random.Range(0, targets.Length);
        int seconds = Random.Range(3, 8);
        yield return new WaitForSeconds(seconds);
        agent.destination = targets[index].transform.position;
    }
    private void OnMeshLink()
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;

        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
        
        if(agent.transform.position == endPos)
            agent.CompleteOffMeshLink();
    }
}
