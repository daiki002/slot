using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum State
{
    Start,
    Playing,
    Stay,
}

public class GameController : MonoBehaviour
{
	public GameObject imgT;
	public GameObject imgC;
	public GameObject imgD;
	public GameObject imgDL;
	public GameObject imgDR;






	public Button coinbt;
	public Button[] coinicon;
	public int coin = 100;
	public Text cointext;

	public GameObject goodtext;

	public int[] yakuvalue;
	public Text[] yakutext;

	int score;

	int odds = 0;

	public Button[] stopbt;
	public Button playbt;

	public GameObject[] reels;
	ReelController[] rc = new ReelController[3];


	int[] lineL, lineC, lineR;
	int stopline_len = 3;
	State state = State.Start;

	// Use this for initialization
	void Start()
	{


		for (int i = 0; i < yakutext.Length; i++)
		{
			yakutext[i].text = yakuvalue[i] + "×0";
		}


		cointext.text = "Coin:" + coin;
		for (int i = 0; i < 3; i++)
		{
			rc[i] = reels[i].GetComponent<ReelController>();
		}

		//playbt.interactable = true;

	}


	void Update()
	{
		if (stopline_len == 3 && state == State.Playing)
		{
			state = State.Stay;
			Chack();
			for (int i = 0; i < coinicon.Length; i++)
			{
				coinicon[i].interactable = false;
               
               
			}
			coinbt.interactable = true;
           
			odds = 0;
			yakutxtUpdate();
		}

		//if (state == State.Stay && odds != 0)
		//{ //Coinを1つでも投入するとプレイできる
		//	playbt.interactable = true;
		//}

	}

	public void Play()
	{   //Playボタンを押した時の動作
		playbt.interactable = false;
		stopline_len = 0;
		state = State.Playing;
		for (int i = 0; i < 3; i++)
		{
			rc[i].Reel_Move();
			stopbt[i].interactable = true;
		}

	}

	public void Stopbt_f(int id)
	{
		stopbt[id].interactable = false;
	}


	public void SetLineL(int[] line)
	{
		lineL = new int[3];
		lineL = line;
		stopline_len++;
	}
	public void SetLineC(int[] line)
	{
		lineC = new int[3];
		lineC = line;
		stopline_len++;
	}
	public void SetLineR(int[] line)
	{
		lineR = new int[3];
		lineR = line;
		stopline_len++;
	}


	public void Chack()
	{
		for (int i = 0; i < 3; i++)
		{
			if (lineL[i] == lineC[i] && lineC[i] == lineR[i])
			{
				switch (i)
				{
					case 0:
						Debug.Log("一番下のラインが揃ったよ。");
						imgD.SetActive(true);
						goodtext.SetActive(true);
						break;
					case 1:
						Debug.Log("真ん中のラインが揃ったよ。");
						imgC.SetActive(true);
						goodtext.SetActive(true);

						break;
					case 2:
						Debug.Log("一番上のラインが揃ったよ。");
						goodtext.SetActive(true);
						imgT.SetActive(true);
						break;
					default:
						Debug.Log("設定ミスでは???");
						break;
				}

				coin += yakuvalue[lineL[i]] * odds;
				cointext.text = "Coin:" + coin;

			}
		}

		if (lineL[0] == lineC[1] && lineC[1] == lineR[2])
		{
			Debug.Log("ラインの左下がりが揃ったよ。");
			imgDR.SetActive(true);
			goodtext.SetActive(true);

			Debug.Log(yakuvalue[lineL[0]] * odds + "yen");
			coin += yakuvalue[lineL[0]] * odds;
			cointext.text = "Coin:" + coin;
		}
		if (lineL[2] == lineC[1] && lineC[1] == lineR[0])
		{
			Debug.Log("ラインの右上がりが揃ったよ。");
			imgDL.SetActive(true);
			goodtext.SetActive(true);

			Debug.Log(yakuvalue[lineL[2]] * odds + "yen");
			coin += yakuvalue[lineL[2]] * odds;
			cointext.text = "Coin:" + coin;
		}
	}



	public void Buy()
	{
		goodtext.SetActive(false);
		imgD.SetActive(false);
		imgC.SetActive(false);
		imgT.SetActive(false);
		imgDL.SetActive(false);
		imgDR.SetActive(false);
		if (coin <= 0)
		{ //コインがないならリターンする
			return;
		}

		if (odds != 2)
		{
			playbt.interactable = true;
			coin--;
			odds++;
			coinicon[odds - 1].interactable = true;

			yakutxtUpdate();
		}
		else
		{ //odds == 2
			coin--;
			odds = 3;
			coinicon[2].interactable = true;
			coinbt.interactable = false;
			yakutxtUpdate();
		}

		
		cointext.text = "Coin:" + coin;
	}


	void yakutxtUpdate()
	{
		for (int i = 0; i < yakutext.Length; i++)
		{
			yakutext[i].text = yakuvalue[i] + "×" + odds;
		}
	}
}
