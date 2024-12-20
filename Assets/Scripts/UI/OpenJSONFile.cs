using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using System;
using TMPro;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileJson : MonoBehaviour, IPointerDownHandler {
    public String file;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        // Only allow JSON files
        UploadFile(gameObject.name, "OnFileUpload", ".json", false);
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        // Only allow JSON files
        var paths = StandaloneFileBrowser.OpenFilePanel("Select JSON File", "", "json", false);
        if (paths.Length > 0) {
            file = paths[0];
            GameObject.FindWithTag("MenuManager").GetComponent<MenuController>().file = file;
            GetComponent<Button>().GetComponentInChildren<TMP_Text> ().text = System.IO.Path.GetFileName(file);
            Debug.Log(file);
        }
    }
#endif
}