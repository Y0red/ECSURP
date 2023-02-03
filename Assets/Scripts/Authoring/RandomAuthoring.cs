﻿using Unity.Entities;
using UnityEngine;

public class RandomAuthoring : MonoBehaviour
{

}

public class RandomBaker : Baker<RandomAuthoring>
{
    public override void Bake(RandomAuthoring authoring)
    {
        AddComponent(new RandomComponent { random = new Unity.Mathematics.Random(5) });
    }
}
