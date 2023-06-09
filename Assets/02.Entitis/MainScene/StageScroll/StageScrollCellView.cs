﻿using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;
using System;

namespace EnhancedScrollerCustom.StageScroll
{
    /// <summary>
    /// This delegate handles the UI's button click
    /// </summary>
    /// <param name="cellView">The cell view that had the button click</param>
    public delegate void SelectedDelegate(StageScrollRowCellView rowCellView);

    /// <summary>
    /// This is the view of our cell which handles how the cell looks.
    /// It stores references to sub cells
    /// </summary>
    public class StageScrollCellView : EnhancedScrollerCellView
    {
        public StageScrollRowCellView[] rowCellViews;
        private LayoutElement layoutElement;
        private bool setedComingsoon = false;
        private bool setedCrossBanner = false;

        private void Start()
        {
            layoutElement = GetComponent<LayoutElement>();
        }

        /// <summary>
        /// This function just takes the Demo data and displays it
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ref SmallList<StageScrollData> data, int myLevel, int startingIndex, StageScrollController controller, SelectedDelegate selected, bool isCrossBanner)
        {
            // loop through the sub cells to display their data (or disable them if they are outside the bounds of the data)

            if (startingIndex >= StaticGameSettings.TotalStage)
            {
                controller.comingsoon.gameObject.SetActiveSelf(true);
                controller.comingsoon.SetParent(transform);
                controller.comingsoon.anchoredPosition = Vector3.zero;
                controller.comingsoon.localScale = Vector3.one;
                setedComingsoon = true;
            }
            else
            {
                if (setedComingsoon)
                {
                    setedComingsoon = false;
                    controller.comingsoon.gameObject.SetActiveSelf(false);
                }
            }

            if (controller.crossBanner != null)
            {
                if (myLevel<=startingIndex && myLevel > startingIndex-4 && isCrossBanner)
                {
                    controller.crossBanner.gameObject.SetActiveSelf(true);
                    controller.crossBanner.SetParent(transform);
                    controller.crossBanner.anchoredPosition = Vector3.zero;
                    controller.crossBanner.localScale = Vector3.one;
                    setedCrossBanner = true;
                }
                else
                {
                    controller.StageOpening = true;
                    if (setedCrossBanner && controller.StageOpening)
                    {
                        setedCrossBanner = false;
                        controller.crossBanner.gameObject.SetActiveSelf(false);
                    }
                }
            }
            float __expandedSize = 0f;
            for (var i = 0; i < rowCellViews.Length; i++)
            {
                var dataIndex = startingIndex + i;
                if (data[dataIndex] == null || dataIndex >= StaticGameSettings.TotalStage)
                {
                    rowCellViews[i].gameObject.SetActiveSelf(false);
                    rowCellViews[i].SetData(dataIndex, null, myLevel, controller, selected);
                    continue;
                }
                if (myLevel<=startingIndex && myLevel>startingIndex-4 && isCrossBanner)
                {
                    rowCellViews[i].gameObject.SetActiveSelf(false);
                    continue;
                }
                rowCellViews[i].gameObject.SetActiveSelf(true);
                if (data[dataIndex].expandedSize != 0 && __expandedSize < data[dataIndex].expandedSize)
                {
                    __expandedSize = data[dataIndex].expandedSize;
                }
                // if the sub cell is outside the bounds of the data, we pass null to the sub cell
                rowCellViews[i].SetData(dataIndex, dataIndex < data.Count ? data[dataIndex] : null, myLevel, controller, selected);
            }
            if (null != layoutElement)
            {
                //Debug.LogWarningFormat("KKI :: {0}", __expandedSize);
                //layoutElement.minHeight = __expandedSize;

                if (__expandedSize == 300)
                {
                    //Debug.LogWarningFormat("KKI{0}", transform);
                    //controller.imageFrameADS.SetParent(transform);
                    //controller.imageFrameADS.anchoredPosition = Vector3.zero;
                    //controller.imageFrameADS.SetParent(transform.parent);
                    //controller.imageFrameADS.SetSiblingIndex(1);
                }
            }
        }
    }
}