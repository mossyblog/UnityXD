using System;
using Assets.UnityXD.Styles;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityXD.Core
{
    [ExecuteInEditMode]
    public abstract class BaseControl : MonoBindable
    {
        [SerializeField] private int _height;

        [SerializeField] private bool _ignoreParentPadding;

        [NonSerialized] private Image _imageRef;

        [SerializeField] private bool _isHeightDependantOnWidth;

        [SerializeField] private bool _isVisible;

        [SerializeField] private RectOffset _margin;

        [SerializeField] private RectOffset _padding;

        [SerializeField] private RectOffset _parentPadding;

        [NonSerialized] private RectTransform _rectTransformRef;

        [NonSerialized] private Text _textRef;

        [SerializeField] private int _width;

        [SerializeField] private int _x;

        [SerializeField] private int _y;
        [SerializeField]
        private SpriteAlignment _currentPivotAlignment;
        [SerializeField]
        private SpriteAlignment _currentAnchorAlignment;
        [SerializeField]
        private Style _currentStyle;

        [SerializeField]
        private bool _isHorizontalStretchEnabled;

        [SerializeField]
        private bool _isVeritcalStretchEnabled;


        private bool HasInitialized;

        #region Public Bindable Properties

        [SerializeField]
        public Style CurrentStyle
        {
            get { return _currentStyle; }
            protected internal set
            {
                NotifyOfPropertyChange(value, () => CurrentStyle, ref _currentStyle);
            }
        }

        public SpriteAlignment CurrentAnchorAlignment
        {
            get { return _currentAnchorAlignment; }
            set { NotifyOfPropertyChange(value, () => CurrentAnchorAlignment, ref _currentAnchorAlignment); }
        }

        public SpriteAlignment CurrentPivotAlignment
        {
            get { return _currentPivotAlignment; }
            set { NotifyOfPropertyChange(value, () => CurrentPivotAlignment, ref _currentPivotAlignment); }
        }

        public int Width
        {
            get { return _width; }
            set { NotifyOfPropertyChange(value, () => Width, ref _width); }
        }

        public int Height
        {
            get { return _height; }
            set { NotifyOfPropertyChange(value, () => Height, ref _height); }
        }

        public int X
        {
            get { return _x; }
            set { NotifyOfPropertyChange(value, () => X, ref _x); }
        }

        public int Y
        {
            get { return _y; }
            set { NotifyOfPropertyChange(value, () => Y, ref _y); }
        }

        public bool IsHeightDependantOnWidth
        {
            get { return _isHeightDependantOnWidth; }
            set { NotifyOfPropertyChange(value, () => IsHeightDependantOnWidth, ref _isHeightDependantOnWidth); }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { NotifyOfPropertyChange(value, () => IsVisible, ref _isVisible); }
        }

        public RectOffset Padding
        {
            get { return _padding; }
            set { NotifyOfPropertyChange(value, () => Padding, ref _padding); }
        }

        public RectOffset Margin
        {
            get { return _margin; }
            set { NotifyOfPropertyChange(value, () => Margin, ref _margin); }
        }

        public RectOffset ParentPadding
        {
            get { return _parentPadding; }
            set { NotifyOfPropertyChange(value, () => ParentPadding, ref _parentPadding); }
        }

        public bool IgnoreParentPadding
        {
            get { return _ignoreParentPadding; }
            set { NotifyOfPropertyChange(value, () => IgnoreParentPadding, ref _ignoreParentPadding); }
        }
        #endregion

        #region Internal References.

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
        ///     Gets the text reference.
        /// </summary>
        /// <value>The text reference.</value>
        public Text TextRef
        {
            get { return _textRef ?? (_textRef = GetComponent<Text>()); }
        }

        #endregion

        #region Stretch Settings

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
            get { return _isHorizontalStretchEnabled; }
            set { NotifyOfPropertyChange(value, () => IsHorizontalStretchEnabled, ref _isHorizontalStretchEnabled); }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is veritcal stretch enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is veritcal stretch enabled; otherwise, <c>false</c>.</value>
        public bool IsVeritcalStretchEnabled
        {
            get { return _isVeritcalStretchEnabled; }
            set { NotifyOfPropertyChange(value, () => IsVeritcalStretchEnabled, ref _isVeritcalStretchEnabled); }
        }

        #endregion

        #region Positioning

        private bool IsHorizonal()
        {
            var min = RectTransformRef.anchorMin;
            var max = RectTransformRef.anchorMax;
            return !min.Equals(max) && min.x == 0 && max.x == 1;
        }

        private bool IsVertical()
        {
            var min = RectTransformRef.anchorMin;
            var max = RectTransformRef.anchorMax;
            return !min.Equals(max) && min.y == 0 && max.y == 1;
        }

        /// <summary>
        ///     Sets the position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;

            var min = RectTransformRef.offsetMin;
            var max = RectTransformRef.offsetMax;

            var oMaxX = 0;
            var oMaxY = 0;
            var oMinX = 0;
            var oMinY = 0;


            if (!IgnoreParentPadding)
            {
                if (ParentPadding != null)
                {
                    oMaxX = -ParentPadding.right;
                    oMinX += ParentPadding.left;
                    oMinY += ParentPadding.bottom;
                    oMaxY += -ParentPadding.top;
                }
            }

            var mR = Margin != null ? Margin.right : 0;
            var mL = Margin != null ? Margin.left : 0;
            var mT = Margin != null ? Margin.top : 0;
            var mB = Margin != null ? Margin.bottom : 0;

            max.x = -mR + oMaxX;
            min.x = mL + x + oMinX;
            min.y = mB + oMinY;
            max.y = -mT + -y + oMaxY;

            // CENTER IS A BIT OF PROBLEM CHILD..
            if (CurrentAnchorAlignment == SpriteAlignment.Center)
            {
                if (IsHorizontalStretchEnabled)
                {
                    max.x = -mR + oMaxX + X*2;
                    min.x = mL + oMinX;
                }
                else
                {
                    max.x = 0;
                    min.x = X*2;
                }

                if (IsVeritcalStretchEnabled)
                {
                    max.y = -mT + oMaxY + -Y*2;
                    min.y = mB + oMinY;
                }
                else
                {
                    min.y = 0;
                    max.y = -Y*2;
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

//            Debug.LogFormat("Docking: {0}, H:{1} V:{2}", alignment, horizStretch, vertStretch);

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
        }

        #endregion

        #region Metrics

        /// <summary>
        ///     Sets the size.
        /// </summary>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        public virtual void SetSize(int w, int h)
        {
            if (IsHeightDependantOnWidth)
            {
                h = w;
            }

            if (!IsHorizontalStretchEnabled)
                RectTransformRef.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);

            if (!IsVeritcalStretchEnabled)
                RectTransformRef.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);

            Width = w;
            Height = h;
        }

        /// <summary>
        ///     Sets the padding.
        /// </summary>
        /// <param name="padding">Padding.</param>
        public void SetPadding(RectOffset padding)
        {
            Padding = padding;
            var children = GetComponentsInChildren<BaseControl>();

            // Tell the Children of the new padding.
            foreach (var child in children)
            {
                if (child != this)
                {
                    child.SetParentPadding(Padding);
                }
            }
        }

        /// <summary>
        ///     Sets the parent padding.
        /// </summary>
        /// <param name="parentPadding">Parent padding.</param>
        public void SetParentPadding(RectOffset parentPadding)
        {
            ParentPadding = parentPadding;
        }

        /// <summary>
        ///     Sets the margin.
        /// </summary>
        /// <param name="margin">Margin.</param>
        public void SetMargin(RectOffset margin)
        {
            Margin = margin;
        }

        #endregion

        #region Utility Helpers

        /// <summary>
        ///     Applies the child naming.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <param name="name">Name.</param>
        public void ApplyChildNaming(GameObject gameObject, string name)
        {
            var treeNodeName = gameObject.transform.parent.name + name;
            if (gameObject.name != treeNodeName)
            {
                gameObject.name = treeNodeName;
            }
        }

        internal abstract void RegisterBindings();

        #endregion


        public virtual void Start()
        {
            if (!HasInitialized)
            {
                Initialize(); ;
            }
        }

        public virtual void OnEnable()
        {
            if (!HasInitialized)
            {
                Initialize();;
            }
        }

        public virtual void Update()
        {
            if (!HasInitialized)
            {
                Initialize(); ;
            }
        }

        private void Initialize()
        {        
            Debug.Log("Initialzing Component");
            CacheComponentReferences();
            RegisterBindings();
            ValidateHeirachy();
            InvalidateComponent();

            HasInitialized = true;
        }

        public virtual void InvalidateLayout()
        {
            Dock(CurrentAnchorAlignment, IsHorizontalStretchEnabled, IsVeritcalStretchEnabled);
            SetPadding(Padding);
            SetMargin(Margin);
            SetSize(Width, Height);
            SetPosition(X, Y);
                  
        }

        public virtual void SetStyle(Style style)
        {
            // TODO : Determine how to handle "isPadding/Margin Dirty" in that should an inbound style overwrite existing settings?          
            if (style.Padding != null & Padding == null)
            {
                SetPadding(style.Padding);
            }

            if (style.Margin != null & Margin == null)
            {
                SetPadding(style.Padding);
            }
        }

        public abstract void InvalidateComponent();
        public abstract void CacheComponentReferences();

        public virtual void ValidateHeirachy()
        {
            
        }
    }
}