using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BombManager : MonoBehaviour
{
    public List<Bomb> bombs;
    public GameObject blastWave;

    protected AudioSource audioSource;

    private List<GameObject> waves;
    private float wavesTTL = 0.3f;
    private GameManager gameManager;
    private bool startCleaning = false;

    void Update()
    {
        UpdateBombTTL();
        RemoveBlastWaves();
    }

    protected virtual void RemoveBlastWaves()
    {
        if (waves != null)
        {
            wavesTTL -= Time.deltaTime;
            if (wavesTTL <= 0)
            {
                for (int i = 0; i < waves.Count; i++)
                {
                    Destroy(waves[i]);
                }

                waves = null;
                wavesTTL = 0.3f;
                startCleaning = true;
            }
        }

        if (startCleaning)
        {
            DestroyObjectsByTag("Wave");
            DestroyObjectsByTag("Bomb");
            startCleaning = false;
        }
    }

    protected virtual void DestroyObjectsByTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (gameObjects.Length > 0)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                DestroyImmediate(gameObjects[i]);
            }
        }
    }
    protected virtual void UpdateBombTTL()
    {

        for (int i = 0; i < bombs.Count; i++)
        {
            Bomb bomb = bombs[i];
            bomb.timeToExplode -= Time.deltaTime;

            if (bomb.timeToExplode <= 0.2f && bomb.timeToExplode > 0)
            {
                bomb.BombPrefab.GetComponent<AudioSource>().Play();
            }

            if (bomb.timeToExplode <= 0)
            {
                Explode(bomb);
            }
        }
    }

    public void Awake()
    {
        bombs = new List<Bomb>();
        audioSource = new BaseObjectCreator().GetBomb().GetComponent<AudioSource>();
    }

    public virtual void ExplodeAllBombs()
    {
        for (int i = 0; i < bombs.Count; i++)
        {
            Explode(bombs[i]);
        }
    }

    public virtual void Explode(Bomb bomb)
    {
        audioSource.Play();
        bomb.BombPrefab.GetComponent<AudioSource>().Play();
        CreateBlastWave(bomb);
        bombs.Remove(bomb);

    }

    protected virtual void CreateBlastWave(Bomb bomb)
    {
        Transform transform = bomb.BombPrefab.transform;
        waves = new List<GameObject>();
        waves.Add(Instantiate(blastWave, transform.position, Quaternion.identity));
        MakeWaveGo(bomb, Vector3.forward);
        MakeWaveGo(bomb, Vector3.back);
        MakeWaveGo(bomb, Vector3.right);
        MakeWaveGo(bomb, Vector3.left);
    }

    protected virtual void MakeWaveGo(Bomb bomb, Vector3 direction)
    {
        int radius = bomb.explosionRadius;
        Vector3 position = bomb.BombPrefab.transform.position;
        for (int i = 0; i < radius; i++)
        {
            position += direction;
            waves.Add(Instantiate(blastWave, position, Quaternion.identity));
        }
    }

    public void DropNewBomb(Vector3 position, int radius)
    {
        Bomb bomb = new Bomb(new BaseObjectCreator().GetBomb());
        position.x = (float)Math.Round(position.x);
        position.z = (float)Math.Round(position.z);
        bomb.BombPrefab.transform.position = position;
        bomb.BombPrefab.SetActive(true);
        bomb.explosionRadius = radius;
        bomb.BombPrefab = Instantiate(bomb.BombPrefab);
        bombs.Add(bomb);
    }
}


