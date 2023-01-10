using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyManager : MonoBehaviour
{
    private float bodyPartsDistance = 0.225f;

    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> bossBody = new List<GameObject>();

    [SerializeField] GameObject weaponObject;
    List<GameObject> weaponList = new List<GameObject>();

    private float bodyPartSpawnDelay = 0;

    public static int bodyPartsDestroyedCounter = 0;
    public static bool bossBodyCompleted = false;

    void Start()
    {
        //countUp = bodyPartsDistance;
        createBody();
    }

    void FixedUpdate()
    {
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
        /*bossBody[0].GetComponent<Rigidbody2D>().velocity = bossBody[0].transform.right * speed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") != 0)
        {
            bossBody[0].transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));
            Debug.Log(Input.GetAxis("Horizontal"));
        }*/

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
        if(bossBody.Count == 0)
        {
            GameObject tmp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            if (!tmp1.GetComponent<CheckpointManager>())
                tmp1.AddComponent<CheckpointManager>();
            bossBody.Add(tmp1);
            bodyParts.RemoveAt(0);
        }

        CheckpointManager checkpoint = bossBody[bossBody.Count - 1].GetComponent<CheckpointManager>();

        if(bodyPartSpawnDelay == 0)
        {
            checkpoint.clearCheckpointList();
        }

        bodyPartSpawnDelay += Time.deltaTime;

        if(bodyPartSpawnDelay >= bodyPartsDistance)
        {
            GameObject tmp2 = Instantiate(bodyParts[0], checkpoint.checkpointList[0].position, checkpoint.checkpointList[0].rotation, transform);
            GameObject weapon = Instantiate(weaponObject, checkpoint.checkpointList[0].position, checkpoint.checkpointList[0].rotation, transform);

            if (!tmp2.GetComponent<CheckpointManager>())
                tmp2.AddComponent<CheckpointManager>();

            bossBody.Add(tmp2);
            bodyParts.RemoveAt(0);
            tmp2.GetComponent<CheckpointManager>().clearCheckpointList();
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
}
