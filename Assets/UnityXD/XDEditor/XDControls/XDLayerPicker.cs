using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Widget for selecting Unity layers.
    /// </summary>
    public interface IXDLayerPicker : IXDWidget
    {
        /// <summary>
        /// Text to display to the left of the widget.
        /// </summary>
        IPropertyBinding<string, IXDLayerPicker> Label { get; }

        /// <summary>
        /// List of names of the selected layers.
        /// </summary>
        IPropertyBinding<string[], IXDLayerPicker> SelectedLayers { get; }
    }

    /// <summary>
    /// Widget for selecting Unity layers.
    /// </summary>
    internal class XDLayerPicker : XDWidget, IXDLayerPicker
    {
        private string label;

        private LayerMask selectedLayerMask;

        private static List<string> layers;
        private static List<int> layerNumbers;
        private static string[] layerNames;
        private static long lastUpdateTick;

        private PropertyBinding<string, IXDLayerPicker> labelProperty;
        private PropertyBinding<string[], IXDLayerPicker> selectedLayersProperty;

        public IPropertyBinding<string, IXDLayerPicker> Label { get { return labelProperty; } }
        public IPropertyBinding<string[], IXDLayerPicker> SelectedLayers { get { return selectedLayersProperty; } }

        internal XDLayerPicker(IXDLayout parent) : base(parent)
        {
            labelProperty = new PropertyBinding<string, IXDLayerPicker>(
                this,
                value => this.label = value
            );

            selectedLayersProperty = new PropertyBinding<string[], IXDLayerPicker>(
                this,
                value => this.selectedLayerMask.value = LayerNamesToMask(value)
            );
        }

        public override void OnGUI()
        {
            var newLayerMask = LayerMaskField(label, selectedLayerMask);
            if (selectedLayerMask != newLayerMask)
            {
                selectedLayerMask = newLayerMask;
                var newLayerNames = MaskToLayerNames(selectedLayerMask).ToArray();
                selectedLayersProperty.UpdateView(newLayerNames);
            }
        }

        internal override void BindViewModel(object viewModel)
        {
            labelProperty.BindViewModel(viewModel);
            selectedLayersProperty.BindViewModel(viewModel);
        }

        /// <summary>
        /// http://answers.unity3d.com/questions/42996/how-to-create-layermask-field-in-a-custom-editorwi.html
        /// </summary>
        private static LayerMask LayerMaskField(string label, LayerMask selected)
        {
            if (layers == null || (DateTime.Now.Ticks - lastUpdateTick > 10000000L && Event.current.type == EventType.Layout))
            {
                lastUpdateTick = DateTime.Now.Ticks;
                if (layers == null)
                {
                    layers = new List<string>();
                    layerNumbers = new List<int>();
                    layerNames = new string[4];
                }
                else
                {
                    layers.Clear();
                    layerNumbers.Clear();
                }

                int emptyLayers = 0;
                for (int i = 0; i < 32; i++)
                {
                    string layerName = LayerMask.LayerToName(i);

                    if (layerName != String.Empty)
                    {
                        for (; emptyLayers > 0; emptyLayers--)
                        {
                            layers.Add("Layer " + (i - emptyLayers));
                        }
                        layerNumbers.Add(i);
                        layers.Add(layerName);
                    }
                    else
                    {
                        emptyLayers++;
                    }
                }

                if (layerNames.Length != layers.Count)
                {
                    layerNames = new string[layers.Count];
                }
                for (int i = 0; i < layerNames.Length; i++)
                {
                    layerNames[i] = layers[i];
                }
            }

            selected.value = EditorGUILayout.MaskField(label, selected.value, layerNames);

            return selected;
        }

        /// <summary>
        /// Convert layer names to a mask.
        /// </summary>
        private int LayerNamesToMask(IEnumerable<string> layerNames)
        {

            return layerNames
                .Select(n => 1 << LayerMask.NameToLayer(n)) // Convert to index then to bitfield.
                .Aggregate(0, (l, r) => l | r);
        }

        /// <summary>
        /// Convert a layers mask to a collection of layer names.
        /// </summary>
        private IEnumerable<string> MaskToLayerNames(int layerMask)
        {
            var layerIndex = 0;

            while (layerMask > 0)
            {
                if ((layerMask & 0x1) != 0)
                {
                    var layerName = LayerMask.LayerToName(layerIndex);
                    if (!string.IsNullOrEmpty(layerName))
                    {
                        yield return layerName;
                    }
                }

                layerMask >>= 1;
                ++layerIndex;
            }
        }
    }
}
