using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [Tooltip("Maximum health.")]
    [SerializeField] int maxHealth;
    [Tooltip("Event called when damage is recieved. The param is the new current health.")]
    [SerializeField] UnityEvent<int> damaged;
    [Tooltip("Event called when healed. The param is the new current health.")]
    [SerializeField] UnityEvent<int> healed;
    [Tooltip("Event called when current health is equals to 0.")]
    [SerializeField] UnityEvent died;
    
    int m_currentHealth;

    private void Start()
    {
        m_currentHealth = maxHealth;
    }

    public void ChangeHealth(int variation)
    {
        m_currentHealth += variation;
        // Limit current health
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, maxHealth);
        
        // Call healed or damaged event depending on the sign of the variation
        if (variation > 0)
        {
            healed.Invoke(m_currentHealth);
        } else {
            damaged.Invoke(m_currentHealth);
        }
        if (m_currentHealth == 0) died.Invoke();
    }
}
