using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeTracker : MonoBehaviour
{
    public Transform probeTransform; 
    ReflectionProbe probe;

    // Start is called before the first frame update
    void Start()
    {
        probe = probeTransform.GetComponent<ReflectionProbe>();
        probe.refreshMode = UnityEngine.Rendering.ReflectionProbeRefreshMode.EveryFrame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void FixedUpdate()
	{
        Vector3 targetPos = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        probeTransform.position = targetPos;
	}
}
