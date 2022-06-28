using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject playerDieParticle;
    [SerializeField] private Material defaultMat;
    [SerializeField] private Material shieldMat;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform startPos;

    private Vector3 _target;
    private bool isCoolDownEnd = true;
    
    public bool isShieldActive { get; set; }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        navMeshAgent.SetDestination(target);
    }

    public void ShieldActive(bool active)
    {
        isShieldActive = active;
        meshRenderer.material = active ? shieldMat : defaultMat;
        if (active && isCoolDownEnd) StartCoroutine(CoolDownShield());
    }
    
    public void SpawnParticle()
    {
        Instantiate(playerDieParticle, transform.position + Vector3.up/2, Quaternion.identity);
    }
    public void ResetPosition()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;
        transform.position = startPos.position;
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement()
    {
        yield return new WaitForSeconds(2f);
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        SetTarget(_target);
    }

    private IEnumerator CoolDownShield()
    {
        isCoolDownEnd = false;
        yield return new WaitForSeconds(2f);
        isShieldActive = false;
        meshRenderer.material = defaultMat;
        isCoolDownEnd = true;
    }
}
