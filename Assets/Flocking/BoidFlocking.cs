using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//original source: http://wiki.unity3d.com/index.php?title=Flocking
//fixed by KT on 2017/03/13
//align facing direction with movement
//add coefficient to steer

public class BoidFlocking : MonoBehaviour
{
	internal BoidController controller;
    public Rigidbody rigid;
    public SphereCollider sphereCollider;

    public Animator animator;

    IEnumerator Start()
	{
        WaitForSeconds waitForFixedSecs = new WaitForSeconds(Random.Range(0.05f, 0.3f));

        while (true)
		{
			if (controller)
			{
				rigid.velocity += steer() * Time.deltaTime;

				// enforce minimum and maximum speeds for the boids
				float speed = rigid.velocity.magnitude;
				if (speed > controller.maxVelocity)
				{
                    rigid.velocity = rigid.velocity * (controller.maxVelocity / speed);
				}
				else if (speed < controller.minVelocity)
				{
                    rigid.velocity = rigid.velocity * (controller.minVelocity / speed);
				}

                if (animator)
                    animator.SetFloat("moveSpeed", rigid.velocity.magnitude);

                rigid.transform.rotation = Quaternion.LookRotation(rigid.velocity);
            }
            else
            {
                this.enabled = false;
            }

			yield return waitForFixedSecs;
		}
	}
    
    Vector3 steer()
    {
        //separation is dealed with sphere collider and physics material
        Vector3 cohesion = Vector3.ClampMagnitude(controller.flockCenter - rigid.transform.localPosition, controller.maxVelocity) * controller.cohesionFactor;
        Vector3 alignment = Vector3.ClampMagnitude(controller.flockVelocity, controller.maxVelocity) * controller.alignFactor;
        Vector3 follow = Vector3.ClampMagnitude(controller.target.localPosition - rigid.transform.localPosition, controller.maxVelocity) * controller.attractFactor;

        Vector3 randomize = Vector3.ClampMagnitude(new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1), controller.maxVelocity) * controller.randomness;

        return (cohesion + alignment + follow + randomize);

    }

}