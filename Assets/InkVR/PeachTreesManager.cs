using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachTreesManager : MonoBehaviour {

	public BoidController boidCtrler;

	PeachTreeLandingPtsCtrler[] peachTrees;
	int minNumLandingPts = int.MaxValue;

	public int numPeachTrees {
		get {
			if(peachTrees != null)
				return peachTrees.Length;
			else
				return 0;
		}
	}


	PeachTreeLandingPtsCtrler chooseTree(bool randomlyChoose = true) {
		if(randomlyChoose)
			return peachTrees[(int)Random.Range(0, peachTrees.Length)];
		else
			return peachTrees[0];
	}

	// Use this for initialization
	void Start () {
		peachTrees = GetComponentsInChildren<PeachTreeLandingPtsCtrler>();
		foreach(PeachTreeLandingPtsCtrler peachTree in peachTrees) {
			//peachTree.putTestingButterfly = true;
			peachTree.GenerateLandingPtsBasedOnBranches();
			peachTree.SetPtsDirBasedOnObserver(Camera.main.transform.position);
			if(minNumLandingPts > peachTree.numPts)
				minNumLandingPts = peachTree.numPts;
		}
		
		if(numPeachTrees > 0 && boidCtrler != null) {
			PeachTreeLandingPtsCtrler peachTree = chooseTree(false);
			if(peachTree != null)
				boidCtrler.PutBoidsOnTree(peachTree);
		}

		Invoke("FlyToSecondTree", 5);		

	}
	
	void FlyToSecondTree() {
		if(numPeachTrees > 1) {
			boidCtrler.FlyToTree(peachTrees[1]);
		}

	}


}
