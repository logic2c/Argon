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
        EditorGUILayout.LabelField("数据库操作", EditorStyles.boldLabel);

        if (GUILayout.Button("从项目扫描卡牌"))
        {
            CardDataCollector.Initialize();
            ((CardDatabase)target).BuildFromCollector();
            EditorUtility.SetDirty(target);
        }

        EditorGUILayout.HelpBox(
            "点击按钮扫描项目中的所有卡牌并填充数据库.\n" +
            "卡牌应该按照类型存储在Resources文件夹下的子文件夹中.",
            MessageType.Info);
    }
}
#endif