using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SecurityScript : MonoBehaviour
{
    private NavMeshAgent _agent;
    private List<GameObject> peoples = new List<GameObject>();
    private Animator _animator;
    [SerializeField] private int currentIndex = 0;
    private bool notStop = true;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        peoples = GameObject.FindGameObjectsWithTag("People").ToList();
        currentIndex = Random.Range(0, peoples.Count);
        _agent.destination = peoples[currentIndex].transform.position;
    }

    private void Update()
    {
        if(_agent.velocity.magnitude > 0.1f)
            _animator.SetBool("Walk",true);
        else 
            _animator.SetBool("Walk", false);
        
        if (notStop)
        {
            if (_agent.remainingDistance < 3f)
            {
                StartCoroutine(Check());
                notStop = !notStop;
            }
            _agent.destination = peoples[currentIndex].transform.position;    
        }
    }

    private IEnumerator Check()
    {
        print("Start Coroutine");
        currentIndex = Random.Range(0, peoples.Count);
        _agent.destination = _agent.transform.position;
        yield return new WaitForSeconds(3);
        notStop = !notStop;
        print("Stop Coroutine");
    }
}
