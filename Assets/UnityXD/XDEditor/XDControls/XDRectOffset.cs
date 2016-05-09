using System;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEngine;
using System.Linq.Expressions;
using System.Reflection;
using Assets.UnityXD.Core;

namespace Assets.UnityXD.XDEditor.XDControls
{
    public interface IXDRectOffset : IXDWidget
    {
        IXDRectOffset Field(Expression<Func<RectOffset>>  currentOffset, BaseControl control);
    }

    internal class XDRectOffset : XDWidget, IXDRectOffset
    {
        private Action<RectOffset> actionRectOffset;
        private RectOffset CurrentOffset;
        private Expression<Func<RectOffset>> rectPointer;
        private BaseControl baseControl;

        internal XDRectOffset(IXDLayout parent) : base(parent)
        {
            
        }


        public IXDRectOffset Field( Expression<Func<RectOffset>>  currentOffset, BaseControl control)
        {
//            actionRectOffset = new Action<RectOffset>((i) => { 
//                var me =((MemberExpression)currentOffset.Body);
//                var ce = (ConstantExpression)me.Expression;
//                var fieldInfo = ce.Value.GetType().GetField(me.Member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//                var value = (RectOffset)fieldInfo.GetValue(ce.Value);
//                fieldInfo.SetValue(value,i);
//                //Debug.LogFormat("Val: {0}", value);
//            });
            baseControl = control;
            rectPointer = currentOffset;
            //CurrentOffset = currentOffset;
            return this;
        }

        delegate void Foo(string s);

        public override void Render()
        {
            var pStyle = new GUIStyle();
            pStyle.normal.background = CreateColoredTexture( Color.red );
            using (new GUILayout.HorizontalScope(pStyle ))
            {
                var gui = new XDGUI();
                var rect = new RectOffset(50, 50, 50, 50);
                gui.Render();
                var property = rectPointer.Body as MemberExpression;
                var prop = (PropertyInfo)((MemberExpression)rectPointer.Body).Member;
                prop.SetValue(baseControl, rect, null);

            }
        }

    }

   
}

