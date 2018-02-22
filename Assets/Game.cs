using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    public GameObject cookie;
    private int maxCookies;

    public GameObject powerup;
    private int maxPowerups;
    string[] powerupTypes = new string[] { "Fly", "Jump" };

    public GameObject rock;
    private int maxRocks;

    public GameObject bomb;
    private int maxBombs;

    public GameObject player;

    public GameObject floor;
    public List<GameObject> floors;

    const int maxEnemies = 80;
    GameObject enemy;

    public float GameGridRadiusInitial;
    public float GameGridRadius;
    public float GameMaxScaleDifference;

    void Start()
    {
        GameGridRadiusInitial = 50;
        GameGridRadius = 50;
        GameMaxScaleDifference = 10;

        enemy = Resources.Load("Enemy") as GameObject;

        floors = new List<GameObject>();
        InvokeRepeating("CreateFloor", 0f, 1f);

        InvokeRepeating("CreateEnemy", 2, 0.3f);
        InvokeRepeating("CreateEnemy", 2, 2);

        maxCookies = 80;
        InvokeRepeating("CreateCookie", 2.0f, 0.3f);
        InvokeRepeating("CreateCookie", 2.0f, 2);

        maxPowerups = 20;
        InvokeRepeating("CreatePowerup", 2.0f, 0.3f);
        InvokeRepeating("CreatePowerup", 2.0f, 2f);

        maxRocks = 200;
        InvokeRepeating("CreateRock", 2.0f, 0.03f);
        InvokeRepeating("CreateRock", 2.0f, 2f);

        maxBombs = 30;
        InvokeRepeating("CreateBomb", 2.0f, 0.03f);
        InvokeRepeating("CreateBomb", 2.0f, 10f);


        var ExplosionCenter = new Vector3(2, 2, 2);
        float ExplosionRange = 10f;
        Collider[] colliders = Physics.OverlapSphere(ExplosionCenter, ExplosionRange);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(500f, ExplosionCenter, ExplosionRange, 100.0F);
        }
    }

    void FixedUpdate()
    {
        GameGridRadius = GameGridRadiusInitial * Mathf.Round(player.transform.localScale.x);

        Debug.Log(player.transform.localScale.x);
        Debug.Log(GameGridRadius);

        if (player.transform.localScale.x * GameGridRadius > floor.transform.localScale.x)
        {
            var floorScale = player.transform.localScale.x * GameGridRadius * 2;
            floor.transform.localScale = new Vector3(floorScale, floorScale, floorScale);
        }
    }

    Vector3 GetRandomInGameGridRadius()
    {
        var pos = player.transform.localPosition;
        return new Vector3(Random.Range(pos.x - GameGridRadius, pos.x + GameGridRadius), 1, Random.Range(pos.z - GameGridRadius, pos.z + GameGridRadius));
    }

    void CreateFloor()
    {
        var playerPos = player.transform.localPosition;

        var floorsInRadius = 1f;
        var floorScale = GameGridRadius * 2;
        var playerClosestX = Mathf.Round(playerPos.x / floorScale) * floorScale;
        var playerClosestZ = Mathf.Round(playerPos.z / floorScale) * floorScale;

        // Destroy floors out of range and scale the current ones to current scale
        foreach (var floorInstance in floors.ToArray())
        {
            if (0.1 * floorInstance.transform.localScale.x < floorScale)
            {
                floorInstance.transform.localScale = new Vector3(0.1f * floorScale, 0.1f, 0.1f * floorScale);
            }

            var floorInstancePos = floorInstance.transform.localPosition;
            if (
                floorInstancePos.x < playerClosestX - floorsInRadius * floorScale
                 || floorInstancePos.x > playerClosestX + floorsInRadius * floorScale
                 || floorInstancePos.z < playerClosestZ - floorsInRadius * floorScale
                 || floorInstancePos.z > playerClosestZ + floorsInRadius * floorScale
             )
            {
                floors.Remove(floorInstance);
                Destroy(floorInstance);
            }
        }

        // Create floors in range
        for (var x = playerClosestX - floorsInRadius * floorScale; x <= playerClosestX + floorsInRadius * floorScale; x += floorScale)
        {
            for (var z = playerClosestZ - floorsInRadius * floorScale; z <= playerClosestZ + floorsInRadius * floorScale; z += floorScale)
            {
                bool floorExists = floors.Exists(m => m.transform.localPosition.x == x && m.transform.localPosition.z == z);

                if (!floorExists)
                {
                    var newFloor = Instantiate(floor, new Vector3(x, 0, z), Quaternion.identity);
                    newFloor.name = "Floor";
                    // newFloor.GetComponent<Renderer>().bounds = new Vector3(floorScale, 0,  floorScale);
                    // Debug.Log();
                    newFloor.tag = "Floor";
                    newFloor.transform.localScale = new Vector3(0.1f * floorScale, 0.1f, 0.1f * floorScale);
                    floors.Add(newFloor);
                }
            }
        }
    }

    void CreateEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Cookie");

        // Destroy the ones out of range
        foreach (var c in enemies)
        {
            if (Vector3.Distance(player.transform.localPosition, c.transform.localPosition) > GameGridRadius || Vector3.Distance(player.transform.localScale, c.transform.localScale) > GameMaxScaleDifference)
            {
                Destroy(c);
            }
        }

        if (enemies.GetLength(0) < maxEnemies)
        {
            var playerScale = player.transform.localScale.x;
            var newEnemy = Instantiate(enemy, GetRandomInGameGridRadius(), Quaternion.identity);
            newEnemy.name = "Cookie";
            newEnemy.tag = "Cookie";

            float enemyScale = Random.Range(0.3f * playerScale, 3f * playerScale);
            newEnemy.GetComponent<NavMeshAgent>().radius = enemyScale;
            Animate.Scale(newEnemy, new Vector3(enemyScale, enemyScale, enemyScale));

            // var cookieSphereCollider = newEnemy.gameObject.GetComponent<SphereCollider>(); 
            // Debug.Log(cookieScale);
            // cookieSphereCollider.radius = cookieScale / 2f; 
        }
    }

    void CreateCookie()
    {
        var cookies = GameObject.FindGameObjectsWithTag("Cookie");

        // Destroy the ones out of range
        foreach (var c in cookies)
        {
            if (Vector3.Distance(player.transform.localPosition, c.transform.localPosition) > GameGridRadius || Vector3.Distance(player.transform.localScale, c.transform.localScale) > GameMaxScaleDifference)
            {
                Destroy(c);
            }
        }

        if (cookies.GetLength(0) < maxCookies)
        {
            var playerScale = player.transform.localScale.x;
            var newCookie = Instantiate(cookie, GetRandomInGameGridRadius(), Quaternion.identity);
            newCookie.name = "Cookie";
            newCookie.tag = "Cookie";

            float cookieScale = Random.Range(0.3f * playerScale, 3f * playerScale);
            Animate.Scale(newCookie, new Vector3(cookieScale, cookieScale, cookieScale));

            // var cookieSphereCollider = newCookie.gameObject.GetComponent<SphereCollider>(); 
            // Debug.Log(cookieScale);
            // cookieSphereCollider.radius = cookieScale / 2f; 
        }
    }

    void CreatePowerup()
    {
        var powerups = GameObject.FindGameObjectsWithTag("Powerup");

        // Destroy the ones out of range
        foreach (var p in powerups)
        {
            if (Vector3.Distance(player.transform.localPosition, p.transform.localPosition) > GameGridRadius || Vector3.Distance(player.transform.localScale, p.transform.localScale) > GameMaxScaleDifference)
            {
                Destroy(p);
            }
        }

        if (powerups.GetLength(0) < maxPowerups)
        {
            var playerScale = player.transform.localScale.x;
            var newPowerup = Instantiate(powerup, GetRandomInGameGridRadius(), Quaternion.identity);
            var powerupType = powerupTypes[Random.Range(0, powerupTypes.Length)];
            newPowerup.name = powerupType;
            newPowerup.tag = "Powerup";
            newPowerup.transform.rotation = new Quaternion(-90, 0, 0, 0);

            var textMesh = newPowerup.GetComponent<TextMesh>();
            textMesh.text = powerupType;

            float powerupScale = Random.Range(0.3f * playerScale, 3f * playerScale);
            Animate.Scale(newPowerup, new Vector3(powerupScale, powerupScale, powerupScale));
        }
    }

    void CreateRock()
    {
        var playerScale = player.transform.localScale.x;
        var rocks = GameObject.FindGameObjectsWithTag("Rock");
        var sizeDeviation = 3f;

        // Destroy the ones out of range
        foreach (var r in rocks)
        {
            if (Vector3.Distance(player.transform.localPosition, r.transform.localPosition) > GameGridRadius || Vector3.Distance(player.transform.localScale, r.transform.localScale) > GameMaxScaleDifference)
            {
                Destroy(r);
            }
        }

        // If less rocks than maxRocks, set destroy on the smallest
        if (rocks.GetLength(0) >= maxRocks)
        {
            rocks
                .Select(rock => rock.GetComponent<RockScript>())
                .Where(rockScript => rockScript != null)
                .Where(rockScript => !rockScript.isDestroying)
                .Select(rockScript => rockScript.gameObject)
                .OrderBy(rock => rock.transform.localScale.x)
                .First()
                .GetComponent<RockScript>()
                .Destroy();
        }

        // Create a rock
        if (rocks.GetLength(0) < maxRocks)
        {
            var newRock = Instantiate(rock, GetRandomInGameGridRadius(), Quaternion.identity);
            newRock.tag = "Rock";
            newRock.name = "Rock";

            float rockScale = Random.Range(1f / sizeDeviation * playerScale, sizeDeviation * playerScale);
            Animate.Scale(newRock, new Vector3(rockScale, rockScale, rockScale));
        }
    }

    void CreateBomb()
    {
        var bombs = GameObject.FindGameObjectsWithTag("Bomb");

        // Destroy the ones out of range
        foreach (var b in bombs)
        {
            if (Vector3.Distance(player.transform.localPosition, b.transform.localPosition) > GameGridRadius || Vector3.Distance(player.transform.localScale, b.transform.localScale) > GameMaxScaleDifference)
            {
                Destroy(b);
            }
        }

        if (bombs.GetLength(0) < maxBombs)
        {
            var playerScale = player.transform.localScale.x;
            // var newBomb = Instantiate(bomb, new Vector3(Random.Range(-3*playerScale, 3*playerScale), 1, Random.Range(-3*playerScale, 3*playerScale)), Quaternion.identity);
            var newBomb = Instantiate(bomb, GetRandomInGameGridRadius(), Quaternion.identity);
            newBomb.tag = "Bomb";
            newBomb.name = "Bomb";

            float bombScale = Random.Range(0.3f * playerScale, 1.5f * playerScale);
            Animate.Scale(newBomb, new Vector3(bombScale, bombScale, bombScale));
        }
    }
}

