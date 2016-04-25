using System;
using UnityEditor;
using UnityEngine;

namespace UnityXD.Components
{
    [InitializeOnLoad]
    [Serializable]
    public class UIComponent : MonoBehaviour
    {
        public string Text;
        public bool IsVisible;
        public event InvalidateCompleteHandler InvalidateComplete;
        public event InvalidateCompleteHandler CreationComplete;
        public delegate void CreationCompleteHandler(EventArgs e);
        public delegate void InvalidateCompleteHandler(EventArgs e);

        #region XD Invalidation Lifecyle
        /// <summary>
        /// The purpose of this function is to set the size and position for all child 
        /// components that comprise the component.
        /// </summary>
        public virtual void InvalidateDisplay()
        {
            CreateChildren();
            InvalidateProperties();
            InvalidateMeasure();
            LayoutChrome();
            OnInvalidateComplete();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void InvalidateProperties()
        {
            CommitProperties();
        }

        public virtual void InvalidateMeasure()
        {
            Measure();
        }
        #endregion

        #region XD Lifecycle

        /// <summary>
        /// Create child objects of the component.
        /// </summary>
        protected virtual void CreateChildren()
        {
            // Now Invoke Children Created 
            ChildrenCreated();
        }

        /// <summary>
        /// The purpose of this function is to commit any changes to properties and to synchronize changes so they occur at the same time or in a specific order.
        /// </summary>
        protected virtual void CommitProperties()
        {
        }

        /// <summary>
        /// The purpose of this function is to set a default width and height for your custom component. 
        /// </summary>
        protected virtual void Measure()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void LayoutChrome()
        {

        }

        #endregion

        /// <summary>
        /// Performs any final processing after child objects are created.
        /// </summary>
        protected virtual void ChildrenCreated()
        {

        }

        protected virtual void OnInvalidateComplete()
        {
            var handler = InvalidateComplete;
            if (handler != null)
                handler(EventArgs.Empty);
        }

        protected virtual void OnCreationComplete()
        {
            var handler = CreationComplete;
            if (handler != null)
                handler(EventArgs.Empty);
        }
    }

    public class Label : MonoBehaviour
    {

    }
}
