using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{	
	public double positionx;
	public double positiony;
	public double positionz;
	public GameObject puerta;
	public int score;

    void Start()
    {
	positionx=0;
	positiony=0;
	positionz=0;
	score = 0;

    }

    void Update()
    {	
	positionx = transform.position.x;
	positiony = transform.position.y;
	positionz = transform.position.z;

	if((positionx < -12.8 && positionx > -14 && positionz > -16.56 && positionz < -14.48) || (positionx < -27.5 && positionx > -29 && positionz >=-16.56 && positionz <=-14.48)){
		score+=1;
		transform.position = new Vector3(-21,1,-15);
	}
	if(score == 3){
		puerta.SetActive(false);
	}
    }
}
