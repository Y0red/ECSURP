using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct MoveToPositionAspect : IAspect
{
    private readonly Entity entity;

    private readonly TransformAspect transformAspect;
    private readonly RefRO<Speed> speed;
    private readonly RefRW<TargetPosition> targetPosition;


    public void Move(float deltaTime, RefRW<RandomComponent> randPos)
    {
        float3 direction = math.normalize(targetPosition.ValueRW.value - transformAspect.LocalPosition);

        transformAspect.LocalPosition += direction * deltaTime * speed.ValueRO.value;

        float reachedTargetDistance = .5f;
        if (math.distance(transformAspect.LocalPosition, targetPosition.ValueRW.value) < reachedTargetDistance)
        {
            //generate new transforms
            targetPosition.ValueRW.value = GetRandomPositions(randPos);
        }
    }
    public void Move2(float deltaTime)
    {
        float3 direction = math.normalize(targetPosition.ValueRW.value - transformAspect.LocalPosition);

        transformAspect.LocalPosition += direction * deltaTime * speed.ValueRO.value;


    }
    public void TestReachedTargetPosition(RefRW<RandomComponent> randPos)
    {
        float reachedTargetDistance = .5f;
        if (math.distance(transformAspect.LocalPosition, targetPosition.ValueRW.value) < reachedTargetDistance)
        {
            //generate new transforms
            targetPosition.ValueRW.value = GetRandomPositions(randPos);
        }
    }
    private float3 GetRandomPositions(RefRW<RandomComponent> random)
    {
        return new float3(random.ValueRW.random.NextFloat(0f,15f), 0, random.ValueRW.random.NextFloat(0f, 15f));
    }
}
