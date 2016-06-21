using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
	public GameObject hpBar;
	public GameObject miniHpBar;
	private float maxHP = 100f;
	private float currentHp = 0f;

	//public float timer; 
	//public Text clockText;

	void Start () {
		currentHp = maxHP;
		//test the HP bar 
		InvokeRepeating ("decreaseHP", 1f, 1f);
	}
	
	void Update ()
    {
        //timerCountDown();
	}

	void decreaseHP()
    {
		//damage calculate will replace this code
		currentHp -= 2f; 

		// calculate the hpValue to scale the HP bar
		float hpValue = currentHp / maxHP;
		updateHpBar(hpValue);
	}

	void updateHpBar(float myHp)
    {
		// myHp value have to be between 0-1
		hpBar.transform.localScale = new Vector3 (myHp,hpBar.transform.localScale.y,hpBar.transform.localScale.z);
		miniHpBar.transform.localScale = new Vector3 (myHp,miniHpBar.transform.localScale.y,miniHpBar.transform.localScale.z);
	}

    /*
	void timerCountDown(){
		timer -= Time.deltaTime;
		printTimer ();
	}
    */

    /*
	void printTimer() {
		if (timer > 0) {
			int minutes = Mathf.FloorToInt (timer / 60F);
			int seconds = Mathf.FloorToInt (timer - minutes * 60);		
			string time = string.Format ("{0:0}:{1:00}", minutes, seconds);
			clockText.text = time;
		} else {
			Time.timeScale = 0; 
			clockText.text =  "TIME IS UP !";
		}			
	}
    */
}
