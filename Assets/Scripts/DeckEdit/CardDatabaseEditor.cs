#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardDatabase))]
public class CardDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("���ݿ����", EditorStyles.boldLabel);

        if (GUILayout.Button("����Ŀɨ�迨��"))
        {
            CardDataCollector.Initialize();
            ((CardDatabase)target).BuildFromCollector();
            EditorUtility.SetDirty(target);
        }

        EditorGUILayout.HelpBox(
            "�����ťɨ����Ŀ�е����п��Ʋ�������ݿ�.\n" +
            "����Ӧ�ð������ʹ洢��Resources�ļ����µ����ļ�����.",
            MessageType.Info);
    }
}
#endif