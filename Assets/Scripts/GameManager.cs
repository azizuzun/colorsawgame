using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public static Player player;

	public bool advancing;
	public bool gameOver;

	public Text levelText;
	public Text stageText;
	public GameObject gameOverUI;
	public GameObject blockDestructionParticles;

	public Level[] levels;
	public int curLevelNr;
	public int curStageNr;
	private GameObject curStage;
    private GameObject curStage2;
    public GameObject copy;

    private void Awake()
	{
		if (!gm)
		{
			gm = this;
		}
	}

	private void Start()
	{
		NextLevel();
	}
    /*IF presses R reload the scene*/
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RestartScene();
		}
	}
    /*Makes the reload above*/
	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Advance()
	{
		if (advancing)
		{
			return;
		}
		advancing = true;

		if (curStageNr < levels[curLevelNr - 1].stages.Length)
		{
			NextStage();
		}
		else if (curLevelNr < levels.Length)
		{
				
			NextLevel();
		}
	}
    /*1 of 3 levels*/
	public void NextLevel()
	{
		curLevelNr++;
		
		curStageNr = 0;
		NextStage();
		RefreshUI();
	}
    /*1 of 2 stages*/
	public void NextStage()
	{
		curStageNr++;
		if (curStage)
		{
			Destroy(curStage);
		}
       
        StartCoroutine(SpawnNextStage());
		RefreshUI();
	}

	public void RefreshUI()
	{
		levelText.text = "LEVEL " + curLevelNr.ToString();
		stageText.text = curStageNr.ToString() + "/" + levels[curLevelNr - 1].stages.Length;
	}

	private IEnumerator SpawnNextStage()
	{
		yield return new WaitForSeconds(1f);
		curStage = Instantiate(levels[curLevelNr - 1].stages[curStageNr - 1]);
        
        advancing = false;

        
    }
   
    
    public void GameOver()
	{
		gameOver = true;
		gameOverUI.SetActive(true);
	}
}

[System.Serializable]
public class Level
{
	public GameObject[] stages;
}