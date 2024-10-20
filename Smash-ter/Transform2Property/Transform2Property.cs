/*
Copyright (c) 2024 Smash-ter <thesmashter@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Transform2Property : MonoBehaviour
{
	[Tooltip("Put materials that you would like to inherit from the transform.")]
	public List<Material> mats;	
	
	[Tooltip("If you don't want the transforms to inherit from the Game Object assigned to the script.")]
	public GameObject overrideObject;

	[Tooltip("Assign the root bone of your skinned mesh renderers to this component. If you want to control mesh renderers and make them uniform to the skin mesh, use this, otherwise you can use this to override the object's own matrix.")]
	public GameObject anchor;

	[Tooltip("For Shader Authors put the name of the property in here. The suffixes for the shader properties are debuggable below the script.")]
	[SerializeField]
	public string userPropertyName;

	//Bottom is for outputting to the editor script for debugging purposes.
	[NonSerialized]
	public Vector3 
		forwardValue,
		rightValue,
		upValue,
		backValue,
		leftValue,
		downValue,
		positionValue,
		rotationValue,
		scaleValue;
	[NonSerialized]
	public Matrix4x4 matrixValue;

	public string forwardName
	{
		get { return "_" + userPropertyName + "ForwardVector"; }
	}
	public string rightName
	{
		get { return "_" + userPropertyName + "RightVector"; }
	}
	public string upName
	{
		get { return "_" + userPropertyName + "UpVector"; }
	}
	public string backName
	{
		get { return "_" + userPropertyName + "BackVector"; }
	}
	public string leftName
	{
		get { return "_" + userPropertyName + "LeftVector"; }
	}
	public string downName
	{
		get { return "_" + userPropertyName + "DownVector"; }
	}
	public string positionName
	{
		get { return "_" + userPropertyName + "Position"; }
	}
	public string rotationName
	{
		get { return "_" + userPropertyName + "Rotation"; }
	}
	public string scaleName
	{
		get { return "_" + userPropertyName + "Scale"; }
	}
	public string matrixName
	{
		get { return "_" + userPropertyName + "Matrix"; }
	}


	void TransformMatProps()
	{
		Transform src;
		Transform dst;
		
		if (overrideObject != null)
		{
			src = overrideObject.transform;
		}
		else
		{
			src = this.transform;
		}
		
		if(anchor != null)
		{
			dst = anchor.transform;
			positionValue = dst.InverseTransformPoint(src.position);
			rotationValue = src.eulerAngles - dst.localEulerAngles;
			scaleValue = dst.InverseTransformVector(src.localScale);
		}
		else
		{
			dst = null;
			positionValue = src.localPosition;
			rotationValue = src.localEulerAngles;
			scaleValue = src.localScale;
		}
		
		//
		matrixValue = Matrix4x4.TRS(positionValue, Quaternion.Euler(rotationValue), scaleValue);

		forwardValue = src.forward;
		rightValue = src.right;
		upValue = src.up;
		//The use case is for people that may need an inverted vector for some SDF based shadowmaps.
		backValue = -src.forward;
		leftValue = -src.right;
		downValue = -src.up;

		if (userPropertyName == null)
		{
			Debug.LogWarning("Property needs to be named for it to work.");
			return;
		}

		if (mats.Count != 0)
		{
			for (int i = 0; i < mats.Count; i++)
			{
				if (mats[i] != null)
				{
					mats[i].SetVector(forwardName, forwardValue);
					mats[i].SetVector(rightName, rightValue);
					mats[i].SetVector(upName, upValue);
					mats[i].SetVector(backName, backValue);
					mats[i].SetVector(leftName, leftValue);
					mats[i].SetVector(downName, downValue);
					mats[i].SetVector(positionName, positionValue);
					mats[i].SetVector(rotationName, rotationValue);
					mats[i].SetVector(scaleName, scaleValue);
					mats[i].SetMatrix(matrixName, matrixValue);
				}
				else
				{
					return;
				}
			}
		}
		else if(mats.Count == 0)
		{
			return;
		}
		
	}
	void Update()
	{
		TransformMatProps();
	}
}
