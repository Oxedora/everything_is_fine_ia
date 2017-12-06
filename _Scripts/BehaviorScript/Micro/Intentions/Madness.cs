using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AgentAllowed(typeof(Agent))]
[AgentAllowed(typeof(Adult))]
[AgentAllowed(typeof(Elder))]
[AgentAllowed(typeof(Worker))]
[AgentAllowed(typeof(Kid))]
public class Madness : Intention
{
    public Madness() : base()
    {
        Priority = 0f;
    }

    public override Vector3 DefaultState(Agent agent)
    {
        return Vector3.zero;
    }

    public override void UpdatePriority(Agent agent)
    {
        return;
    }
}
