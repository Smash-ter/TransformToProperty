using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Drawing.Drawing2D;
//using System.Numerics;

[CustomEditor(typeof(Transform2Property))]
public class Transform2PropertyEditor : Editor
{
    public void MatrixField(string name, Matrix4x4 matrix)
    {
        GUILayout.Label(name);
        EditorGUI.indentLevel += 1;
        for (int j = 0; j < 4; j++)
        {
            EditorGUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            for (int i = 0; i < 4; i++)
            {
                matrix[i,j] = EditorGUILayout.FloatField(matrix[i,j]); 
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel -= 1;
    }

    public bool BeginSection(string title, ref bool foldout)
    {
        EditorGUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
        foldout = EditorGUILayout.Foldout(foldout, title, EditorStyles.foldoutHeader);
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel += 1;
        return foldout;
    }
    
    public void EndSection()
    {
        EditorGUI.indentLevel -= 1;
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Transform2Property t2p = (Transform2Property)((Editor)this).target;

        if (!string.IsNullOrEmpty(((Transform2Property)t2p).userPropertyName) && !((Editor)this).serializedObject.isEditingMultipleObjects)
        {
            EditorGUILayout.HelpBox("Shader Properties being Used\n" + ((Transform2Property)t2p).forwardName + "[float4]\n" + ((Transform2Property)t2p).rightName + "[float4]\n" + ((Transform2Property)t2p).upName + "[float4]\n" + ((Transform2Property)t2p).backName + "[float4]\n" + ((Transform2Property)t2p).leftName + "[float4]\n" + ((Transform2Property)t2p).downName + "[float4]\n" + ((Transform2Property)t2p).positionName + "[float4]\n" + ((Transform2Property)t2p).rotationName + "[float4]\n" + ((Transform2Property)t2p).scaleName + "[float4]\n" + ((Transform2Property)t2p).matrixName + "[float4x4]", (MessageType)1);
            
        }
        else
        {
            EditorGUILayout.HelpBox("Choose a parameter name to use this feature.", (MessageType)1);
        }
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).forwardName, ((Transform2Property)t2p).forwardValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).rightName, ((Transform2Property)t2p).rightValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).upName, ((Transform2Property)t2p).upValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).backName, ((Transform2Property)t2p).backValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).leftName, ((Transform2Property)t2p).leftValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).downName, ((Transform2Property)t2p).downValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).positionName, ((Transform2Property)t2p).positionValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).rotationName, ((Transform2Property)t2p).rotationValue, Array.Empty<GUILayoutOption>());
        EditorGUILayout.Vector3Field(((Transform2Property)t2p).scaleName, ((Transform2Property)t2p).scaleValue, Array.Empty<GUILayoutOption>());
        MatrixField(((Transform2Property)t2p).matrixName, ((Transform2Property)t2p).matrixValue);
        
        //EndSection();
    }
}
