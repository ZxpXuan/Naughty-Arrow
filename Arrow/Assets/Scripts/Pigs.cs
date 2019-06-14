using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pigs : MonoBehaviour {
	
	public float maxSpeed = 20;
    public float midSpeed = 10;//不同速度make different hurt
	public float minSpeed = 5;
	

	private SpriteRenderer render;
    private Rigidbody2D rg;

	public Sprite hurt;

	public GameObject boom;
	public GameObject score;


    public bool _Fire_State;
    public bool _Ice_State;
    public bool _Thunder_State;

    public bool isMaster = false;
    public bool isBox = false;
    public bool isStorm = false;


    public float Resistence_Fire = 0;
    public float Resistence_Ice= 0;
    public float Resistence_Thunder = 0;

    public float Arrow_Dmg_Fire = 0;
    public float Arrow_Dmg_Ice = 0;
    public float Arrow_Dmg_Thunder = 0;

    public float Max_damage = 50;
    public float Mid_damage = 30;
    public float Min_damage = 10;

    float Fire_damage = 0;
    float Ice_damage = 0;
    float Thunder_damage = 0;
    float All_damage = 0;
    /// <summary>
    /// 血条
    /// </summary>
    public  Slider hpSlider;
    public  RectTransform rectTrans;
    public Transform target;
    public Vector2 offsetPos;



    public float HP_Max = 100;
    private float HP_Now;

    private void Awake(){
		render = GetComponent<SpriteRenderer> ();
        rg = GetComponent<Rigidbody2D>();
        Init();
	}

    void Init() {
        HP_Now = HP_Max;
        hpSlider.maxValue = HP_Max;
        hpSlider.minValue = 0 ;
        hpSlider.value = HP_Now;
    }
	public void OnTriggerEnter2D(Collider2D collision){
       
        if (isMaster)
        {
            Calculate();

            var vel = collision.GetComponent<Rigidbody2D>().velocity;

            if (vel.magnitude > maxSpeed)
            {
                HP_Now = HP_Now - All_damage - Max_damage;
            }
            else if ((vel.magnitude) > midSpeed &&
              (vel.magnitude < maxSpeed))
            {

                HP_Now = HP_Now - All_damage - Mid_damage;

            }
            else if ((vel.magnitude) > minSpeed &&
             (vel.magnitude < midSpeed))
            {
                HP_Now = HP_Now - All_damage - Min_damage;

            }
            else if ((vel.magnitude) < minSpeed )
            {

                HP_Now = HP_Now - 0;

            }
            if (HP_Now <= 0)
            {
                Dead();
            }
        }
    }
    

    void Dead(){
		if (isMaster) {
			GameManager._instance.pig.Remove (this);
		}
		Destroy (gameObject);
		Instantiate (boom, transform.position, Quaternion.identity);
		GameObject go = Instantiate (score, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
		Destroy (go, 1.5f);
	}
	
    public void Calculate() {
        /// <summary>
        /// 伤害计算
        /// </summary>
        if (_Fire_State)
        {
            Arrow_Dmg_Fire += Arrow_Dmg_Fire;
			Fire_damage = Arrow_Dmg_Fire - Resistence_Fire;
        }
        if (_Ice_State)
        {
            Arrow_Dmg_Fire += Arrow_Dmg_Ice;
			Ice_damage = Arrow_Dmg_Ice - Resistence_Ice;
        }
        if (_Thunder_State)
        {
            Arrow_Dmg_Fire += Arrow_Dmg_Thunder;
			Thunder_damage = Arrow_Dmg_Thunder - Resistence_Thunder;
        }
			
        All_damage = Fire_damage + Ice_damage + Thunder_damage;
    }
    // Update is called once per frame
    void Update() {
        hpSlider.value = HP_Now;
        Debug.Log("HP:" + hpSlider.value);
    }

}
