using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyManager : MonoBehaviour
{
    [SerializeField] float bodyPartsDistance = 1;
    [SerializeField] float speed = 100;
    [SerializeField] float turnSpeed = 100;
    [SerializeField] List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> bossBody = new List<GameObject>();

    [SerializeField] GameObject weaponObject;
    List<GameObject> weaponList = new List<GameObject>();

    private float countUp = 0; 

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
        }
        bossMovement();   
    }

    void bossMovement()
    {
        bossBody[0].GetComponent<Rigidbody2D>().velocity = bossBody[0].transform.right * speed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") != 0)
        {
            bossBody[0].transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal")));
        }

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

        if(countUp == 0)
        {
            checkpoint.clearCheckpointList();
        }

        countUp += Time.deltaTime;

        if(countUp >= bodyPartsDistance)
        {
            GameObject tmp2 = Instantiate(bodyParts[0], checkpoint.checkpointList[0].position, checkpoint.checkpointList[0].rotation, transform);
            GameObject weapon = Instantiate(weaponObject, checkpoint.checkpointList[0].position, checkpoint.checkpointList[0].rotation, transform);

            if (!tmp2.GetComponent<CheckpointManager>())
                tmp2.AddComponent<CheckpointManager>();

            bossBody.Add(tmp2);
            bodyParts.RemoveAt(0);
            tmp2.GetComponent<CheckpointManager>().clearCheckpointList();
            countUp = 0;

            weaponList.Add(weapon);
        }
    }
}
