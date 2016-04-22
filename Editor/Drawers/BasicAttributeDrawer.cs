﻿using UnityEditor;
using UnityEngine;

namespace AbilitySystem {

    [CustomPropertyDrawer(typeof(BasicAttribute), true)]
    public class ModifiableAttributeDrawer : PropertyDrawer {

        private string[] attributeFormulaOptions;
        private SerializedProperty property;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var p = new Rect(position);
            var rect = new EditorRect(p);

            var prop = new ImprovedSerializedProperty(property);
            var pointer = prop.Property("serializedMethodPointer");
            var modifiers = prop.Property("serializedModifiers");

            int modifierCount = property.FindPropertyRelative("serializedModifiers").arraySize;
            if(modifierCount == 0) {
               // EditorGUI.Foldout(rect.Width(EditorGUIUtility.labelWidth), true, label.text + "(" + modifierCount + ")");
            }
            EditorGUI.LabelField(rect.Width(EditorGUIUtility.labelWidth), label.text + " (" + modifierCount + ")");

            var options = GetOptions(property);
            int currentIndex = DrawerUtil.GetMatchingIndex(pointer.Get<string>("signature"), attributeFormulaOptions);

            int idx = EditorGUI.Popup(rect.WidthMinus(50), currentIndex, options);
            if (currentIndex != idx) {
                if (idx == 0) {
                    pointer.Set("signature", "");
                }
                else {
                    pointer.Set("signature", attributeFormulaOptions[idx]);
                }
            }

            prop.Set("baseValue", EditorGUI.FloatField(rect, prop.Get<float>("baseValue")));

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }

        private string[] GetOptions(SerializedProperty inputProperty) {
            if (attributeFormulaOptions != null) return attributeFormulaOptions;
            attributeFormulaOptions = DrawerUtil.GetFloatFormulaOptions<FormulaAttribute>(inputProperty);
            return attributeFormulaOptions;
        }

    }
}