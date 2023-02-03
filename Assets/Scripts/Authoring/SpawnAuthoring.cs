using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class SpawnAuthoring : MonoBehaviour
{

    public GameObject Prefab;
    public float Timer;
    public List<Transform> Path => GetComponentsInChildren<Transform>().Where(go => go.gameObject != this.gameObject).ToList();
}

public class SpawnBaker : Baker<SpawnAuthoring>
{
    public override void Bake(SpawnAuthoring authoring)
    {
        DynamicBuffer<Waypoints> path = AddBuffer<Waypoints>();
        foreach (var point in authoring.Path)
        {
            Waypoints wp = default;
            wp.value = point.position;
            path.Add(wp);
        }

        SpawnerData sd = default;
        sd.Prefab = GetEntity(authoring.Prefab);
        sd.Timer = authoring.Timer;
        sd.TimeToNextSpawn = authoring.Timer;
        AddComponent(sd);
    }
}