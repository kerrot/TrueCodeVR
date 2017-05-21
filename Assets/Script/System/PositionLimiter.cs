using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PositionLimiter : MonoBehaviour {

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

    private Vector3 initPosition;
    private Vector3 tmpPosition;

    private void Awake()
    {
        tmpPosition = transform.localPosition;

        Transform tmpT = (transform.parent) ? transform.parent : transform;

        tmpPosition.x = X.positive;
        X.positive = tmpT.TransformPoint(tmpPosition).x;
        tmpPosition.x = X.negative;
        X.negative = tmpT.TransformPoint(tmpPosition).x;

        tmpPosition.y = Y.positive;
        Y.positive = tmpT.TransformPoint(tmpPosition).y;
        tmpPosition.y = Y.negative;
        Y.negative = tmpT.TransformPoint(tmpPosition).y;

        tmpPosition.z = Z.positive;
        Z.positive = tmpT.TransformPoint(tmpPosition).z;
        tmpPosition.z = Z.negative;
        Z.negative = tmpT.TransformPoint(tmpPosition).z;
    }

    private void Start()
    {
        this.LateUpdateAsObservable().Where(_ => enabled).Subscribe(_ =>
        {
            tmpPosition = transform.position;
            if (tmpPosition.x > X.positive)
            {
                tmpPosition.x = X.positive;
            }
            else if (tmpPosition.x < X.negative)
            {
                tmpPosition.x = X.negative;
            }

            if (tmpPosition.y > Y.positive)
            {
                tmpPosition.y = Y.positive;
            }
            else if (tmpPosition.y < Y.negative)
            {
                tmpPosition.y = Y.negative;
            }

            if (tmpPosition.z > Z.positive)
            {
                tmpPosition.z = Z.positive;
            }
            else if (tmpPosition.z < Z.negative)
            {
                tmpPosition.z = Z.negative;
            }

            transform.position = tmpPosition;
        });
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PositionLimiter))]
    public class PositionLimiterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PositionLimiter pl = target as PositionLimiter;

            if (GUILayout.Button("Get Current position"))
            {
                pl.X.positive = pl.transform.localPosition.x;
                pl.X.negative = pl.transform.localPosition.x;

                pl.Y.positive = pl.transform.localPosition.y;
                pl.Y.negative = pl.transform.localPosition.y;

                pl.Z.positive = pl.transform.localPosition.z;
                pl.Z.negative = pl.transform.localPosition.z;
            }

            EditorGUILayout.LabelField("X(Max/Min)");
            EditorGUILayout.BeginHorizontal();
            pl.X.positive = EditorGUILayout.FloatField(pl.X.positive, GUILayout.Width(100));
            pl.X.negative = EditorGUILayout.FloatField(pl.X.negative, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Y(Max/Min)");
            EditorGUILayout.BeginHorizontal();
            pl.Y.positive = EditorGUILayout.FloatField(pl.Y.positive, GUILayout.Width(100));
            pl.Y.negative = EditorGUILayout.FloatField(pl.Y.negative, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Z(Max/Min)");
            EditorGUILayout.BeginHorizontal();
            pl.Z.positive = EditorGUILayout.FloatField(pl.Z.positive, GUILayout.Width(100));
            pl.Z.negative = EditorGUILayout.FloatField(pl.Z.negative, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
}

