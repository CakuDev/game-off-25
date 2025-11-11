using UnityEngine;

public class TestInteractiveBehaviour : InteractiveBaseBehaviour
{
    public override void Interact()
    {
        Debug.Log(name + " interacted!");
    }
}
