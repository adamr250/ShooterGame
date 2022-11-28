using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public class Checkpoint
    {
        public Vector3 position;
        public Quaternion rotation;

        public Checkpoint(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }

    public List<Checkpoint> checkpointList = new List<Checkpoint>();

    private void FixedUpdate()
    {
        addCheckpoint();
    }

    public void addCheckpoint()
    {
        checkpointList.Add(new Checkpoint(transform.position, transform.rotation));
    }

    public void clearCheckpointList()
    {
        checkpointList.Clear();
        checkpointList.Add(new Checkpoint(transform.position, transform.rotation));
    }
}
