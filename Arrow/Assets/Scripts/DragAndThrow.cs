using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndThrow : MonoBehaviour
{
    [SerializeField]
    float launchForce;
    /// <summary>
    /// 速度的三个档位
    /// maxForce/2 /4 /8 为三个速度
    /// </summary>
    [SerializeField]
    float maxForce;

    [SerializeField]
    float midForce;

    [SerializeField]
    float minForce;

    Vector3 downPos;

    private SpriteRenderer render;
    private Rigidbody2D rg;
    /// <summary>
    /// 三种状态
    /// </summary>
    public Sprite Fire_State;
    public Sprite Ice_State;
    public Sprite Thunder_State;

    public bool _Fire_State;
    public bool _Ice_State;
    public bool _Thunder_State;
    /// <summary>
    /// 时间
    /// </summary>
    public float Time_Max = 100;
    public float Time_Pers = 1;
    /// <summary>
    /// 能量
    /// </summary>
    public int Arrow_Energy_Max = 100;
    private int Arrow_Energy_Now;
    public int Arrow_Power_decrease_Pers = 1;
    public Slider egSlider;
    public RectTransform rectTrans;
    public Transform target;
	public LineRenderer aimingLine;
    public Vector2 offsetPos;

	public Canvas Esc;
	public Canvas Ends;

    public Text Times;

    public float Arrow_Dmg_Fire;
    public float Arrow_Dmg_Ice;
    public float Arrow_Dmg_Thunder;

    private bool stopFlag = false;
    /// <summary>
    /// 空中暂停
    /// </summary>
    public void OnPointerDown()
    {
        stopFlag = true;
        OnPointerEnter();
        downPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //rg.velocity = Vector2.zero;
    }
    /// <summary>
    /// 不同速度
    /// </summary>
    public void OnPointerUp()
    {
        stopFlag = false;
        OnPointerExit();
        var upPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = (downPos - upPos) * launchForce;

        if (dir.sqrMagnitude > maxForce)
            dir = dir.normalized * maxForce/2;
        if (dir.sqrMagnitude < maxForce && dir.sqrMagnitude > midForce)
            dir = dir.normalized * maxForce/4;
        if (dir.sqrMagnitude < midForce)
            dir = dir.normalized * maxForce/8;
        
        rg.AddForce(dir, ForceMode2D.Impulse);
    }

    void Awake()
    {
		rg = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer> ();
		Init();
		Esc.enabled = false;
		Ends.enabled = false;
    }
    void Init()
    {
        Arrow_Energy_Now = Arrow_Energy_Max;
        egSlider.maxValue = Arrow_Energy_Max;
        egSlider.minValue = 0;
        egSlider.value = Arrow_Energy_Now;
    }
    /// <summary>
    /// 计时器
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnPointerDown();

        if (Input.GetMouseButtonUp(0))
            OnPointerUp();

		aimingLine.enabled = stopFlag;
		if (stopFlag)
		{
			
			var currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var dir = (downPos - currentPosition);

			aimingLine.SetPosition(0, target.position);
			aimingLine.SetPosition(1, dir.normalized * 5 + target.position);
		}

        if (Time_Max > 0)
        {
            Time_Pers -= Time.deltaTime;
            if (Time_Pers <= 0)
            {
                Time_Pers += 1;
                Time_Max = Time_Max - Time_Pers;
                Times.text = string.Format("{0:D2}:{1:D2}", (int)Time_Max / 60, (int)Time_Max % 60);
				if (stopFlag) {
					Arrow_Energy_Now = Arrow_Energy_Now - Arrow_Power_decrease_Pers;
				}
            }
        }

		transform.right = rg.velocity;

        Debug.Log("Fire:"+_Fire_State);
        Debug.Log("Ice:" + _Ice_State);
        Debug.Log("Thunder:" + _Thunder_State);

		egSlider.value = Arrow_Energy_Now;
		Debug.Log("Enengy:" + egSlider.value);
		/// <summary>
		/// 暂停功能
		/// </summary>
		if(Input.GetKeyDown(KeyCode.Escape)){
			Esc.enabled = true;
			Time.fixedDeltaTime = 0;
			Time.timeScale = 0;
		}
		if (Arrow_Energy_Now <= 0 || Time_Max <= 0) {
			Ends.enabled = true;
			Time.fixedDeltaTime = 0;
			Time.timeScale = 0;
		}

    }
    /// <summary>
    /// 子弹时间
    /// </summary>
    public void OnPointerEnter()
    {
        Time.fixedDeltaTime = 0.001f;
        Time.timeScale = 0.1f;
    }

    public void OnPointerExit()
    {
        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1f;
    }
    public void Statement(int x) {
        
        if (x == 1) {
            GameManager._instance.Fire_State = true;
            _Fire_State = true;
			GameManager._instance.Ice_State = false;
			_Ice_State = false;
			GameManager._instance.Thunder_State = false;
			_Thunder_State =false;
			render.sprite = Fire_State;
        }
        if (x == 2)
        {
			GameManager._instance.Fire_State = false;
			_Fire_State = false;
			GameManager._instance.Ice_State = true;
			_Ice_State = true;
			GameManager._instance.Thunder_State = false;
			_Thunder_State =false;
			render.sprite = Ice_State;
        }
        if (x == 3)
        {
			GameManager._instance.Fire_State = false;
			_Fire_State = false;
			GameManager._instance.Ice_State = false;
			_Ice_State = false;
			GameManager._instance.Thunder_State = true;
			_Thunder_State =true;
			render.sprite = Thunder_State;
        }
    }

	public void Restart()
	{
		Application.LoadLevel(0);
	}
	public void Quit(){
		Application.Quit();
	}
 
}
