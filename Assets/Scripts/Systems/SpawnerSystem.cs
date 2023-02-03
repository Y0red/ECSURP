using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using System;

[BurstCompile]
public partial struct SpawnerSystem //: ISystem
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
        ///<summary>
        ///Queries for all Spawner components. Uses RefRW because this system wants
        ///to read from and write to the component. if the system only needs read-only
        ///access, it would use ReFRO insted.
        ///</summary>
        
        //foreach(RefRW<SpawnerComp> spawner in SystemAPI.Query<RefRW<SpawnerComp>>())
        //{
        //    ProcessSpawner(ref state, spawner);
        //}
    }

    private void ProcessSpawner(ref SystemState state, RefRW<SpawnerComp> spawner)
    {
        //if the next spawn time has passed.
        if(spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            //spawn a new entity and positions it at he spawner.
            Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prifab);
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));

            //reset the next spawn time.
            spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
        }
        RefRW<RandomComponent> random = SystemAPI.GetSingletonRW<RandomComponent>();

        foreach (MoveToPositionAspect moveToPositionAspect in SystemAPI.Query<MoveToPositionAspect>())
        {
            moveToPositionAspect.Move(SystemAPI.Time.DeltaTime, random);
        }
    }
}
