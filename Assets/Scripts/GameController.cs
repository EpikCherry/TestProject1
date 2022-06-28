using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WorldGeneration worldGeneration;
    [SerializeField] private Transform endPoint;

    [SerializeField] private float coolDownTime = 2f;

    private void Awake()
    {
        worldGeneration.GenerateWorld();
        StartCoroutine(ActivatePlayer());
    }

    private IEnumerator ActivatePlayer()
    {
        yield return new WaitForSeconds(coolDownTime);
        playerController.SetTarget(endPoint.position);
    }
}