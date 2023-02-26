using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBodyManager : MonoBehaviour
{
    SpawnEnemies spawnEnemies;
    private float bodyPartsDistance = 0.235f;

    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> bossBody = new List<GameObject>();

    [SerializeField] GameObject weaponObject;
    List<GameObject> weaponList = new List<GameObject>();

    private float bodyPartSpawnDelay = 0;

    public static int bodyPartsDestroyedCounter = 0;
    public static bool bossBodyCompleted = false;

    public static int bodyPartsCount = 0;

    void Start()
    {
        spawnEnemies = GameObject.Find("GameCore").GetComponent<SpawnEnemies>();
        //countUp = bodyPartsDistance;
        createBody();
    }

    void FixedUpdate()
    {
        bodyPartsCount = bossBody.Count;

        if(bodyParts.Count > 0)
        {
            createBody();
        } else
        {
            bossBodyCompleted = true;
        }

        bodyMovement();   
    }

    void bodyMovement()
    {
        if(bossBody.Count > 1)
        {
            for(int i = 1; i < bossBody.Count; i++)
            {
                CheckpointManager checkpointBody = bossBody[i - 1].GetComponent<CheckpointManager>(); //bierzemy liste pozycji fragmentu ciala o inteksie i-1 (przed nami)
                bossBody[i].transform.position = checkpointBody.checkpointList[0].position; //aktualizujemy pozycjê fragmenu ciala bossa o indeksie i
                bossBody[i].transform.rotation = checkpointBody.checkpointList[0].rotation; //

                weaponList[i-1].transform.position = checkpointBody.checkpointList[0].position;

                checkpointBody.checkpointList.RemoveAt(0); //usuwamy pierwsz¹ pozycje z listy cia³ i-1 (przed nami)
            }
        }
    }

    void createBody()
    {
        //tworzy glowe
        if(bossBody.Count == 0)
        {
            GameObject head = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            if (!head.GetComponent<CheckpointManager>())
                head.AddComponent<CheckpointManager>();
            bossBody.Add(head);
            bodyParts.RemoveAt(0);
        }

        //pobierami liste pozycji ostatniego stworzonego segmentu
        CheckpointManager checkpoint = bossBody[bossBody.Count - 1].GetComponent<CheckpointManager>();

        bodyPartSpawnDelay += Time.deltaTime;

        //tworzy cialo i bronie
        if(bodyPartSpawnDelay >= bodyPartsDistance)
        {
            GameObject body = Instantiate(bodyParts[0], checkpoint.checkpointList[0].position, checkpoint.checkpointList[0].rotation, transform);
            GameObject weapon = Instantiate(weaponObject, checkpoint.checkpointList[0].position, checkpoint.checkpointList[0].rotation, transform);

            if (!body.GetComponent<CheckpointManager>())
                body.AddComponent<CheckpointManager>();

            bossBody.Add(body);
            bodyParts.RemoveAt(0);
            body.GetComponent<CheckpointManager>().clearCheckpointList();
            bodyPartSpawnDelay = 0;

            weaponList.Add(weapon);
        }
    }

    public void destoryBodySegment()
    {
        Destroy(weaponList[weaponList.Count - 1]);
        weaponList.RemoveAt(weaponList.Count - 1);

        Destroy(bossBody[bossBody.Count - 1]);
        bossBody.RemoveAt(bossBody.Count - 1);

        bodyPartsDestroyedCounter++;       
    }


    public void death()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        spawnEnemies.increaseBossScoreThreshold();
    }
}
