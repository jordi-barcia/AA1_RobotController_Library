using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMe : MonoBehaviour
{


    Rigidbody me;
    // Start is called before the first frame update
    void Start()
    {
        me = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I am having a trigger activated");
        transform.SetParent(other.transform);
        me.useGravity = false;
        me.isKinematic = true;
    }

    public void LeaveMe()
    {
        Debug.Log("I am leaving my parent");
        transform.SetParent(null);
        me.useGravity = true;
        me.isKinematic = false;


    }

    public void Reset()
    {
        Debug.Log("I am going to the initial position");
        transform.position = new Vector3(3.055f, 1.168f, -0.356f);
        transform.rotation = Quaternion.Euler(0, -180, 0);
    }


}
