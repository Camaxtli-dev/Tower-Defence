using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMaster : Singleton<GameMaster>
{
    public FieldToBuild focusedField;
    public Transform fieldToBuildRoot;
    public LayerMask fieldToBuildLayerMask;
    public LayerMask turretLayerMask;
    public GameObject lastHitObject;
    private bool showFieldToBuild = false;

    public GameObject[] allTurrets;
    public int selectedTurret = -1;
    private Camera camera;
    public bool rayNotOnField;

    public int healthCount;
    public int scoreCount;
    public int cashCount;
    public int waveCount;
    public int enemyCount;

    public GameObject waypointRoot;
    public List<Transform> waypoints;
    
    public GameObject[] mobs;
    public GameObject spawnPoints;

    private WaveMobs waveMobs;
    private CreateOrUpgrade playerStructure;
    private GameObject myStruct;

    public bool rayUI = false;

    private GameMaster() { }
    
    void Start()
    {
        waveMobs = new WaveMobs();
        playerStructure = new CreateOrUpgrade();
        waypoints = new List<Transform>();
        camera = GetComponent<Camera>();
        findWaypoints();

        healthCount = 10;
        cashCount = 1000;
        Wave();
        UpdateHUD();
    }

    void Update()
    {         
        if (waveMobs.waveActive)
        {
            waveMobs.nextWave();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FindPlane();
            HandlerClickField();
        }
        if (healthCount <= 0)
        {
            waveMobs.waveActive = false;
            InGameGUI.Instance.ShowFailPanel();
        }
        if(waveCount > 5)
        {
            waveMobs.waveActive = false;
            if (enemyCount == 0)
            {
                InGameGUI.Instance.ShowVicrotyPanel();
            }
        }
    }

    private void FindPlane()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, fieldToBuildLayerMask) && !rayUI)
        {
                lastHitObject = hit.collider.gameObject;
            
            rayNotOnField = false;
        }
        else
        {
            if (lastHitObject && InGameGUI.Instance.createBtnPanel.activeSelf)
            {
                lastHitObject = null;
            }
        }
        if (lastHitObject == null)
        {
            rayNotOnField = true;
        }
    }

    private void Wave()
    {
        waveMobs.Mobs(mobs);
        waveMobs.SpawnPoints(spawnPoints);
        waveMobs.StartNewWave();
    }

    public void ShowFieldToBuild()
    {
        if (InGameGUI.Instance.createBtnPanel.activeSelf)
        {
            foreach (Transform field in fieldToBuildRoot)
            {
                field.gameObject.GetComponent<Renderer>().enabled = true;
            }
            showFieldToBuild = true;
        }
        else
        {
            foreach (Transform field in fieldToBuildRoot)
            {
                field.gameObject.GetComponent<Renderer>().enabled = false;
            }
            showFieldToBuild = false;
        }
    }

    #region Create, Upgrade and Sell turrets

    private void CreateTurrets()
    {
        if (lastHitObject != null && selectedTurret != -1)
        {
            focusedField = lastHitObject.GetComponent<FieldToBuild>();
            if(focusedField.myStructure == null && allTurrets[selectedTurret].GetComponent<TurretBase>().price <= cashCount)
            {
                cashCount -= allTurrets[selectedTurret].GetComponent<TurretBase>().price;
                playerStructure.Create(allTurrets[selectedTurret], lastHitObject.transform.position);
                InGameGUI.Instance.Cancel();
                lastHitObject = null;
                selectedTurret = -1;
                UpdateHUD();
            }
        }
    }

    public void ConfirmUpgrade()
    {
        focusedField = lastHitObject.GetComponent<FieldToBuild>();
        if (focusedField.myStructure.GetComponent<TurretBase>().upgradeTurrets != null && focusedField.myStructure.GetComponent<TurretBase>().upgradeTurretsCost <= cashCount)
        {
            cashCount -= focusedField.myStructure.GetComponent<TurretBase>().upgradeTurretsCost;
            playerStructure.Upgrade(focusedField.myStructure, focusedField.myStructure.GetComponent<TurretBase>().upgradeTurrets);
            InGameGUI.Instance.Cancel();
            lastHitObject = null;
            UpdateHUD();
        }
    }

    public void SellTurrets()
    {
        focusedField = lastHitObject.GetComponent<FieldToBuild>();
        if (focusedField.myStructure != null)
        {
            cashCount += focusedField.myStructure.GetComponent<TurretBase>().price / 2;
            focusedField.myStructure.GetComponent<TurretBase>().Destroy();
            InGameGUI.Instance.Cancel();
            lastHitObject = null;
            UpdateHUD();
        }
    }

    #endregion

    #region Update UI

    public void UpdateHUD()
    {
        InGameGUI.Instance.health.text = healthCount.ToString();
        InGameGUI.Instance.score.text = scoreCount.ToString();
        InGameGUI.Instance.cash.text = cashCount.ToString();
        InGameGUI.Instance.wave.text = waveCount.ToString();
        InGameGUI.Instance.enemy.text = enemyCount.ToString();
    }

    public void StatsTurret()
    {
        focusedField = lastHitObject.GetComponent<FieldToBuild>();
        InGameGUI.Instance.ShowStatsTurretsPanel();
        InGameGUI.Instance.damageTurret.text = focusedField.myStructure.GetComponent<TurretBase>().projectile.GetComponent<Projectile>().damage.ToString();
        InGameGUI.Instance.attackSpeedTurret.text = focusedField.myStructure.GetComponent<TurretBase>().reloadTime.ToString();
        InGameGUI.Instance.rangeTurret.text = focusedField.myStructure.GetComponent<TurretBase>().transform.FindChild("RadiusDamage").GetComponent<SphereCollider>().radius.ToString();
    }

    public void RayUIEnter()
    {
        rayUI = true;
    }

    public void RayUIExit()
    {
        rayUI = false;
    }

    #endregion

    private void findWaypoints()
    {
        int max = 0;
        foreach (Transform w in waypointRoot.transform)
        {
            w.name = max.ToString();
            max++;
            waypoints.Add(w);
        }
        waypoints[waypoints.Count - 1].gameObject.AddComponent<PlayerBase>(); // Последняя точка - база игрока
    }

    private void HandlerClickField()
    {
        if (InGameGUI.Instance.createBtnPanel.activeSelf)
        {
            if (!rayNotOnField && lastHitObject.GetComponent<FieldToBuild>().myStructure == null)
            {
                CreateTurrets();
            }
            else if (!rayNotOnField && lastHitObject.GetComponent<FieldToBuild>().myStructure != null)
            {
                InGameGUI.Instance.ShowCreateTurretsButtons();
            }
        }
        else
        {
            if (!rayNotOnField && lastHitObject.GetComponent<FieldToBuild>().myStructure != null)
            {
                InGameGUI.Instance.ShowUpgradeTurretsButtons();
                StatsTurret();
                InGameGUI.Instance.ShowStatsTurretsPanel();
            }
        }
    }

}
