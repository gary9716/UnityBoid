using UnityEngine;

public class LandingSpotController : MonoBehaviour, Landable {

	public BoidFlocking boid;

	public void TargetBy(BoidFlocking boid) {
		boid.SetTarget(transform);
		this.boid = boid;
	}
	public void Release(BoidFlocking boid){
		Debug.Assert(boid == this.boid);
		boid.landingPt = null;
		this.boid = null;
	}
	public void ReleaseAll(){
		Release(boid);
	}

	public bool isLandable() {
		return boid == null;
	}

	public Transform getTrans() {
		return transform;
	}
}
