using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Arrow : MonoBehaviour {

	private bool InClick = false;
	public float maxDis = 3;
	[HideInInspector]
	public SpringJoint2D sp;
	private Rigidbody2D rg;
	public GameObject boom;
	public Transform FlyPos;
	public Transform SelfPos;

	public float Time_Max = 100;
	public float Time_Pers = 1;
	public int Arrow_Energy_Max= 100;
	public int Arrow_Power_decrease_Pers=1;

	public Text Times;

	private void Awake(){
		sp = GetComponent<SpringJoint2D>();
		rg = GetComponent<Rigidbody2D> ();
	}
	private void OnMouseDown(){

		InClick = true;

	}
	private void OnMouseUp(){
		InClick = false;
	}

	// Update is called once per frame
	void Update () {

		if (Time_Max > 0) {
			Time_Pers -= Time.deltaTime;
			if (Time_Pers <= 0) {
				Time_Pers += 1;
				Time_Max = Time_Max - Time_Pers;
				Times.text = string.Format("{0:D2}:{1:D2}",(int)Time_Max/60,(int)Time_Max%60);
				Arrow_Energy_Max = Arrow_Energy_Max - Arrow_Power_decrease_Pers;
			}
		}
		FlyPos.position = SelfPos.position;
		Vector3 c = new Vector3 (FlyPos.position.x, FlyPos.position.y, 0);
		Debug.Log ("The position" + FlyPos.position.x);
		Debug.Log ("The new position" + c.x);
		sp.connectedAnchor = new Vector3(FlyPos.position.x, FlyPos.position.y, 0);
		Debug.Log ("The Anchor position" + sp.connectedAnchor.x);
		if (InClick) {
			transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position += new Vector3 (0, 0, 10);

			if (Vector3.Distance (transform.position, FlyPos.position) > maxDis) {
				Vector3 pos = (transform.position - FlyPos.position).normalized;//huo得dan位化向量，大写的Normalized会改bian zhi。
				pos *= maxDis;//最大chang度的向量
				transform.position = pos + FlyPos.position;	
			}//ji算两个点的距离

		}
	}
}
