using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
[BurstCompile]
public partial struct InputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnDestroy(ref SystemState state)
    {
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
        DynamicBuffer<Towers> towers = SystemAPI.GetSingletonBuffer<Towers>();
        var ecbBOS = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var input in SystemAPI.Query<DynamicBuffer<TowerPlacementInput>>())
        {
            foreach (var placementInput in input)
            {
                if (physicsWorld.CastRay(placementInput.Value, out var hit))
                {
                    Debug.Log($"{hit.Position}");
                    Entity e = ecbBOS.Instantiate(towers[placementInput.index].Prefab);
                    ecbBOS.SetComponent(e, new LocalToWorld() { Value = ComputeTransform(math.round(hit.Position) + math.up()) });
                }
            }
            input.Clear();
        }

    }

    private float4x4 ComputeTransform(float3 pos)
    {
        return float4x4.Translate(new float3(pos));
    }
}