using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLandableSpotCtrler : MonoBehaviour,Landable {
	
	public int capacity = -1; //-1 means no limit

	List<BoidFlocking> boids = new List<BoidFlocking>();

	public void TargetBy(BoidFlocking boid) {
		boids.Add(boid);
		boid.SetTarget(transform);
	}
	public void Release(BoidFlocking boid){
		boid.landingPt = null;
		boids.Remove(boid);
	}
	public void ReleaseAll(){
		foreach(BoidFlocking boid in boids) {
			boid.landingPt = null;
		}
		boids.Clear();
	}
	
	public bool isLandable() {
		return (capacity == -1) || (boids.Capacity < capacity);
	}

	public Transform getTrans() {
		return transform;
	}
}
