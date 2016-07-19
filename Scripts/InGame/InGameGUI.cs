using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InGameGUI : Singleton<InGameGUI>
{
    public Text health;
    public Text cash;
    public Text wave;
    public Text score;
    public Text enemy;

    public Text NameTurret;
    public Text damageTurret;
    public Text attackSpeedTurret;
    public Text rangeTurret;

    public bool showVictoryPanel = false;
    
    public GameObject buildBtn;
    public GameObject createBtnPanel;
    public GameObject upgradeBtnPanel;
    public GameObject victoryPanel;
    public GameObject failPanel;
    public GameObject statsTurretsPanel;

    private InGameGUI() { }

    void Start()
    {
        victoryPanel.SetActive(false);
        failPanel.SetActive(false);
        Cancel();
    }

    public void ShowStatsTurretsPanel()
    {
        statsTurretsPanel.SetActive(true);
    }

    public void ShowCreateTurretsButtons()
    {
        buildBtn.SetActive(false);
        createBtnPanel.SetActive(true);
        upgradeBtnPanel.SetActive(false);
        GameMaster.Instance.ShowFieldToBuild();

    }
    public void ShowUpgradeTurretsButtons()
    {
        buildBtn.SetActive(false);
        createBtnPanel.SetActive(false);
        upgradeBtnPanel.SetActive(true);
    }

    public void Cancel()
    {
        buildBtn.SetActive(true);
        createBtnPanel.SetActive(false);
        upgradeBtnPanel.SetActive(false);
        statsTurretsPanel.SetActive(false);
        if (GameMaster.Instance.lastHitObject != null)
        {
            GameMaster.Instance.lastHitObject = null;
        }
        GameMaster.Instance.ShowFieldToBuild();
    }

    public void ShowVicrotyPanel()
    {
        victoryPanel.SetActive(true);
    }

    public void ShowFailPanel()
    {
        failPanel.SetActive(true);
    }

    public void CreateTurrets(int id)
    {
        GameMaster.Instance.selectedTurret = id;
    }
    
    public void VictroyOfFali(bool victoryOrFail)
    {
        if(victoryOrFail)
        {
            // Победа, сохраняем очки и т.п. пока что просто перезапускаем уровень
            Application.LoadLevel(0);
        }
        else
        {
            // Перезапускаем уровень
            Application.LoadLevel(0);
        }
    }
    
}
