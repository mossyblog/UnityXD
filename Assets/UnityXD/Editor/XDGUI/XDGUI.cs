using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.UI;
using UnityXD.Styles;
using UnityEditor.Sprites;
using UnityXD.Components;
using System.Collections.Generic;
using System.Linq;

namespace UnityXD.XDGUIEditor

{
    public  class XDGUI
    {
        private String CurrentLabel;
        private Sprite CurrentSprite;
        private GUIContent CurrentContent = new GUIContent();
        private GUIStyle CurrentStyle;
        private GUIStyle FieldStyle;
        private int Width;
        private int Height;
        private UIComponent _componentRef;
        private List<GUILayoutOption> CurrentOptions = new List<GUILayoutOption>();
        private List<GUILayoutOption> FieldOptions = new List<GUILayoutOption>();
        private int FieldWidth;
        private int FieldHeight;

        #region Constructors

        public XDGUI()
        {
            CurrentStyle = new GUIStyle(XDGUIStyles.Instance.Label);
            FieldStyle = new GUIStyle(XDGUIStyles.Instance.Field);
            CurrentOptions.Add(GUILayout.MinHeight(22));
            FieldOptions.Add(GUILayout.MinHeight(22));
        }

        public XDGUI(ref UIComponent componentRef)
        {
            _componentRef = componentRef;
            CurrentStyle = new GUIStyle(XDGUIStyles.Instance.Label);
            FieldStyle = new GUIStyle(XDGUIStyles.Instance.Field);
            CurrentOptions.Add(GUILayout.MinHeight(22));
            FieldOptions.Add(GUILayout.MinHeight(22));
        }

        public static XDGUI Create()
        {            
            return new XDGUI();
        }

        public static XDGUI Create(ref UIComponent componentRef)
        {
            return new XDGUI(ref componentRef);
        }

        #endregion

        #region Core Shared Methods

        public XDGUI Options(GUILayoutOption[] options)
        {
            CurrentOptions = options.ToList();
            return this;
        }

        public XDGUI Sprite(Sprite sprite)
        {
            CurrentContent.image = SpriteUtility.GetSpriteTexture(sprite, false);
            return this;
        }

        public XDGUI Label(string label)
        {
            CurrentLabel = label;
            CurrentContent.text = CurrentLabel;
            return this;
        }

        public XDGUI Size(int size)
        {            
            return Size(size, size);
        }

        public XDGUI Size(int width, int height)
        {            
            return Size(width, height, 0, 0);
        }

        public XDGUI Size(int width, int height, int fieldWidth) {           
            return  Size(width, height, fieldWidth, height);
        }

        public XDGUI Size(int width, int height, int fieldWidth, int fieldHeight) {
            Width = width;
            Height = height;         
            FieldWidth = fieldWidth;
            FieldHeight = fieldHeight;

            CurrentOptions.Add(GUILayout.Width(width));
            CurrentOptions.Add(GUILayout.Height(height));

            if (fieldWidth > 0 && fieldHeight > 0)
            {
                FieldOptions.Add(GUILayout.Width(fieldWidth));
                FieldOptions.Add(GUILayout.Height(fieldHeight));
            }
            else
            {
                FieldOptions.Add(GUILayout.Height(height));
            }

            return this;
        }

        public XDGUI Style(GUIStyle style)
        {
            CurrentStyle = style;           
            return this;
        }

        public XDGUI Style(GUIStyle style, GUIStyle fieldStyle) {
            FieldStyle = fieldStyle;
            CurrentStyle = style;
            return this;
        }

        #endregion

        #region Button

        public  bool RenderButton()
        {          
            return GUILayout.Button(CurrentContent, CurrentStyle, CurrentOptions.ToArray());
        }

        public  bool RenderButton(ref bool field, bool isEnabled = true)
        {
            GUI.enabled = isEnabled;
            return GUILayout.Button(CurrentContent, CurrentStyle, CurrentOptions.ToArray()) ? field = !field : field;
        }

        #endregion

        #region ToolBar - Alignment

        public bool RenderAnchorToolBar()
        {   
            var m_horizAlignment = _componentRef.CurrentAnchorAlignment.ToHorizontalAlignment();
            var m_vertAlignment = _componentRef.CurrentAnchorAlignment.ToVerticalAlignment();

            var m_isAnchorRightEnabled = XDHorizontalAlignment.Right == m_horizAlignment;
            var m_isAnchorLeftEnabled = XDHorizontalAlignment.Left == m_horizAlignment;
            var m_isAnchorMiddleHEnabled = XDHorizontalAlignment.Center == m_horizAlignment;
            var m_isAnchorTopEnabled = XDVerticalAlignment.Top == m_vertAlignment;
            var m_isAnchorMiddleVEnabled = XDVerticalAlignment.Center == m_vertAlignment;
            var m_isAnchorBottomEnabled = XDVerticalAlignment.Bottom == m_vertAlignment;
            var m_isAnchorHStretched = _componentRef.IsHorizontalStretchEnabled;
            var m_isAnchorVStretched = _componentRef.IsVeritcalStretchEnabled;

            var path = "icons/Editor/";
            var horizPrefix = "AlignHoriz";
            var vertPrefix = "AlignVert";
            var onState = "On";
            var offState = "Off";

            var leftName = "Left_" + (m_isAnchorLeftEnabled ? onState : offState);
            var rightName = "Right_" + (m_isAnchorRightEnabled ? onState : offState);
            var topName = "Top_" + (m_isAnchorTopEnabled ? onState : offState);
            var botName = "Bottom_" + (m_isAnchorBottomEnabled ? onState : offState);
            var midHName = "Middle_" + (m_isAnchorMiddleHEnabled ? onState : offState);
            var midVName = "Middle_" + (m_isAnchorMiddleVEnabled ? onState : offState);

            var leftSprite = Resources.Load<Sprite>(path + horizPrefix + leftName);
            var midHSprite = Resources.Load<Sprite>(path + horizPrefix + midHName);
            var rightSprite = Resources.Load<Sprite>(path + horizPrefix + rightName);

            var topSprite = Resources.Load<Sprite>(path + vertPrefix + topName);
            var midVSprite = Resources.Load<Sprite>(path + vertPrefix + midVName);
            var botSprite = Resources.Load<Sprite>(path + vertPrefix + botName);

            var horizStretchSprite = Resources.Load<Sprite>(path + "StretchHoriz_" + (m_isAnchorHStretched ? onState : offState));
            var vertStretchSprite = Resources.Load<Sprite>(path + "StretchVert_" + (m_isAnchorVStretched ? onState : offState));
            var size = 24;
            var style = new GUIStyle(GUIStyle.none);
            style.normal.background = XDGUIUtility.CreateColoredTexture(Color.white);
            style.alignment = TextAnchor.MiddleCenter;
            style.margin = new RectOffset(3, 3, 3, 3);

            using (new XDGUIPanel(true, style, GUILayout.MaxWidth(256)))
            {

                XDGUI.Create().Sprite(horizStretchSprite).Size(size).Style(style).RenderButton(ref m_isAnchorHStretched);
                XDGUI.Create().Sprite(vertStretchSprite).Size(size).Style(style).RenderButton(ref m_isAnchorVStretched);

                Spacer(8);

                if (XDGUI.Create().Sprite(leftSprite).Size(size).Style(style).RenderButton())
                    m_horizAlignment = XDHorizontalAlignment.Left;

                if (XDGUI.Create().Sprite(midHSprite).Size(size).Style(style).RenderButton())
                    m_horizAlignment = XDHorizontalAlignment.Center;

                if (XDGUI.Create().Sprite(rightSprite).Size(size).Style(style).RenderButton())
                    m_horizAlignment = XDHorizontalAlignment.Right;

                Spacer(8);

                if (XDGUI.Create().Sprite(topSprite).Size(size).Style(style).RenderButton())
                    m_vertAlignment = XDVerticalAlignment.Top;

                if (XDGUI.Create().Sprite(midVSprite).Size(size).Style(style).RenderButton())
                    m_vertAlignment = XDVerticalAlignment.Center;

                if (XDGUI.Create().Sprite(botSprite).Size(size).Style(style).RenderButton())
                    m_vertAlignment = XDVerticalAlignment.Bottom;
            }
            if (GUI.changed)
            {
                var align = XDThemeUtility.ToAlignment(m_horizAlignment, m_vertAlignment);
                _componentRef.Dock(align, m_isAnchorHStretched, m_isAnchorVStretched);
            }

            return true;
        }

        #endregion

        #region Spacer

        public XDGUI Spacer(int amt = 0)
        {
            GUILayout.Space(amt);
            return this;
        }

        #endregion

        public void RenderLabel()
        {
            GUILayout.Label(CurrentContent, CurrentStyle, CurrentOptions.ToArray());
        }

        #region TextFields
        public void RenderTextField(ref double field, bool isSingleLine, bool isEnabled = true)
        {
            GUI.enabled = isEnabled;
            using (new XDGUIPanel(isSingleLine) )
            {
                if (isSingleLine)
                {
                    RenderLabel();
                }
                else
                {
                    FieldStyle.alignment = TextAnchor.MiddleCenter;
                }

                field = EditorGUILayout.DoubleField(field, FieldStyle, FieldOptions.ToArray());
                RenderLine(XDVerticalAlignment.Bottom,0,XDColors.ChromeLight.ToColor());

                if (!isSingleLine)
                {
                    CurrentStyle.alignment = TextAnchor.MiddleCenter;
                    RenderLabel();
                }
            }
            GUI.enabled = true;
        }

        public void RenderTextField(ref string field, bool isSingleLine, bool isEnabled = true)
        {            
            GUI.enabled = isEnabled;
            using (new XDGUIPanel(isSingleLine) )
            {
                if (isSingleLine)
                {
                    RenderLabel();
                }
                else
                {
                    FieldStyle.alignment = TextAnchor.MiddleCenter;
                }

                field = EditorGUILayout.TextField(field, FieldStyle, FieldOptions.ToArray());
                RenderLine(XDVerticalAlignment.Bottom,0,XDColors.ChromeLight.ToColor());

                if (!isSingleLine)
                {
                    CurrentStyle.alignment = TextAnchor.MiddleCenter;
                    RenderLabel();
                }
            }
            GUI.enabled = true;
        }

        public void RenderTextField(ref int field, bool isSingleLine, bool isEnabled = true)
        {
            GUI.enabled = isEnabled;           
            using (new XDGUIPanel(isSingleLine) )
            {
                if (isSingleLine)
                {
                    RenderLabel();
                }
                else
                {
                    FieldStyle.alignment = TextAnchor.MiddleCenter;
                }

                field = EditorGUILayout.IntField(field, FieldStyle, FieldOptions.ToArray());
                RenderLine(XDVerticalAlignment.Bottom,0,XDColors.ChromeLight.ToColor());
                if (!isSingleLine)
                {
                    CurrentStyle.alignment = TextAnchor.MiddleCenter;
                    RenderLabel();
                }

            }
            GUI.enabled = true;
        }

        public void RenderTextField(ref float field, bool isSingleLine, bool isEnabled = true)
        {
            GUI.enabled = isEnabled;
            using (new XDGUIPanel(isSingleLine) )
            {
                if (isSingleLine)
                {
                    RenderLabel();
                }
                else
                {
                    FieldStyle.alignment = TextAnchor.MiddleCenter;
                }

                field = EditorGUILayout.FloatField(field, FieldStyle, FieldOptions.ToArray());
                RenderLine(XDVerticalAlignment.Bottom,0,XDColors.ChromeLight.ToColor());

                if (!isSingleLine)
                {
                    CurrentStyle.alignment = TextAnchor.MiddleCenter;
                    RenderLabel();
                }
            }

            GUI.enabled = true;
        }

        #endregion

        public void RenderLine(XDVerticalAlignment alignment, int offset, Color color) {
            var rect = GUILayoutUtility.GetLastRect();
            rect.width -= offset;
            rect.x += offset/2;
            XDGUIUtility.DrawLine(rect, 1, color, alignment);
        }


        public void RenderSwatchPicker(ref UIComponent componentRef, bool fillEnabled, bool backFillEnabled) {
            var groupStyle = XDGUIStyles.Instance.Panel;

            // Chrome Row
            var ChromeColors = Enum.GetNames (typeof(XDColors))
                .OrderBy (x => x)
                .Where (s => s.ToLower ().Contains (XDColorTypes.Chrome.ToString ().ToLower ()))
                .ToList ();

            var BrandColors = Enum.GetNames (typeof(XDColors))
                .OrderBy (x => x)
                .Where (s => s.ToLower ().Contains (XDColorTypes.Brand.ToString ().ToLower ()))
                .ToList ();

            var AccentColors = Enum.GetNames (typeof(XDColors))
                .OrderBy (x => x)
                .Where (s => s.ToLower ().Contains (XDColorTypes.Accent.ToString ().ToLower ()))
                .ToList ();


            if (fillEnabled) {
                XDGUIUtility.CreateSpacer (8);
                XDGUI.Create().Label("Fill Color").Style(XDGUIStyles.Instance.Heading).RenderLabel();
                using (new XDGUIPanel (true, groupStyle)) {
                    // Preview Swatch.
                    XDGUIUtility.CreateSwatch (componentRef.CurrentStyle.FrontFill.ToColor (), 48, true);
                    XDGUIUtility.CreateSpacer (16);
                    using (new XDGUIPanel (false)) {
                        XDGUIUtility.CreateSwatchRow (AccentColors.ToArray (), 16, ref componentRef.CurrentStyle.FrontFill);
                        XDGUIUtility.CreateSwatchRow (BrandColors.ToArray (), 16, ref componentRef.CurrentStyle.FrontFill);
                        XDGUIUtility.CreateSwatchRow (ChromeColors.ToArray (), 16, ref componentRef.CurrentStyle.FrontFill);
                    }

                }

                XDGUIUtility.CreateSpacer (16);
            }

            if (backFillEnabled) {
                XDGUI.Create().Label("BackFill Color").Style(XDGUIStyles.Instance.Heading).RenderLabel();
                using (new XDGUIPanel (true, groupStyle)) {
                    // Preview Swatch.
                    XDGUIUtility.CreateSwatch (componentRef.CurrentStyle.FrontFill.ToColor (), 48, true);
                    XDGUIUtility.CreateSpacer (16);
                    using (new XDGUIPanel (false)) {
                        XDGUIUtility.CreateSwatchRow (AccentColors.ToArray (), 16, ref componentRef.CurrentStyle.FrontFill);
                        XDGUIUtility.CreateSwatchRow (BrandColors.ToArray (), 16, ref componentRef.CurrentStyle.FrontFill);
                        XDGUIUtility.CreateSwatchRow (ChromeColors.ToArray (), 16, ref componentRef.CurrentStyle.FrontFill);
                    }

                }
            }

            

        }

        public void RenderEnumField<T>(ref T field, List<String> filters, bool isSingleLine, bool isEnabled = true) {
            var rawValues = new Dictionary<Type, int[]>();
            var names = Enum.GetNames(typeof(T));
            var values = GetEnumValues<T>(rawValues);

            if (filters != null)
            {
                names = filters.ToArray();
                var valList = new List<int>();
                foreach (var name in names)
                {
                    valList.Add((int)Enum.Parse(typeof(T), name));
                }
                values = valList.ToArray();
            }


            var result = 0;
            GUI.enabled = isEnabled;           
            using (new XDGUIPanel(isSingleLine) )
            {
                if (isSingleLine)
                {
                    RenderLabel();
                }
                else
                {
                    FieldStyle.alignment = TextAnchor.MiddleCenter;
                }
                var selected = (int)(object)field;

                var selectedIndex = EditorGUILayout.Popup(Array.IndexOf(values, selected), names,FieldStyle, FieldOptions.ToArray());
                RenderOutline(XDColors.ChromeLight.ToColor());
                result = selectedIndex >= 0 && selectedIndex < values.Length ? values[selectedIndex] : selected;

                if (!isSingleLine)
                {
                    CurrentStyle.alignment = TextAnchor.MiddleCenter;
                    RenderLabel();
                }

            }
            GUI.enabled = true;


          
            field = (T)(object)result;

         
        }

        private void RenderOutline(Color color) {
            var pos = GUILayoutUtility.GetLastRect();

            var leftRect = pos;
            leftRect.width = 1;

            var rightRect = leftRect;
            rightRect.x += pos.width;

            var topRect = pos;
            topRect.height = 1;

            var botRect = topRect;
            botRect.y += pos.height;

            EditorGUI.DrawRect(leftRect, color);
            EditorGUI.DrawRect(rightRect, color);
            EditorGUI.DrawRect(topRect, color);
            EditorGUI.DrawRect(botRect, color);

        }


        public void RenderCheckBox(ref bool field, bool isEnabled = true) {
            var style = new GUIStyle(XDGUIStyles.Instance.Checkbox);
            var fillEnabled = style.active.background.GetPixel(1, 1);
            var fillDisabled = style.normal.background.GetPixel(1, 1);

            var trueColor = XDColors.Brand.ToColor();
            var falseColor = XDColors.ChromeLightest.ToColor();

            style.normal.background = XDGUIUtility.CreateColoredTexture(field ? trueColor : falseColor);

            using (new XDGUIPanel(true))
            {
                RenderLabel();
                using (var checkBoxRec = new XDGUIPanel(true, GUILayout.Width(FieldWidth), GUILayout.Height(FieldHeight)))
                {
                    GUILayout.FlexibleSpace();
                    var fieldRect = checkBoxRec.Rect;

                    fieldRect.y += Height/2 - FieldHeight/2 + 2;
                    XDGUIUtility.DrawRect(fieldRect, Color.black, (field ? trueColor : falseColor), style.border);
                    if (GUI.Button(fieldRect, String.Empty, GUIStyle.none))
                    {
                        field = !field;
                    }
                }

            }


           


        }

        private static int[] GetEnumValues<T>(Dictionary<Type, int[]> enumcache)
        {
            if (!enumcache.ContainsKey(typeof(T)))
                enumcache[typeof(T)] = Enum.GetValues(typeof(T)).Cast<int>().ToArray();
            return enumcache[typeof(T)];
        }
    }

   

}
