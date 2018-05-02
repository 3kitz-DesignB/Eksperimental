using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct DayCycle : IComponentData
{
    public float Speed;
}

public class DayCycleComponent : ComponentDataWrapper<DayCycle>
{
}