using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof ( CompositeBehavior ) )]
public class CompositeBehaviorEditor : Editor // this code is shit in a few places, don't try to do it this way next time, please..
{
    public override void OnInspectorGUI()
    {   
        //if(GUILayout.Button("test")) Debug.Log("yay");
        //base.OnInspectorGUI();
        //some basic setup
        CompositeBehavior compositeBehavior = (CompositeBehavior) target;
        //compositeBehavior.update(7); DATA MIGRATION PURPOSES ONLY

        Rect cursorRect = new Rect();
        cursorRect.height = EditorGUIUtility.singleLineHeight;
        GUIStyle style = new GUIStyle();
        style.stretchWidth = true;
        
        //getting behaviors
        if(compositeBehavior.behaviors == null || compositeBehavior.behaviors.Length == 0){
            horizontalDiv();
            EditorGUILayout.HelpBox("No behaviors added to the composite behavior", MessageType.Warning);
            horizontalDivEnd();
            cursorRect.height = EditorGUIUtility.singleLineHeight;
        }
        else{
            verticalDiv();

            cursorRect.x = 30f;
            cursorRect.width = EditorGUIUtility.currentViewWidth - 95f;
            style.fixedWidth = 1;
            style.border = new RectOffset(0, 0, 0, 1);
            style.contentOffset = new Vector2(0, 0);
            style.overflow = new RectOffset(0, 0, 0, 1);
            style.padding = new RectOffset(0, 0, 0, 1);
            horizontalDiv(style);
            //EditorGUI.LabelField(cursorRect, "Behaviors"); old
            EditorGUILayout.LabelField("Num", style);
            EditorGUILayout.LabelField("Behaviors", style);

            cursorRect.x = EditorGUIUtility.currentViewWidth - 65f;
            cursorRect.width = 60f;
            //EditorGUI.LabelField(cursorRect, "Weights"); old
            EditorGUILayout.LabelField("Weights", style);
            EditorGUILayout.LabelField("Multipliers", style);
            horizontalDivEnd();
            cursorRect.y += EditorGUIUtility.singleLineHeight * 1.3f;

            EditorGUI.BeginChangeCheck();

            for(int i = 0; i < compositeBehavior.behaviors.Length; i++){
                horizontalDiv();{

                cursorRect.x = 5f;
                cursorRect.width = 20f;
                style.fixedWidth = 1f;
                horizontalDiv(style);{
                //EditorGUI.LabelField(cursorRect, i.ToString());
                EditorGUILayout.LabelField(i.ToString());
                horizontalDivEnd();}

                cursorRect.x = 30f;
                cursorRect.width = EditorGUIUtility.currentViewWidth - 95f;
                style.fixedWidth = 10f;
                style.stretchWidth = true;
                horizontalDiv();{
                //compositeBehavior.behaviors[i] = (FlockBehavior) EditorGUI.ObjectField(cursorRect, compositeBehavior.behaviors[i], typeof(FlockBehavior), false); // old // place, what it displays, type to drag, non scene obj only
                compositeBehavior.behaviors[i] = (FlockBehavior) EditorGUILayout.ObjectField(compositeBehavior.behaviors[i], typeof(FlockBehavior));
                horizontalDivEnd();}
                cursorRect.x = EditorGUIUtility.currentViewWidth - 65f;
                cursorRect.width = 60f;
                style.fixedWidth = 60f;
                horizontalDiv(style);{
                //compositeBehavior.weights[i] = EditorGUI.FloatField(cursorRect, compositeBehavior.weights[i]); old
                compositeBehavior.weights[i] = EditorGUILayout.FloatField(compositeBehavior.weights[i]); 

                cursorRect.y += EditorGUIUtility.singleLineHeight * 1.2f;
                horizontalDivEnd();}

                horizontalDiv(style);{
                //compositeBehavior.weights[i] = EditorGUI.FloatField(cursorRect, compositeBehavior.weights[i]); old
                compositeBehavior.multipliers[i] = EditorGUILayout.FloatField(compositeBehavior.multipliers[i]); 

                cursorRect.y += EditorGUIUtility.singleLineHeight * 1.2f;
                horizontalDivEnd();}

                horizontalDivEnd();}
            }
            verticalDivEnd();
            if(EditorGUI.EndChangeCheck()){
                EditorUtility.SetDirty(compositeBehavior);
            }
        }

        
        cursorRect.x = 5f;//EditorGUIUtility.currentViewWidth/3
        cursorRect.width = EditorGUIUtility.currentViewWidth - 10f; //EditorGUIUtility.currentViewWidth/3;
        cursorRect.y += EditorGUIUtility.singleLineHeight * 0.5f;
        //if(GUI.Button(cursorRect, "Add behavior")){
        if(GUILayout.Button("Add behavior")){
            //add todo
            addBehavior(compositeBehavior);
            EditorUtility.SetDirty(compositeBehavior);
            Debug.Log("yay");
        }

        cursorRect.y += EditorGUIUtility.singleLineHeight * 1.5f;
        if(compositeBehavior.behaviors != null && compositeBehavior.behaviors.Length != 0){
            //if(GUI.Button(cursorRect, "Remove behavior")){
            if(GUILayout.Button("Remove behavior")){
                //delete todo
                removeBehavior(compositeBehavior);
                EditorUtility.SetDirty(compositeBehavior);
            }
        }
    }

    void horizontalDiv(GUIStyle style = null){
        if(style != null){
            EditorGUILayout.BeginHorizontal(style);
            return;
        }
        EditorGUILayout.BeginHorizontal();

    }

    void horizontalDivEnd(){
        EditorGUILayout.EndHorizontal();
    }

    void verticalDiv(GUIStyle style = null){
        if(style != null){
            EditorGUILayout.BeginVertical(style);
            return;
        }
        EditorGUILayout.BeginVertical();
    }

    void verticalDivEnd(){
        EditorGUILayout.EndVertical();
    }

    void addBehavior(CompositeBehavior compositeBehavior){
        int oldCount = (compositeBehavior.behaviors != null) ? compositeBehavior.behaviors.Length : 0;

        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];
        float[] newMultipliers = new float[oldCount + 1];
        for(int i = 0; i < oldCount; i++){
            newBehaviors[i] = compositeBehavior.behaviors[i];
            newWeights[i] = compositeBehavior.weights[i];
            newMultipliers[i] = compositeBehavior.multipliers[i];
        }
        newWeights[oldCount] = 1;
        newMultipliers[oldCount] = 1;
        compositeBehavior.behaviors = newBehaviors;
        compositeBehavior.weights = newWeights;
        compositeBehavior.multipliers = newMultipliers;
    }

    void removeBehavior(CompositeBehavior compositeBehavior){
        if(compositeBehavior.behaviors == null || compositeBehavior.behaviors.Length < 1){
            Debug.LogError("Trying to remove a behavior, while composite behavior has none stored. Name: " + name, this);
            return;
        }
        int oldCount = compositeBehavior.behaviors.Length;

        if(oldCount == 1){
            compositeBehavior.behaviors = null;
            compositeBehavior.weights = null;
            compositeBehavior.multipliers = null;
            return;
        }

        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];
         float[] newMultipliers = new float[oldCount - 1];
        for(int i = 0; i < oldCount - 1; i++){
            newBehaviors[i] = compositeBehavior.behaviors[i];
            newWeights[i] = compositeBehavior.weights[i];
            newMultipliers[i] = compositeBehavior.multipliers[i];
        }
        compositeBehavior.behaviors = newBehaviors;
        compositeBehavior.weights = newWeights;
        compositeBehavior.multipliers = newMultipliers;
    }
}
