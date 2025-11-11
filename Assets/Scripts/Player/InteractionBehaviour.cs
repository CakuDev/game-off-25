using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionBehaviour : MonoBehaviour
{
    [Tooltip("Input action to interact with objects.")]
    [SerializeField] private InputActionReference interactAction;

    private readonly List<InteractiveBaseBehaviour> m_interactiveObjects = new();
    private InteractiveBaseBehaviour m_objectToInteract = null;
    private InputAction m_interactAction;

    private void Start()
    {
        m_interactAction = interactAction.action;
    }

    private void Update()
    {
        CheckClosestInteractiveObject();
        // Interact if action is pressed and there's a valid interactiveObject
        if (m_interactAction.WasPressedThisFrame() && m_objectToInteract != null)
        {
            m_objectToInteract.Interact();
        }
    }

    private void CheckClosestInteractiveObject()
    {
        // Null if there's no interactive object in range
        if (m_interactiveObjects.Count == 0) 
        {
            m_objectToInteract = null;
            return; 
        }

        foreach(var interactiveObject in  m_interactiveObjects)
        {
            // Set as the objectToInteract if there's no other item already set
            if(m_objectToInteract == null)
            {
                m_objectToInteract = interactiveObject;
                
                continue;
            }

            // If interactiveObject is closer than current objectToInteract, set it as the objectToInteract
            float objectToInteractDistance = Vector2.Distance(transform.position, m_objectToInteract.transform.position);
            float interactiveObjectDistance = Vector2.Distance(transform.position, interactiveObject.transform.position);
            if (interactiveObjectDistance < objectToInteractDistance)
            {
                m_objectToInteract.DisableIndicator();
                m_objectToInteract = interactiveObject;
            }
        }

        m_objectToInteract.EnableIndicator();
    }

    public void AddInteractiveObject(InteractiveBaseBehaviour interactiveObject)
    {
        m_interactiveObjects.Add(interactiveObject);
    }

    public void RemoveInteractiveObject(InteractiveBaseBehaviour interactiveObject)
    {
        m_interactiveObjects.Remove(interactiveObject);
        if (m_objectToInteract == interactiveObject)
        {
            m_objectToInteract.DisableIndicator();
            m_objectToInteract = null;
        }
    }
}
