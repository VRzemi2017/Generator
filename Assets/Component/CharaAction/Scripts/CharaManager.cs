using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour {

	[SerializeField]
	private GameObject enemy01;
	[SerializeField]
	private GameObject enemy02;
	[SerializeField]
	private GameObject enemy03;
	[SerializeField]
	private GameObject enemy04;
	[SerializeField]
	private GameObject enemy05;
	[SerializeField]
	private GameObject enemy06;
	[SerializeField]
	private GameObject enemy07;


    public void SetEnemyActive(int num, bool Active) {
        //enemy[num].setActive(Active);
    }
}
