using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtenerObject : MonoBehaviour
{
	public GameObject numero;
	public GameObject auxiliar;
	public GameObject puerta;
	public int score = 0;
	
	void Update(){
		if(numero != null && numero.GetComponent<ValidarObjects>().isPickable == true){
			if(numero.name == auxiliar.name){
				score=1;
				if(numero.name=="5"){
					puerta.SetActive(false);
				}
			}
		}
	}
}
