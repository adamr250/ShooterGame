using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBulletEvent : MonoBehaviour
{
    public event EventHandler OnShoot;

    public class OnShootEventArgs : EventArgs {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(OnShoot != null) {
                OnShoot(this, EventArgs.Empty);
            }
        }
    }
}
