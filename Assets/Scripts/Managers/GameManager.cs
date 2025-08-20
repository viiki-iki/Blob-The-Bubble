using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Dictionary<string, bool> missionStates = new Dictionary<string, bool>();

    [HideInInspector] public GameObject activeItem = null;
    [HideInInspector] public Button lastButtonClicked = null;
    [HideInInspector] public bool isUsingItem = false;

    private void Awake()
    {   
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadMissionStates();
    }

    public void CompleteMission(string missionKey)
    {
        missionStates[missionKey] = true;
        SaveMissionStates();
    }

    public bool IsMissionCompleted(string missionKey)
    {
        return missionStates.ContainsKey(missionKey) && missionStates[missionKey];
    }

    private void SaveMissionStates()
    {
        foreach (var mission in missionStates)
        {
            PlayerPrefs.SetInt(mission.Key, mission.Value ? 1 : 0);
        }
        PlayerPrefs.Save();
       // Debug.Log("Estados das missões salvos.");
    }

    private void LoadMissionStates()
    {
        string[] missionKeys = { "Mission1", "Mission2", "Mission3" }; // Adicione suas missões aqui

        foreach (string missionKey in missionKeys)
        {
            // Caso o PlayerPrefs não tenha um valor para a missão, ele irá começar como não completada (false).
            missionStates[missionKey] = PlayerPrefs.GetInt(missionKey, 0) == 1;
        }
        Debug.Log("Estados das missões carregados.");
    }

    [ContextMenu("Resetar Missões")]
    public void ResetMissionsForTesting()
    {
        missionStates["Mission1"] = false; // Reseta a missão 1 para não completada
        missionStates["Mission2"] = false; // Reseta a missão 2 para não completada
        missionStates["Mission3"] = false; // Reseta a missão 3 para não completada

        SaveMissionStates(); // Salva imediatamente para persistir no PlayerPrefs
        Debug.Log("Missões resetadas.");
    }
}
