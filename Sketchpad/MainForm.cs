﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using FillCut.Data;
using FillCut.Utils;
using FillCut.Data.AreaAlgorithms;

namespace FillCut
{
    public partial class MainForm : Form
    {
        private CanvasData canvasData = new CanvasData();

        public MainForm()
        {
            InitializeComponent();

        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                canvasData.clickCoordinates = e.Location;
                return;
            }

            HandleMouseDown(canvasData, e.Location);

            //PaintCanvas();
            canvas.Invalidate();
        }

        private void HandleMouseDown(CanvasData canvasData, Point clickCoordinates)
        {
            InputMode inputMode = InputMode.SingleLeftClick;
            if ((ModifierKeys == Keys.Control))
                inputMode = InputMode.CtrlPressed;

            canvasData = GetBehaviorAndCanvasData(canvasData, clickCoordinates, inputMode);
            CommandsList.GetCommand(canvasData).Change(canvasData);
            this.canvasData = canvasData;

        }


        private CanvasData GetBehaviorAndCanvasData(CanvasData canvasData, Point clickCoordinates, InputMode clickMode = InputMode.SingleLeftClick)
        {
            if (clickMode == InputMode.SingleLeftClick)
            {
                int clickedVertexIndex = -1;
                if ((clickedVertexIndex = Algorithms.FindIfClickedNearVertex(canvasData, clickCoordinates)) == -1)
                {
                    Tuple<int, int> edge = new Tuple<int, int>(-1, -1);
                    if ((edge = Algorithms.FindIfClickedNearEdge(canvasData, clickCoordinates)).Equals(new Tuple<int, int>(-1, -1)))
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexAdd, clickCoordinates = clickCoordinates };
                    else
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.EdgeVertexAdd, clickedEdge = edge };
                }
                else
                    return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexDelete, clickedVertexIndex = clickedVertexIndex };
            }

            if (clickMode == InputMode.CtrlPressed)
            {
                int clickedVertexIndex = -1;
                if ((clickedVertexIndex = Algorithms.FindIfClickedNearVertex(canvasData, clickCoordinates)) == -1)
                {
                    if (Algorithms.CheckIfClickedInsideBoundingBox(Polygon.GetBoundingBox(canvasData.polygon.vertices), clickCoordinates))
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.PolygonMove, clickCoordinates = clickCoordinates, moveCoordinates = clickCoordinates };
                    else
                        return new CanvasData(canvasData) { behaviourMode = BehaviourMode.DoNothing };
                }
                else
                    return new CanvasData(canvasData) { behaviourMode = BehaviourMode.VertexMove, clickCoordinates = clickCoordinates, clickedVertexIndex = clickedVertexIndex };
            }

            return new CanvasData(canvasData) { behaviourMode = BehaviourMode.DoNothing };
        }

 

        void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            HandleMouseMove(canvasData, e.Location);

            //PaintCanvas();
            canvas.Invalidate();
        }

        private void HandleMouseMove(CanvasData canvasData, Point clickCoordinates)
        {
            if (canvasData.behaviourMode == BehaviourMode.VertexMove)
            {
                canvasData.clickCoordinates = clickCoordinates;
                CommandsList.GetCommand(canvasData).Change(canvasData);
            }
            else if (canvasData.behaviourMode == BehaviourMode.PolygonMove)
            {
                canvasData.moveCoordinates = clickCoordinates;
                CommandsList.GetCommand(canvasData).Change(canvasData);
            }

            this.canvasData = canvasData;

        }

        void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            canvasData.behaviourMode = BehaviourMode.DoNothing;
        }
        
        //private void PaintCanvas()
        //{
        //    Graphics graphics = canvas.CreateGraphics();
        //    Render.Render.PaintCanvas(graphics, canvasData.polygon.vertices);

        //    graphics.Dispose();
        //}

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            
            Render.Render.PaintCanvas(e, canvasData);
        }

    }
}
