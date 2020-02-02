using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class qteSystem : MonoBehaviour
{
	public GameObject DisplayBox;
	public GameObject PassBox;
	public int QTEGen;
	public int waitingKey;
	public int correctKey;
	public int coutingDown;
	public GameObject car;
	public GameObject score_label;
	private Animator anim;
	int score;
	int sub_score;
	int QTE_count;
	string[] keys;
	public Sprite car0;
	public Sprite car1;
	public Sprite car2;
	public Sprite car3;
	public Sprite car4;
	public Sprite car5;
	public Sprite car6;
	public Sprite car7;
	int toFixCount;

	void setCarTexture(int index, int fix) {
		if (fix == 0) {
			if (index == 1) {
				car.GetComponent<SpriteRenderer>().sprite = car1;
			} else if (index == 2) {
				car.GetComponent<SpriteRenderer>().sprite = car3;
			} else if (index == 3) {
				car.GetComponent<SpriteRenderer>().sprite = car5;
			} else if (index == 4) {
				car.GetComponent<SpriteRenderer>().sprite = car7;
			}
		} else if (fix == 1) {
			if (index == 1) {
				car.GetComponent<SpriteRenderer>().sprite = car0;
			} else if (index == 2) {
				car.GetComponent<SpriteRenderer>().sprite = car2;
			} else if (index == 3) {
				car.GetComponent<SpriteRenderer>().sprite = car4;
			} else if (index == 4) {
				car.GetComponent<SpriteRenderer>().sprite = car6;
			}
		}
	}

	void Start() {
    	anim = GetComponent<Animator>();
    	score = 0;
    	sub_score = 0;
    	QTE_count = 3;
    	keys = new string[26] {"NKey", "BKey", "VKey", "CKey", "XKey", "WKey", "MKey", "LKey", "KKey", "JKey", "HKey", "GKey", "FKey", "DKey", "SKey", "QKey", "PKey", "OKey", "IKey", "UKey", "YKey", "TKey", "RKey", "EKey", "ZKey", "AKey"};
    	toFixCount = Random.Range(1, 4);
    	setCarTexture(toFixCount, 0);
    }

	void Update() {
    	if (!car.GetComponent<Animator>().IsInTransition(0) && car.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
			if (waitingKey == 0) {
				QTEGen = Random.Range(1, 26);
				coutingDown = 1;
				StartCoroutine(CountDown());
				waitingKey = QTEGen;
				DisplayBox.GetComponent<Text>().text = "[" + keys[waitingKey][0] + "]";
			}
			if (Input.anyKeyDown) {
				for (int i = 1; i < keys.Length; i++) {
	    			if ((QTEGen == i) &&  (Input.GetButtonDown(keys[i]))) {
	    				correctKey = 1;
	    				sub_score++;
	    				StartCoroutine(KeyPressing());
	    			}
	    		}
	    	}
		}
	}

	IEnumerator KeyPressing() {
		QTEGen = 4;
		if (correctKey == 1) {
			if (sub_score == QTE_count) {
				score++;
				score_label.GetComponent<Text>().text = score.ToString();
				QTE_count += 2;
				sub_score = 0;
				car.GetComponent<Animator>().Play("endAnimation");
				toFixCount = Random.Range(1, 4);
    			setCarTexture(toFixCount, 1);
    			waitingKey = 0;
    			yield return new WaitForSeconds(1.5f);
    			toFixCount = Random.Range(1, 4);
    			setCarTexture(toFixCount, 1);
    			car.GetComponent<Animator>().Play("enter", -1, 0f);
	    	} else {
	    		coutingDown = 2;
				PassBox.GetComponent<Text>().text = "Pass";
				yield return new WaitForSeconds(1.5f);
				correctKey = 0; 
				PassBox.GetComponent<Text>().text = "";
				DisplayBox.GetComponent<Text>().text = "";
				yield return new WaitForSeconds(1.5f);
				waitingKey = 0;
				coutingDown = 1;
	    	}
		} else if (correctKey == 2) {
			coutingDown = 2;
			PassBox.GetComponent<Text>().text = "Failed";
			yield return new WaitForSeconds(1.5f);
			correctKey = 0; 
			PassBox.GetComponent<Text>().text = "";
			DisplayBox.GetComponent<Text>().text = "";
			yield return new WaitForSeconds(1.5f);
			waitingKey = 0;
			coutingDown = 1;	
		}
	}

	IEnumerator CountDown() {
		yield return new WaitForSeconds(1.5f);
		if (coutingDown == 1) {
			QTEGen = 4;
			coutingDown = 2;
			PassBox.GetComponent<Text>().text = "Failed";
			yield return new WaitForSeconds(1.5f);
			correctKey = 0; 
			PassBox.GetComponent<Text>().text = "";
			DisplayBox.GetComponent<Text>().text = "";
			yield return new WaitForSeconds(1.5f);
			waitingKey = 0;
			coutingDown = 1;
			toFixCount = Random.Range(1, 4);
    		setCarTexture(toFixCount, 1);
    		car.GetComponent<Animator>().Rebind();
		}
	}
}
