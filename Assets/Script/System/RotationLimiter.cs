using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RotationLimiter : MonoBehaviour {

    [Serializable]
    public struct LimitValue
    {
        public float positive;
        public float negative;
    }

    [SerializeField]
    private LimitValue X;
    [SerializeField]
    private LimitValue Y;
    [SerializeField]
    private LimitValue Z;

    private Vector3 initRotation;
    private Vector3 tmpRotation;

    private void Awake()
    {
        tmpRotation = transform.localRotation.eulerAngles;

        Quaternion tmpQ = (transform.parent) ? transform.parent.rotation : transform.rotation;

        tmpRotation.x = X.positive;
        X.positive = (tmpQ * Quaternion.Euler(tmpRotation)).eulerAngles.x;
        tmpRotation.x = X.negative;
        X.negative = (tmpQ * Quaternion.Euler(tmpRotation)).eulerAngles.x;

        tmpRotation.y = Y.positive;
        Y.positive = (tmpQ * Quaternion.Euler(tmpRotation)).eulerAngles.y;
        tmpRotation.y = Y.negative;
        Y.negative = (tmpQ * Quaternion.Euler(tmpRotation)).eulerAngles.y;

        tmpRotation.z = Z.positive;
        Z.positive = (tmpQ * Quaternion.Euler(tmpRotation)).eulerAngles.z;
        tmpRotation.z = Z.negative;
        Z.negative = (tmpQ * Quaternion.Euler(tmpRotation)).eulerAngles.z;
    }

    private void Start()
    {
        this.LateUpdateAsObservable().Where(_ => enabled).Subscribe(_ =>
        {
            tmpRotation = transform.rotation.eulerAngles;
            if (tmpRotation.x > X.positive)
            {
                tmpRotation.x = X.positive;
            }
            else if (tmpRotation.x < X.negative)
            {
                tmpRotation.x = X.negative;
            }

            if (tmpRotation.y > Y.positive)
            {
                tmpRotation.y = Y.positive;
            }
            else if (tmpRotation.y < Y.negative)
            {
                tmpRotation.y = Y.negative;
            }

            if (tmpRotation.z > Z.positive)
            {
                tmpRotation.z = Z.positive;
            }
            else if (tmpRotation.z < Z.negative)
            {
                tmpRotation.z = Z.negative;
            }

            transform.rotation = Quaternion.Euler(tmpRotation);
        });
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RotationLimiter))]
    public class RotationLimiterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RotationLimiter rl = target as RotationLimiter;

            if (GUILayout.Button("Get Current rotation"))
            {
                rl.X.positive = rl.transform.localRotation.eulerAngles.x;
                rl.X.negative = rl.transform.localRotation.eulerAngles.x;

                rl.Y.positive = rl.transform.localRotation.eulerAngles.y;
                rl.Y.negative = rl.transform.localRotation.eulerAngles.y;

                rl.Z.positive = rl.transform.localRotation.eulerAngles.z;
                rl.Z.negative = rl.transform.localRotation.eulerAngles.z;
            }

            EditorGUILayout.LabelField("X(Max/Min)");
            EditorGUILayout.BeginHorizontal();
            rl.X.positive = EditorGUILayout.FloatField(rl.X.positive, GUILayout.Width(100));
            rl.X.negative = EditorGUILayout.FloatField(rl.X.negative, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Y(Max/Min)");
            EditorGUILayout.BeginHorizontal();
            rl.Y.positive = EditorGUILayout.FloatField(rl.Y.positive, GUILayout.Width(100));
            rl.Y.negative = EditorGUILayout.FloatField(rl.Y.negative, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Z(Max/Min)");
            EditorGUILayout.BeginHorizontal();
            rl.Z.positive = EditorGUILayout.FloatField(rl.Z.positive, GUILayout.Width(100));
            rl.Z.negative = EditorGUILayout.FloatField(rl.Z.negative, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
}

