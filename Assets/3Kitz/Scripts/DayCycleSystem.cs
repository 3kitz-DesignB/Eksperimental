using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class DayCycleSystem : ComponentSystem
{
    private static readonly float3 RotationVector = new float3(1f, 0f, 0f);

    private ComponentGroup group;

    protected override void OnUpdate()
    {
        const float TwoPi = Mathf.PI * 2f;
        const float InverseSecondsPerDay = 0.000011574074074074074074074074074074f;

        var entities = this.group.GetEntityArray();
        var rotations = this.group.GetComponentDataArray<Rotation>();
        var dayCycles = this.group.GetComponentDataArray<DayCycle>();
        for (int typeIndex = 0; typeIndex < entities.Length; typeIndex++)
        {
            var dayCycle = dayCycles[typeIndex];
            var rot = rotations[typeIndex];
            
            rot.Value = math.mul(
                rot.Value,
                math.axisAngle(
                    RotationVector,
                    ((Time.deltaTime * dayCycle.Speed * TwoPi) * InverseSecondsPerDay) % TwoPi));

            rotations[typeIndex] = rot;
        }
    }

    protected override void OnCreateManager(int capacity) =>
        this.group = this.GetComponentGroup(
            typeof(Rotation),
            ComponentType.ReadOnly(typeof(DayCycle)),
            ComponentType.ReadOnly(typeof(Light)));
}
