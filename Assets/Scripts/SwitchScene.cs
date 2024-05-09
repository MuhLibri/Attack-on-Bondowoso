using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // // Get the currently active scene
        // Scene currentScene = scene.;

        // // Iterate through all root GameObjects in the scene hierarchy
        // foreach (GameObject rootGameObject in currentScene.GetRootGameObjects())
        // {
        //     // List the name of each GameObject
        //     Debug.Log("GameObject Name: " + rootGameObject.name);
        // }

        SaveManager.SetLoaded();
        // Debug.Log("Masuk");
        // // Load all available save data
        // SaveData[] saveDatas = SaveManager.LoadAllData();
        // // Ex: chose Save1.json
        // string fileName = saveDatas[0].name;
        // string folderPath = Application.persistentDataPath;

        // GameObject saveManager = GameObject.Find("GameObject");
        // Debug.Log("Null kah: " + saveManager.IsUnityNull());
        // SaveManager sm = saveManager.GetComponent<SaveManager>();

        // Debug.Log("Lewat");
        // string filePath = $"{folderPath}/{fileName}.{SaveManager.fileFormat}";
        // sm.LoadGame(filePath);
        // Debug.Log("Keload");
    }

    public void Switch() {
        SceneManager.LoadScene("CobaLibri");

    }
}
