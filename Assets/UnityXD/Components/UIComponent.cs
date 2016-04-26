using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityXD.Styles;

namespace UnityXD.Components
{
    [Serializable]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasRenderer))]
    public class UIComponent : MonoBehaviour
    {
        [SerializeField] public SpriteAlignment CurrentAnchorAlignment;
        [SerializeField] public SpriteAlignment CurrentPivotAlignment;
		public bool IsChildReadOnly;
        public int Width;
        public int Height;
        public int X;
        public int Y;
        public bool IsHeightDependantOnWidth;
        public bool IsVisible;
        public RectOffset Padding;
        public RectOffset Margin;
        public RectOffset ParentPadding;
        public bool IgnoreParentPadding;
        public XDStyle CurrentStyle = new XDStyle();
        public event InvalidateCompleteHandler InvalidateComplete;
        public event InvalidateCompleteHandler CreationComplete;
        public delegate void CreationCompleteHandler(EventArgs e);
        public delegate void InvalidateCompleteHandler(EventArgs e);

        [NonSerialized]
        private RectTransform _rectTransformRef;

        [NonSerialized]
        private Image _imageRef;

        [NonSerialized]
        private Text _textRef;

        /// <summary>
        /// Awake thee and strike forth!!
        /// </summary>
        public void Awake()
        {
            if (Padding == null)
            {
                Padding = new RectOffset(0, 0, 0, 0);
                Margin = new RectOffset(0, 0, 0, 0);
                ParentPadding = new RectOffset(0, 0, 0, 0);
            }
        }

		/// <summary>
		/// Start this instance.
		/// </summary>
		public virtual void Start()
        {
           InvalidateDisplay();
        }

		/// <summary>
		/// Update this instance.
		/// </summary>
		public virtual void Update() {
			ValidateHeirachy ();
		}

		#region XD Internal References.
		/// <summary>
		///     Gets the rect transform reference & caches it.
		/// </summary>
		/// <value>The rect transform reference.</value>
		public RectTransform RectTransformRef
		{
			get { return _rectTransformRef ?? (_rectTransformRef = GetComponent<RectTransform>()); }
		}

		/// <summary>
		///     Gets the rect transform reference & caches it.
		/// </summary>
		/// <value>The rect transform reference.</value>
		public Image ImageRef
		{
			get { return _imageRef ?? (_imageRef = GetComponent<Image>()); }
		}

		/// <summary>
		/// Gets the text reference.
		/// </summary>
		/// <value>The text reference.</value>
		public Text TextRef
		{
			get { return _textRef ?? (_textRef = GetComponent<Text>()); }
		}
		#endregion

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
            UpdateLayout();
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

			//ValidateHeirachy ();
            // Now Invoke Children Created             
			OnChildrenCreated();

        }


		/// <summary>
		/// Validates the heirachy (ensures things are where they are supposed to be).
		/// </summary>
		protected virtual void ValidateHeirachy() {
			
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
		/// Updates the layout once all measurements, children and properties have finalized.
		/// </summary>
        protected virtual void UpdateLayout()
        {
            SetPosition(X, Y);

        }

        #endregion

        #region XD Events
        /// <summary>
        /// Performs any final processing after child objects are created.
        /// </summary>
        protected virtual void OnChildrenCreated()
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
        #endregion

        /// <summary>
		///     Gets a value indicating whether this instance is strech enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is strech enabled; otherwise, <c>false</c>.</value>
		public bool IsStrechEnabled
        {
            get { return IsHorizontalStretchEnabled || IsVeritcalStretchEnabled; }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is horizontal stretch enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is horizontal stretch enabled; otherwise, <c>false</c>.</value>
        public bool IsHorizontalStretchEnabled
        {
            get
            {
                var min = RectTransformRef.anchorMin;
                var max = RectTransformRef.anchorMax;
                return !min.Equals(max) && min.x == 0 && max.x == 1;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is veritcal stretch enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is veritcal stretch enabled; otherwise, <c>false</c>.</value>
        public bool IsVeritcalStretchEnabled
        {
            get
            {
                var min = RectTransformRef.anchorMin;
                var max = RectTransformRef.anchorMax;
                return !min.Equals(max) && min.y == 0 && max.y == 1;
            }
        }

        /// <summary>
		/// Adjusts the pivot.
		/// </summary>
		/// <param name="newPos">New position.</param>
		public void SetPivot(SpriteAlignment newPos)
        {
            RectTransformRef.pivot = newPos.ToVector();
        }

        /// <summary>
		/// Sets the size.
		/// </summary>
		/// <param name="w">The width.</param>
		/// <param name="h">The height.</param>
		public virtual void SetSize(int w, int h)
        {
            if (IsHeightDependantOnWidth)
            {
                h = w;
            }

            if (CurrentStyle.Size != XDSizes.Custom)
            {
                w = h = (int)CurrentStyle.Size;
                IsHeightDependantOnWidth = true;
            }

            if (!IsHorizontalStretchEnabled)
                RectTransformRef.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);

            if (!IsVeritcalStretchEnabled)
                RectTransformRef.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);

            Width = w;
            Height = h;
        }

        /// <summary>
		/// Sets the position.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;

            var min = RectTransformRef.offsetMin;
            var max = RectTransformRef.offsetMax;
            var apos = RectTransformRef.anchoredPosition;

            var oMaxX = 0;
            var oMaxY = 0;
            var oMinX = 0;
            var oMinY = 0;


            if (!IgnoreParentPadding)
            {
                oMaxX = -ParentPadding.right;
                oMinX += ParentPadding.left;
                oMinY += ParentPadding.bottom;
                oMaxY += -ParentPadding.top;
            }

            max.x = -Margin.right + oMaxX;
            min.x = Margin.left + x + oMinX;
            min.y = Margin.bottom + oMinY;
            max.y = -Margin.top + -y + oMaxY;

            // CENTER IS A BIT OF PROBLEM CHILD..
            if (CurrentAnchorAlignment == SpriteAlignment.Center)
            {

                if (IsHorizontalStretchEnabled)
                {
                    max.x = (-Margin.right + oMaxX) + X * 2;
                    min.x = Margin.left + oMinX;
                }
                else {
                    max.x = 0;
                    min.x = X * 2;
                }

                if (IsVeritcalStretchEnabled)
                {
                    max.y = (-Margin.top + oMaxY) + -Y * 2;
                    min.y = Margin.bottom + oMinY;
                }
                else {
                    min.y = 0;
                    max.y = -Y * 2;
                }
            }

            RectTransformRef.offsetMin = min;
            RectTransformRef.offsetMax = max;
            SetSize(Width, Height);
        }

        /// <summary>
        ///     Docks the instance to the nominated Alignment Position.
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="horizStretch"></param>
        /// <param name="vertStretch"></param>
        public void Dock(SpriteAlignment alignment, bool horizStretch, bool vertStretch)
        {


            var min = RectTransformRef.anchorMin;
            var max = RectTransformRef.anchorMax;
            var oMin = RectTransformRef.offsetMin;
            var oMax = RectTransformRef.offsetMax;

            // Only manipulate the Min/Max anchor settings if they aren't Custom.
            if (alignment != SpriteAlignment.Custom)
            {
                min = alignment.ToVector();
                max = alignment.ToVector();
                CurrentAnchorAlignment = alignment;
                CurrentPivotAlignment = alignment;
            }

            // If Vertical Stretch is Enabled adjust the min/max values on Y vector.
            if (vertStretch)
            {
                min.y = 0;
                max.y = 1;
                oMin.y = oMax.y = 0;
            }

            // If Vertical Stretch is Enabled adjust the min/max values on X vector.
            if (horizStretch)
            {
                min.x = 0;
                max.x = 1;
                oMin.x = oMax.x = 0;
            }

            RectTransformRef.offsetMin = oMin;
            RectTransformRef.offsetMax = oMax;
            RectTransformRef.anchorMax = max;
            RectTransformRef.anchorMin = min;
            RectTransformRef.pivot = min;

            InvalidateDisplay();
        }

        /// <summary>
        /// Sets the padding.
        /// </summary>
        /// <param name="padding">Padding.</param>
        public void SetPadding(RectOffset padding)
        {
            Padding = padding;
            var children = GetComponentsInChildren<UIComponent>();

            // Tell the Children of the new padding.
            foreach (var child in children)
            {
                if (child != this)
                {
                    child.SetParentPadding(Padding);
                }
            }

            InvalidateDisplay();
        }

        /// <summary>
        /// Sets the parent padding.
        /// </summary>
        /// <param name="parentPadding">Parent padding.</param>
        public void SetParentPadding(RectOffset parentPadding)
        {

            ParentPadding = parentPadding;
            InvalidateDisplay();
        }

        /// <summary>
        /// Sets the margin.
        /// </summary>
        /// <param name="margin">Margin.</param>
        public void SetMargin(RectOffset margin)
        {
            Margin = margin;
            InvalidateDisplay();
        }

		/// <summary>
		/// Applies the theme.
		/// </summary>
		/// <param name="xd">Xd.</param>
        public virtual void ApplyTheme(XDStyle xd)
        {
           // CurrentStyle = xd;
        }
				
		/// <summary>
		/// Gets the or create child.
		/// </summary>
		/// <returns>The or create child.</returns>
		/// <param name="name">Name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        public T GetOrCreateChild<T>(string name)
        {
            var result = default(T);
            var searchresult = gameObject.GetComponentsInChildren<T>(true);

            foreach (var item in searchresult)
            {
                var childObj = item.GetType().GetProperty("gameObject").GetValue(item, null);
                var gameObj = (GameObject)Convert.ChangeType(childObj, typeof(GameObject));
                result = item;
                if (gameObj.name == name)
                {
                    result = item;
                }
            }

            if (result == null)
            {
                result = CreateChild<T>(name);
            }
            return result;
        }

		/// <summary>
		/// Creates the child.
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="name">Name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
        public T CreateChild<T>(string name)
        {

            if (!gameObject.activeInHierarchy)
            {
                return default(T);
            }

            var child = new GameObject();
            child.transform.SetParent(transform, false);
            child.name = name;
            var comp = child.AddComponent(typeof(T));

            return (T)Convert.ChangeType(comp, typeof(T));
        }

		/// <summary>
		/// Applies the child naming.
		/// </summary>
		/// <param name="gameObject">Game object.</param>
		/// <param name="name">Name.</param>
		public void ApplyChildNaming(GameObject gameObject, String name) {

			var treeNodeName = gameObject.transform.parent.name + name;
			if (gameObject.name != treeNodeName) {
				gameObject.name = treeNodeName;
			}
		}


    }
}
