using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public SaveDataClass saveData = new();
    public GameManager gameManager;
    [SerializeField] private TMPro.TextMeshProUGUI SaveLoadText;

    private void Start()
    {
        if (Debug.isDebugBuild)
        {
            SaveLoadText.text = "Press S to save, L to load";
        }
        else
        {
            SaveLoadText.text = "";
        }
    }
    private void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

    }

    // Save the data to the file
    public void Save()
    {

        BuildList();
        string data = JsonUtility.ToJson(saveData);
        string filePath = Application.persistentDataPath + "/save.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, data);
        if (Debug.isDebugBuild)
        {
            SaveLoadText.text = "Saved to: " + filePath;
        }
        else SaveLoadText.text = "Saved!";

    }
    // Load the data from the file
    public void Load()
    {
        string filePath = Application.persistentDataPath + "/save.json";
        if (System.IO.File.Exists(filePath))
        {
            string data = System.IO.File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveDataClass>(data);
            BuildArray();
            gameManager.UpdateBoard();
            if (Debug.isDebugBuild)
            {
                SaveLoadText.text = "Loaded from: " + filePath;
            }
            else SaveLoadText.text = "Loaded!";
        }
        else
        {
            if (Debug.isDebugBuild)
            {
                SaveLoadText.text = "No save file found at: " + filePath;
            }
            else SaveLoadText.text = "No save file found!";
        }
    }




    // Building and rebuilding the list and array
    private void BuildList() // Build the list from the array
    {
        saveData.values2048Save.Clear();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)

                saveData.values2048Save.Add(gameManager.values2048[i, j]);
        }
    }
    private void BuildArray() // Build the array from the list (reverse of BuildList)
    {
        int index = 0;
        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < 4; j++)
            {
                gameManager.values2048[i, j] = saveData.values2048Save[index];
                index++;
            }
        }
    }
}


[System.Serializable]
public class SaveDataClass // just keeps the list
{
    public List<int> values2048Save = new();
}