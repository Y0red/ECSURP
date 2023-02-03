using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SpawnerComp : IComponentData
{
    public Entity Prifab;
    public float3 SpawnPosition;
    public float NextSpawnTime;
    public float SpawnRate;

}
