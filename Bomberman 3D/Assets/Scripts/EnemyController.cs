using Assets.Scripts.Entities.Characters.BadBoys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterControllerBase
{

    private int xDir = 0;
    private int zDir = 0;
    private Enemy enemy;
    private List<int[]> directions;
    private BombManager bombManager;
    private Vector3 playerPosition;
    private List<Vector3> path;
    private List<Vector3> tempPath;
    private int counter = 0;
    private IIntelligence intelligentSystem;

    protected AudioSource audioSource;
    public AudioClip stepSound;

    protected override void OnStart()
    {
        base.OnStart();
        bombManager = GetComponent<BombManager>();
        if (transform.root.gameObject.name.Contains("Balloom"))
        {
            enemy = new Balloom(transform.root.gameObject);
        }
        else if (transform.root.gameObject.name.Contains("Oneal"))
        {
            enemy = new Oneal(transform.root.gameObject);
        }
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        path = new List<Vector3>();

        if (enemy.intelligence == Smart.Low)
        {
            intelligentSystem = new IntelligentSystemBase(enemy);
        }
        else if (enemy.intelligence == Smart.High)
        {
            intelligentSystem = new AdvancedIntelligentSystem(enemy);
        }
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private float twoSec = 2f;

    void Update()
    {
        if (enemy == null) return;

        Collider collider = enemy.gameObject.GetComponentInChildren<Collider>();

        if (!collider.enabled) return;


        twoSec -= Time.deltaTime;
        if (twoSec <= 0)
        {
            if (lockMove == 0)
            {
                if (path == null)
                {
                    path = intelligentSystem.ChooseRandomPath();
                }
                if (path.Count != 0)
                {
                    FollowYourPath();
                }
                else
                {
                    path = intelligentSystem.CalculatePath();
                }
            }
        }
    }

    protected virtual void FollowYourPath()
    {
        Vector3 moveTo = path[path.Count - 1];
        path.RemoveAt(path.Count - 1);
        Vector3 direction = moveTo - enemy.gameObject.transform.position;
        RaycastHit hit;
        Move((int)direction.x, (int)direction.z, out hit, enemy);
    }

    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(stepSound);
    }
}
