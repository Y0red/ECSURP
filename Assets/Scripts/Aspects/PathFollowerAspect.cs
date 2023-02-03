using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct PathFollowerAspect : IAspect
{
    [Optional]
    readonly RefRO<Speed> speed;
    readonly RefRW<NextPathIndex> pathIndex;
    [ReadOnly]
    readonly DynamicBuffer<Waypoints> path;
    readonly TransformAspect transform;

    public void FollowPath(float time)
    {
        float3 direction = path[pathIndex.ValueRO.value].value - transform.LocalPosition;
        if (math.distance(transform.LocalPosition, path[pathIndex.ValueRO.value].value) < 0.1f)
        {
            pathIndex.ValueRW.value = (pathIndex.ValueRO.value + 1) % path.Length;
        }
        float movementSpeed = speed.IsValid ? speed.ValueRO.value : 1;
        transform.LocalPosition += math.normalize(direction) * time * movementSpeed;

        transform.LookAt(path[pathIndex.ValueRO.value].value);
    }

    public bool HasReachedEndOfPath()
    {
        return math.distance(transform.LocalPosition, path[path.Length - 1].value) < 0.1f;
    }
}
