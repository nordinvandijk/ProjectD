using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class TagNameWindow: EditorWindow {
   
    [MenuItem("GameObject/Set Tags and Layers")]
 
    static void Init(){
        TagNameWindow window =
            (TagNameWindow)EditorWindow.GetWindow(typeof(TagNameWindow));
        if (window == null) Debug.Log("No window");
    }
 
    string tag;
    int layer;
    bool tagKindern =false;
    bool layerKindern=false;
 
    void OnGUI(){
        ArrayList queue;
        Transform parent;
        EditorGUIUtility.LookLikeControls(150);
        tag=EditorGUILayout.TagField("Tag name",tag);
        tagKindern=EditorGUILayout.Toggle("Include Children",tagKindern);
        if(GUILayout.Button("Apply Tag")){
            Undo.RegisterSceneUndo("Set Mass Tags");
            foreach(GameObject obj in Selection.gameObjects){
                obj.tag=tag;
                if(tagKindern){
                    queue=new ArrayList();
                    queue.Add(obj.transform);
                    while(queue.Count>0){
                        parent= (Transform) queue[queue.Count-1];
                        queue.RemoveAt(queue.Count-1);
                        parent.gameObject.tag=tag;
                        foreach(Transform child in parent) queue.Add(child);
                    }
                }
            }
        }
        EditorGUILayout.Space();
        layer=EditorGUILayout.LayerField("Layer",layer);
        layerKindern=EditorGUILayout.Toggle("Include Children",layerKindern);
        if(GUILayout.Button("Apply Layer")){
            Undo.RegisterSceneUndo("Set Mass Layers");
            foreach(GameObject obj in Selection.gameObjects){
                obj.layer=layer;
                if(layerKindern){
                    queue=new ArrayList();
                    queue.Add(obj.transform);
                    while(queue.Count>0){
                        parent= (Transform) queue[queue.Count-1];
                        queue.RemoveAt(queue.Count-1);
                        parent.gameObject.layer=layer;
                        foreach(Transform child in parent)queue.Add(child);
                    }
                }
            }
        }
    }
}