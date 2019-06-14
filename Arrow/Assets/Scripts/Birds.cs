using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Birds : MonoBehaviour {

	private bool InClick = false;
	public float maxDis = 3;
	[HideInInspector]
	public SpringJoint2D sp;
	private Rigidbody2D rg;
	private LineRenderer lr;

	public LineRenderer right;
	public Transform rightPos;
	public LineRenderer left;
	public Transform leftPos;
	public GameObject boom;


    public float Time_Max = 100;
	public float Time_Pers = 1;
	public int Arrow_Energy_Max= 100;
	public int Arrow_Power_decrease_Pers=1;


	public Text Energy;

	private void Awake(){
		
		sp = GetComponent<SpringJoint2D>();
		rg = GetComponent<Rigidbody2D> ();
		lr = GetComponent<LineRenderer> ();
	}

	void Start(){
		Energy.text = string.Format ("Enengy:", (int)Arrow_Energy_Max);
	}

	private void OnMouseDown(){

		InClick = true;
		rg.isKinematic = true;
		sp.enabled = true;

		
	}
	private void OnMouseUp(){

		InClick = false;
		rg.isKinematic = false;
		Invoke ("Fly", 0.1f);

		
	}

	void Update () {
		if (Time_Max > 0) {
			Time_Pers -= Time.deltaTime;
			if (Time_Pers <= 0) {
				Time_Pers += 1;
				Time_Max = Time_Max - Time_Pers;
				Energy.text = string.Format("{0:D2}:{1:D2}",(int)Time_Max/60,(int)Time_Max%60);
				Arrow_Energy_Max = Arrow_Energy_Max - Arrow_Power_decrease_Pers;
			}
		}
		if (InClick) {
			transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position += new Vector3 (0, 0, 10);
			if (Vector3.Distance (transform.position, rightPos.position) > maxDis) {
				Vector3 pos = (transform.position - rightPos.position).normalized;//huo得dan位化向量，大写的Normalized会改bian zhi。
				pos *= maxDis;//最大chang度的向量
				transform.position = pos + rightPos.position;	
			}//ji算两个点的距离
			Line ();//划xian

		}
		if (Arrow_Energy_Max <= 0) {
			Next ();
		}

	}

	public void Fly(){
		sp.enabled = false;
		Invoke ("Next", 5f);
	}
	/// <summary>
	/// 划xian
	/// </summary>
	void Line(){
		right.SetPosition (0, rightPos.position);
		right.SetPosition (1, transform.position);

		left.SetPosition (0, leftPos.position);
		left.SetPosition (1, transform.position);
	}
	/// <summary>
	/// 下只小niao fei出
	/// </summary>
	void Next(){
		
		GameManager._instance.bird.Remove (this);
		Destroy(gameObject);
		Instantiate (boom, transform.position, Quaternion.identity);
		GameManager._instance.NextBird ();

	}


}
