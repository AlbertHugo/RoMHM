using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using UnityEngine.Audio;

public class RepeatableCode : MonoBehaviour
{
    public static AudioMixerGroup sfxMixerGroup;
    public static void TakeDamage(ref float life, float damage, GameObject obj,
    VisualEffect effect, Vector3 spawnPosition, Quaternion spawnRotation, VisualEffect damageTaken,
    AudioClip enemyDeathSound, AudioClip hitSound)
    {
        life -= damage;

        PlaySound(hitSound, spawnPosition);

        damageTaken.gameObject.SetActive(true);
        VisualEffect vfxDamage = GameObject.Instantiate(damageTaken, spawnPosition, Quaternion.identity);
        vfxDamage.Play();
        GameObject.Destroy(vfxDamage.gameObject, 0.5f);
        damageTaken.gameObject.SetActive(false);

        if (life <= 0)
        {
            GlobalVariables.enemiesAlive = GlobalVariables.enemiesAlive - 1;
            if (GlobalVariables.bossCounter == 50)
            {
                GlobalVariables.bossCounter = 15;
            }
            Die(effect, spawnPosition, spawnRotation);
            PlaySound(enemyDeathSound, spawnPosition);
            GameObject.Destroy(obj);
            GlobalVariables.score += 100;
            GlobalVariables.revengeCount += 1;
            GlobalVariables.waveCounter += 1;
        }
    }

    public static void TakeDamagePlayer(ref float life, float damage, GameObject obj, AudioClip playerHitSound)
    {
        life -= damage;
        PlaySound(playerHitSound, obj.transform.position);

        if (life <= 0)
        {
            GameObject.Destroy(obj);
            if (GlobalVariables.phaseCounter <= 2)
            {
                SceneManager.LoadScene("Defeat");
            }else if(GlobalVariables.phaseCounter==5||GlobalVariables.phaseCounter==6)
            {
                SceneManager.LoadScene("Defeat2");
            }else if(GlobalVariables.phaseCounter==7||GlobalVariables.phaseCounter==8 || GlobalVariables.phaseCounter == 4 || GlobalVariables.phaseCounter == 9)
            {
                SceneManager.LoadScene("Defeat3");
            }
        }
    }
    public static void HandleMovement(ref float speed, ref Rigidbody rb)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (movement == Vector3.zero)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
    public static void HandleFocusMode(ref float speed, ref bool focus, float baseSpeed)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            focus = true;
            speed = baseSpeed / 2;
        }
        else
        {
            focus = false;
            speed = baseSpeed;
        }
    }
    public static void EnemyAttack(GameObject projectile, Vector3 spawnPosition, Quaternion spawnRotation,
        float speed)
    {
        GameObject bullet = GameObject.Instantiate(projectile, spawnPosition, spawnRotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = bullet.transform.forward;
        }
    }
    public static void Die(VisualEffect effect, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        effect.gameObject.SetActive(true);
        VisualEffect vfx = Instantiate(effect, spawnPosition, Quaternion.identity);
        vfx.Play();
        Destroy(vfx.gameObject, 2f);
    }
    public static void PlaySound(AudioClip clip, Vector3 position)
    {
        if (clip == null) return;

        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.outputAudioMixerGroup = sfxMixerGroup;
        aSource.Play();
        GameObject.Destroy(tempGO, clip.length);
    }
}