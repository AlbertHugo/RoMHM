using UnityEngine;
using System.Collections;

public class BossAttacks : MonoBehaviour
{

    //Kleber
    public void ShootArc(GameObject projectilePrefab, Transform firePoint, int projectiles)
    {
        int projectileCount = projectiles;
        float startAngle = -30f;
        float endAngle = 30f;

        for (int i = 0; i < projectileCount; i++)
        {
            float t = (float)i / (projectileCount - 1);
            float angle = Mathf.Lerp(startAngle, endAngle, t);

            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, -Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            BossProjectile bossProj = proj.GetComponent<BossProjectile>();
            if (bossProj != null)
            {
                bossProj.direction = direction.normalized;
            }
        }
    }
    public void MinionSpawn(GameObject kamikaze, Vector3 self, Transform player)
    {
        GameObject enemy = Instantiate(kamikaze, self, Quaternion.identity);

        KamikazeEnemy script = enemy.GetComponent<KamikazeEnemy>();
        script.enabled = false;

        script.player = player;
        script.playerScript = player.GetComponent<PlayerCode>();

        script.enabled = true;
    }
    public void StartSideColumnAttack(GameObject projectilePrefab, float zStart, float zEnd, int countPerColumn, float leftX, float rightX, int linesPerColumn, float columnWidth, float delayBetweenLines, MonoBehaviour caller)
    {
        caller.StartCoroutine(FireSideColumnsCoroutine(projectilePrefab, zStart, zEnd, countPerColumn, leftX, rightX, linesPerColumn, columnWidth, delayBetweenLines));
    }

    private IEnumerator FireSideColumnsCoroutine(GameObject projectilePrefab, float zStart, float zEnd, int countPerColumn, float leftX, float rightX, int linesPerColumn, float columnWidth, float delay)
    {
        float spacing = (zEnd - zStart) / (countPerColumn - 1);
        float xOffsetSpacing = columnWidth / (linesPerColumn - 1);

        for (int i = 0; i < countPerColumn; i++)
        {
            float z = zStart + spacing * i;

            for (int j = 0; j < linesPerColumn; j++)
            {
                float xOffset = j * xOffsetSpacing - columnWidth / 2;

                Vector3 leftPos = new Vector3(leftX + xOffset, 5f, z);
                Vector3 rightPos = new Vector3(rightX + xOffset, 5f, z);
                if (GlobalVariables.phaseCounter == 6 || zEnd == 10)
                {
                    CreateSideSpore(projectilePrefab, leftPos, Vector3.forward);
                    CreateSideSpore(projectilePrefab, rightPos, Vector3.forward);
                }
                else if (zEnd == 9 && GlobalVariables.phaseCounter == 9)
                {
                    CreateSideProjectile(projectilePrefab, leftPos, Vector3.forward);
                    CreateSideProjectile(projectilePrefab, rightPos, Vector3.forward);
                }
                else if (GlobalVariables.phaseCounter == 9)
                {
                    CreateSideTrap(projectilePrefab, leftPos, Vector3.forward);
                    CreateSideTrap(projectilePrefab, rightPos, Vector3.forward);
                }
                CreateSideProjectile(projectilePrefab, leftPos, Vector3.forward);
                CreateSideProjectile(projectilePrefab, rightPos, Vector3.forward);
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void CreateSideProjectile(GameObject prefab, Vector3 position, Vector3 direction)
    {
        GameObject proj = GameObject.Instantiate(prefab, position, Quaternion.LookRotation(direction));
        BossProjectile bp = proj.GetComponent<BossProjectile>();
        if (bp != null)
        {
            bp.direction = direction.normalized;
        }
    }

    public void KamikazeSpawn(GameObject kamikaze, Vector3 self, Transform player)
    {
        GameObject enemy = Instantiate(kamikaze, self, Quaternion.identity);

        Kamikaze3 script = enemy.GetComponent<Kamikaze3>();
        script.enabled = false;

        script.player = player;
        script.playerScript = player.GetComponent<Player3Code>();

        script.enabled = true;
    }

    //Garance

    public void ShootRadial(GameObject projectilePrefab, Transform firePoint, int projectileCount, float speed)
    {
        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            ProjectileUnmovable projScript = proj.GetComponent<ProjectileUnmovable>();
            if (projScript != null)
            {
                projScript.direction = direction.normalized;
                projScript.SetSpeed(speed);
            }
        }
    }
    public void ShootSporeBurst(GameObject projectilePrefab, Transform firePoint, int projectileCount, float speed, float angleVariance)
    {
        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep + Random.Range(-angleVariance, angleVariance);
            float radians = angle * Mathf.Deg2Rad;
            float speedRange = Random.Range(speed / 2, speed);
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            BossProjectile projScript = proj.GetComponent<BossProjectile>();
            if (projScript != null)
            {
                projScript.direction = direction.normalized;
                projScript.SetSpeed(speedRange);
            }
        }
    }
    public void SporeBlast(GameObject projectilePrefab, Transform firePoint, int projectileCount, float speed, float delay, float angleVariance = 20f)
    {
        StartCoroutine(SporeBlastCoroutine(projectilePrefab, firePoint, projectileCount, speed, delay, angleVariance));
    }


    private IEnumerator SporeBlastCoroutine(GameObject projectilePrefab, Transform firePoint, int projectileCount, float speed, float delay, float angleVariance)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = Random.Range(0f, 360f) + Random.Range(-angleVariance, angleVariance);
            float radians = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            ProjectileUnmovable projScript = proj.GetComponent<ProjectileUnmovable>();
            if (projScript != null)
            {
                projScript.direction = direction.normalized;
                projScript.SetSpeed(speed);
            }

            yield return new WaitForSeconds(delay);
        }
    }
    public void ShootCrossBarAttack(GameObject projectilePrefab, Transform firePoint, float speed, float curveDelay, float curveStrength, int projectilesPerColumn, float spacing)
    {
        for (int i = 0; i < projectilesPerColumn; i++)
        {
            float verticalOffset = i * spacing;


            Vector3 leftSpawn = firePoint.position + firePoint.right * -0.5f + firePoint.forward * verticalOffset;
            GameObject leftProj = Instantiate(projectilePrefab, leftSpawn, Quaternion.identity);
            SetCrossBar(leftProj, Vector3.forward, -1, speed, curveDelay, curveStrength);


            Vector3 rightSpawn = firePoint.position + firePoint.right * 0.5f + firePoint.forward * verticalOffset;
            GameObject rightProj = Instantiate(projectilePrefab, rightSpawn, Quaternion.identity);
            SetCrossBar(rightProj, Vector3.forward, 1, speed, curveDelay, curveStrength);
        }
    }

    private void SetCrossBar(GameObject proj, Vector3 initialDir, int curveDirection, float speed, float delay, float curveStrength)
    {
        ProjectileUnmovable script = proj.GetComponent<ProjectileUnmovable>();
        if (script != null)
        {
            script.SetSpeed(speed);
            script.direction = initialDir.normalized;
            script.EnableDelayedCurve(curveDirection, delay, curveStrength);
        }
    }
    public void SniperSpawn(int numberOfEnemies, Vector3 startSpawnPosition, float horizontalSpacing, GameObject movableEnemyPrefab, float spawnInterval)
    {
        StartCoroutine(SpawnSniper(numberOfEnemies, startSpawnPosition, horizontalSpacing, movableEnemyPrefab, spawnInterval));
    }
    public IEnumerator SpawnSniper(int numberOfEnemies, Vector3 startSpawnPosition, float horizontalSpacing, GameObject movableEnemyPrefab, float spawnInterval)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = startSpawnPosition + new Vector3(horizontalSpacing * i, 0f, 0f);

            GameObject enemy = Instantiate(movableEnemyPrefab, spawnPosition, Quaternion.identity);
            MovableEnemy script = enemy.GetComponent<MovableEnemy>();
            script.enemy = enemy.transform;

            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
                Collider[] wallColliders = new Collider[walls.Length];
                for (int j = 0; j < walls.Length; j++)
                {
                    wallColliders[j] = walls[j].GetComponent<Collider>();
                    if (wallColliders[j] != null)
                    {
                        Physics.IgnoreCollision(enemyCollider, wallColliders[j], true);
                    }
                }
                RestoreWallCollision restore = enemy.AddComponent<RestoreWallCollision>();
                restore.Init(enemyCollider, wallColliders, 0.5f);
            }

            MovableEnemy movableEnemy = enemy.GetComponent<MovableEnemy>();
            movableEnemy.enemy = enemy.transform;
            movableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 4f));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void CreateSideSpore(GameObject prefab, Vector3 position, Vector3 direction)
    {
        GameObject proj = GameObject.Instantiate(prefab, position, Quaternion.LookRotation(direction));
        ProjectileUnmovable bp = proj.GetComponent<ProjectileUnmovable>();
        if (bp != null)
        {
            bp.direction = direction.normalized;
        }
    }

    //Bait
    public void ShootFastTrap(GameObject trapProjectilePrefab, Transform firePoint, float speed, int projectileCount, float fireRate)
    {
        StartCoroutine(ShootTrapFast(trapProjectilePrefab, firePoint, speed, projectileCount, fireRate));
    }

    private IEnumerator ShootTrapFast(GameObject trapProjectilePrefab, Transform firePoint, float speed, int projectileCount, float fireRate)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject trap = Instantiate(trapProjectilePrefab, firePoint.position, firePoint.rotation);

            BossProjectile trapScript = trap.GetComponent<BossProjectile>();
            if (trapScript != null)
            {
                trapScript.direction = firePoint.forward.normalized;
                trapScript.SetSpeed(speed);
            }

            yield return new WaitForSeconds(fireRate);
        }
    }
    public void ShootFocusTrap(GameObject trapProjectilePrefab, Transform firePoint, float speed, float trapDelay, int projectileCount, float fireRate)
    {
        StartCoroutine(ShootTrapFocused(trapProjectilePrefab, firePoint, speed, trapDelay, projectileCount, fireRate));
    }

    private IEnumerator ShootTrapFocused(GameObject trapProjectilePrefab, Transform firePoint, float speed, float trapDelay, int projectileCount, float fireRate)
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject trap = Instantiate(trapProjectilePrefab, firePoint.position, firePoint.rotation);

            TrapProjectile trapScript = trap.GetComponent<TrapProjectile>();
            if (trapScript != null)
            {
                trapScript.direction = firePoint.forward.normalized;
                trapScript.SetSpeed(speed);
                trapScript.Invoke("ActivateTrap", trapDelay);
            }

            yield return new WaitForSeconds(fireRate);
        }
    }
    public void ShootRepelBurst(GameObject projectilePrefab, Transform firePoint, int projectileCount, float speed, float angleVariance, float repelRadius, float repelForce)
    {
        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep + Random.Range(-angleVariance, angleVariance);
            float radians = angle * Mathf.Deg2Rad;
            float speedRange = Random.Range(speed / 2f, speed);
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            ProjectileUnmovable projScript = proj.GetComponent<ProjectileUnmovable>();
            if (projScript != null)
            {
                projScript.direction = direction.normalized;
                projScript.SetSpeed(speedRange);
            }

            int bulletLayer = LayerMask.NameToLayer("bullet");
            Collider[] bullets = Physics.OverlapSphere(firePoint.position, repelRadius, 1 << bulletLayer);
            foreach (Collider bullet in bullets)
            {
                Rigidbody rb = bullet.attachedRigidbody;
                if (rb != null)
                {
                    Vector3 repelDir = (bullet.transform.position - firePoint.position).normalized;
                    rb.AddForce(repelDir * repelForce, ForceMode.Impulse);
                }
            }
        }
    }
    public void ShootCharged(GameObject projectilePrefab, Transform firePoint, int projectiles, float trapDelay, float speed)
    {
        int projectileCount = projectiles;
        float startAngle = -30f;
        float endAngle = 30f;

        for (int i = 0; i < projectileCount; i++)
        {
            float t = (float)i / (projectileCount - 1);
            float angle = Mathf.Lerp(startAngle, endAngle, t);

            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, -Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            TrapProjectile bossProj = proj.GetComponent<TrapProjectile>();

            bossProj.Invoke("ActivateTrap", trapDelay);
            bossProj.SetSpeed(speed);
            if (bossProj != null)
            {
                bossProj.direction = direction.normalized;
            }
        }
    }
    //Otello
    private void CreateSideTrap(GameObject prefab, Vector3 position, Vector3 direction)
    {
        GameObject proj = GameObject.Instantiate(prefab, position, Quaternion.LookRotation(direction));
        TrapProjectile bp = proj.GetComponent<TrapProjectile>();
        if (bp != null)
        {
            bp.direction = direction.normalized;
        }
    }
    public void SideColumnAttack(GameObject projectilePrefab, float zStart, float zEnd, int countPerColumn, float leftX, float rightX, int linesPerColumn, float columnWidth, float delayBetweenLines, MonoBehaviour caller)
    {
        caller.StartCoroutine(FireSideCoroutine(projectilePrefab, zStart, zEnd, countPerColumn, leftX, rightX, linesPerColumn, columnWidth, delayBetweenLines));
    }
        private IEnumerator FireSideCoroutine(GameObject projectilePrefab, float zStart, float zEnd, int countPerColumn, float leftX, float rightX, int linesPerColumn, float columnWidth, float delay)
    {
        float spacing = (zEnd - zStart) / (countPerColumn - 1);
        float xOffsetSpacing = columnWidth / (linesPerColumn - 1);

        for (int i = 0; i < countPerColumn; i++)
        {
            float z = zStart + spacing * i;

            for (int j = 0; j < linesPerColumn; j++)
            {
                float xOffset = j * xOffsetSpacing - columnWidth / 2;

                Vector3 leftPos = new Vector3(leftX + xOffset, 5f, z);
                Vector3 rightPos = new Vector3(rightX + xOffset, 5f, z);
                CreateExplosiveTrap(projectilePrefab, leftPos, Vector3.forward);
                CreateExplosiveTrap(projectilePrefab, rightPos, Vector3.forward);
            }

            yield return new WaitForSeconds(delay);
        }
    }
    private void CreateExplosiveTrap(GameObject prefab, Vector3 position, Vector3 direction)
    {
        GameObject proj = GameObject.Instantiate(prefab, position, Quaternion.LookRotation(direction));
        TrapProjectile bp = proj.GetComponent<TrapProjectile>();
        bp.Invoke("ActivateTrap", 0.5f);
        if (bp != null)
        {
            bp.direction = direction.normalized;
        }
    }

    public void ShootNormalRadial(GameObject projectilePrefab, Transform firePoint, int projectileCount, float speed)
    {
        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            BossProjectile projScript = proj.GetComponent<BossProjectile>();
            if (projScript != null)
            {
                projScript.direction = direction.normalized;
                projScript.SetSpeed(speed);
            }
        }
    }

    public void ShootTrapRadial(GameObject projectilePrefab, Transform firePoint, int projectileCount, float speed)
    {
        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            TrapProjectile projScript = proj.GetComponent<TrapProjectile>();
            projScript.Invoke("ActivateTrap", 0.5f);
            if (projScript != null)
            {
                projScript.direction = direction.normalized;
                projScript.SetSpeed(speed);
            }
        }
    }
    public void ShootUnmovableArc(GameObject projectilePrefab, Transform firePoint, int projectiles)
    {
        int projectileCount = projectiles;
        float startAngle = -30f;
        float endAngle = 30f;

        for (int i = 0; i < projectileCount; i++)
        {
            float t = (float)i / (projectileCount - 1);
            float angle = Mathf.Lerp(startAngle, endAngle, t);

            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, -Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            ProjectileUnmovable bossProj = proj.GetComponent<ProjectileUnmovable>();
            if (bossProj != null)
            {
                bossProj.direction = direction.normalized;
            }
        }
    }
    public void ShootTrapArc(GameObject projectilePrefab, Transform firePoint, int projectiles)
    {
        int projectileCount = projectiles;
        float startAngle = -30f;
        float endAngle = 30f;

        for (int i = 0; i < projectileCount; i++)
        {
            float t = (float)i / (projectileCount - 1);
            float angle = Mathf.Lerp(startAngle, endAngle, t);

            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, -Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            TrapProjectile bossProj = proj.GetComponent<TrapProjectile>();
            bossProj.Invoke("ActivateTrap", 0.3f);
            if (bossProj != null)
            {
                bossProj.direction = direction.normalized;
            }
        }
    }
    public void ApplyPlayerDebuff(Transform player, float duration, float reducedSpeed, Renderer bossRenderer, Color debuffColor)
    {
        MonoBehaviour mono = player.GetComponent<MonoBehaviour>();
        if (mono != null)
        {
            mono.StartCoroutine(DebuffCoroutine(player, duration, reducedSpeed, bossRenderer, debuffColor));
        }
    }

    private IEnumerator DebuffCoroutine(Transform player, float duration, float reducedSpeed, Renderer bossRenderer, Color debuffColor)
    {
        Player3Code playerScript = player.GetComponent<Player3Code>();
        if (playerScript == null) yield break;

        float originalSpeed = playerScript.baseSpeed;
        float originalCurrentSpeed = playerScript.speed;

        Renderer renderer = player.GetComponent<Renderer>();
        Color originalColor = Color.white;

        playerScript.baseSpeed = reducedSpeed;
        playerScript.speed = reducedSpeed;

        debuffColor *= 5;
        renderer.material.color = debuffColor * 2;
        bossRenderer.material.color = debuffColor;
        playerScript.playerRenderer.material.color = debuffColor;
        bossRenderer.material.SetColor("_Color", debuffColor * 10f);

        yield return new WaitForSeconds(duration);
        playerScript.baseSpeed = originalSpeed;
        playerScript.speed = originalCurrentSpeed;
        renderer.material.color = originalColor;
        bossRenderer.material.color = originalColor;
        bossRenderer.material.SetColor("_Color", originalColor);
    }

    public void SpeedControl(Transform player, float duration, float reducedSpeed, Renderer bossRenderer, Color debuffColor)
    {
        MonoBehaviour mono = player.GetComponent<MonoBehaviour>();
        if (mono != null)
        {
            mono.StartCoroutine(ControlCoroutine(player, duration, reducedSpeed, bossRenderer, debuffColor));
        }
    }
    private IEnumerator ControlCoroutine(Transform player, float duration, float amplifiedSpeed, Renderer bossRenderer, Color debuffColor)
    {
        Player3Code playerScript = player.GetComponent<Player3Code>();
        if (playerScript == null) yield break;

        float originalSpeed = playerScript.baseSpeed;
        float originalCurrentSpeed = playerScript.speed;

        Renderer renderer = player.GetComponent<Renderer>();
        Color originalColor = Color.white;

        playerScript.baseSpeed = amplifiedSpeed;
        playerScript.speed = amplifiedSpeed;

        renderer.material.color = debuffColor*2;
        bossRenderer.material.color = debuffColor;
        playerScript.playerRenderer.material.color = debuffColor;
        bossRenderer.material.SetColor("_EmissionColor", debuffColor * 10f);

        yield return new WaitForSeconds(duration);
        playerScript.baseSpeed = originalSpeed;
        playerScript.speed = originalCurrentSpeed;
        renderer.material.color = originalColor;
        bossRenderer.material.color = originalColor;
        bossRenderer.material.SetColor("_EmissionColor", originalColor);
    }
}