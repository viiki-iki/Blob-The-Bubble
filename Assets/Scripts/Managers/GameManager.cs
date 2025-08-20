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
       // Debug.Log("Estados das miss�es salvos.");
    }

    private void LoadMissionStates()
    {
        string[] missionKeys = { "Mission1", "Mission2", "Mission3" }; // Adicione suas miss�es aqui

        foreach (string missionKey in missionKeys)
        {
            // Caso o PlayerPrefs n�o tenha um valor para a miss�o, ele ir� come�ar como n�o completada (false).
            missionStates[missionKey] = PlayerPrefs.GetInt(missionKey, 0) == 1;
        }
        Debug.Log("Estados das miss�es carregados.");
    }

    [ContextMenu("Resetar Miss�es")]
    public void ResetMissionsForTesting()
    {
        missionStates["Mission1"] = false; // Reseta a miss�o 1 para n�o completada
        missionStates["Mission2"] = false; // Reseta a miss�o 2 para n�o completada
        missionStates["Mission3"] = false; // Reseta a miss�o 3 para n�o completada

        SaveMissionStates(); // Salva imediatamente para persistir no PlayerPrefs
        Debug.Log("Miss�es resetadas.");
    }
}
