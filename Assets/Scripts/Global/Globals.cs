using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static bool CameraIsInHive = false;
    
    public static readonly int ProjectileDamage = 20;

    public static readonly float FireCooldown =  0.3f;

    public static readonly float ProjectileSpeed = 2f;

    public static readonly int ShotCost = 0;

    public static readonly int MaxWobbleAngle = 15;
    
    public static readonly Dictionary<string, GameResource> GameResources = new()
    {
        { "honey", new GameResource("honey", 0) }
    };

    public static readonly BuildingData[] Buildings =
    {
        new("Барак", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        }, string.Empty),
        new("Медохранилище", 100, new Dictionary<string, int>
        {
            { "honey", 1500 },
        }, string.Empty),
        new("Ратуша", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        }, string.Empty),
        new("Колеус", 100, new Dictionary<string, int>
        {
            { "honey", 1000 },
        }, string.Empty),
        new("Ромашка", 100, new Dictionary<string, int>
        {
            { "honey", 1500 },
        }, string.Empty),
        new("Подсолнух", 100, new Dictionary<string, int>
        {
            { "honey", 2000 },
        }, string.Empty),
        new("Куст", 100, new Dictionary<string, int>
        {
            { "honey", 500 },
        }, string.Empty)
    };

    public static readonly AttackWave[] AttackWaves =
    {
        new() { EnemyCount = 3, Duration = 5, Speed = 2, Scale = 0.5f, EnemyIndex = 0 },
        new() { EnemyCount = 10, Duration = 10, Speed = 5, Scale = 0.7f, EnemyIndex = 1 },
        new() { EnemyCount = 5000, Duration = 10, Speed = 2, Scale = 1f, EnemyIndex = 2 }
    };
    
    public static bool InBounds(Vector3 position)
    {
        return position.x > -12
               && position.y > -9 
               && position.x < 12 
               && position.y < 4.5f;
    }
}