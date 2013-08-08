using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public List<EnemyClass> Enemys = new List<EnemyClass>();

	
	void Start () {
		Enemys.Add(new EnemyClass());
	}

	void Update () {
		Enemys[0].EnemyUpdate();
	}
}