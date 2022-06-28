using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent onTriggerEnter;

    [SerializeField] private bool isWorkWithSkill;
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        
        if (playerController != null)
        {
            if (!playerController.isShieldActive)
            {
                onTriggerEnter.Invoke();
            }
            else if (isWorkWithSkill)
            {
                onTriggerEnter.Invoke();
            }
        }
    }
}
