using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//original source: http://wiki.unity3d.com/index.php?title=Flocking
//fixed by KT on 2017/03/17
//add coefficient

/// <summary>
/// these define the flock's behavior
/// </summary>
public class BoidController : MonoBehaviour
{
	public float minVelocity = 5;
	public float maxVelocity = 20;
	public int flockSize = 120;
	public BoidFlocking prefab;
	public Transform target;
    
    internal Vector3 flockCenter;
	internal Vector3 flockVelocity;

    [Range(-20, 20)]
    public float cohesionFactor = 0.4f;

    [Range(0, 10)]
    public float separationRadius = 3f;

    [Range(0, 20)]
    public float alignFactor = 0.5f;

    [Range(0, 20)]
    public float attractFactor = 1f;

    [Range(0, 5)]
    public float randomness = 1f;

    public float landingDist = 0.3f;
	public float perchingDist = 0.01f;
    List<BoidFlocking> boids = new List<BoidFlocking>();

	void Start()
	{
        
		for (int i = 0; i < flockSize; i++)
		{
			BoidFlocking boid = Instantiate(prefab, transform.position, transform.rotation) as BoidFlocking;
			boid.transform.parent = transform;
			boid.transform.localPosition = new Vector3(
							Random.value * GetComponent<Collider>().bounds.size.x,
							Random.value * GetComponent<Collider>().bounds.size.y,
							Random.value * GetComponent<Collider>().bounds.size.z) - GetComponent<Collider>().bounds.extents;
			boid.controller = this;
			boids.Add(boid);
		}
	}

	public int numFlockingInstances = 0;

	void Update()
	{
		Vector3 center = Vector3.zero;
		Vector3 velocity = Vector3.zero;
		numFlockingInstances = 0;
		foreach (BoidFlocking boid in boids)
		{
			if(boid.curState == BoidFlocking.State.flocking) {
				center += boid.transform.position;
				velocity += boid.rigid.velocity;
				numFlockingInstances++;
			}
		}

		if(numFlockingInstances > 0) {
			flockCenter = center / numFlockingInstances;
			flockVelocity = velocity / numFlockingInstances;
		}

    }

}