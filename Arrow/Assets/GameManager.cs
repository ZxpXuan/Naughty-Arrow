using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public List<Birds> bird;
	public List <Pigs> pig;
	public static GameManager _instance;
	private Vector3 originPos;//初始化位置
    public bool Fire_State;
    public bool Ice_State;
    public bool Thunder_State;
    private void Awake(){
		_instance = this;

        var ps = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var p in ps)
        {
            pig.Add(p.GetComponent<Pigs>());
        }

        var bs = GameObject.FindGameObjectsWithTag("Player");
        foreach (var b in bs)
        {
            bird.Add(b.GetComponent<Birds>());
        }

        if (bird.Count > 0) {
			originPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		}
	}

	private void Start(){
		//Initialized ();
	}
	private void Initialized(){
		for (int i = 0; i < bird.Count; i++) {
			if (i == 0) {//第一只bird
				bird [i].transform.position = originPos;
				bird [i].enabled = true;
				bird [i].sp.enabled = true;
			} else {
				bird [i].enabled = false;
				bird [i].sp.enabled = false;
			}
		}
	}
	///<summary>
	/// 判定you xi luo ji.
	/// </summary>
	public void NextBird(){
		if (pig.Count > 0) {
			if (bird.Count > 0) {
				//next bird
				Initialized();
			} else {
				//lose
			}
		} else {
			//win
		}
	}
}
