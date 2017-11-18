using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

using FillCut.Data;
using FillCut.Utils;
using FillCut.Data.AreaAlgorithms;

namespace FillCut
{
    public partial class MainForm : Form
    {
        private CanvasData canvasData = new CanvasData();
        private TextureData textureData = new TextureData();

        private void InitCanvasData()
        {
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(100, 100));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(150, 100));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(300, 200));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(200, 250));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(100, 200));

            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(100, 100));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(150, 100));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(300, 200));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(200, 150));
            //canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(500, 500));

            //canvasData.polygons.Add(new Polygon());

            //canvasData.polygons[1].vertices.Add(new Point(200, 200));
            //canvasData.polygons[1].vertices.Add(new Point(250, 200));
            //canvasData.polygons[1].vertices.Add(new Point(400, 300));
            //canvasData.polygons[1].vertices.Add(new Point(300, 350));
            //canvasData.polygons[1].vertices.Add(new Point(200, 300));

            //canvasData.polygons[1].vertices.Add(new Point(200, 200));
            //canvasData.polygons[1].vertices.Add(new Point(250, 200));
            //canvasData.polygons[1].vertices.Add(new Point(400, 300));
            //canvasData.polygons[1].vertices.Add(new Point(300, 250));
            //canvasData.polygons[1].vertices.Add(new Point(600, 600));





            canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(100, 100));
            canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(150, 137));
            canvasData.polygons[canvasData.activePolygon].vertices.Add(new Point(300, 157));


        }

        private void InitTextureData()
        {
            //textureData.lightColor = Color.FromArgb(0, 255, 64);

            //textureData.objectColor = Color.FromArgb(0, 162, 232);
            //textureData.polygonColorMode = PolygonColorMode.ConstantColor;

            //textureData.normalVectorMode = NormalVectorMode.ConstantNormalVector;

            //textureData.lightVectorMode = LightVectorMode.ConstantLightVector;

            //textureData.vectorDisorderMode = VectorDisorderMode.LackOfDisorder;

            //textureData.lightColor = Color.FromArgb(255, 255, 255);

            //textureData.texture = new Bitmap(@"C:\Users\bartosz\Desktop\crupled_white_paper_texture.jpeg");
            //textureData.polygonColorMode = PolygonColorMode.TextureColor;

            //textureData.normalMap = new Bitmap(@"C:\Users\bartosz\Desktop\world_normal_map.jpg");
            //textureData.normalVectorMode = NormalVectorMode.TextureNormalVector;

            //textureData.sphereRadius = 10;
            //textureData.lightVectorMode = LightVectorMode.MovingLightVector;

            //textureData.heightMap = new Bitmap(@"C:\Users\bartosz\Desktop\stone_wall_height_map.jpg");
            //textureData.vectorDisorderMode = VectorDisorderMode.HeightMapDisorder;

            textureData.lightColor = Color.FromArgb(255, 255, 255);

            textureData.texture = new Bitmap(@"C:\Users\bartosz\Desktop\wood_texture.jpg");
            textureData.polygonColorMode = PolygonColorMode.TextureColor;

            textureData.normalMap = new Bitmap(@"C:\Users\bartosz\Desktop\wall_normal_map.png");
            textureData.normalVectorMode = NormalVectorMode.TextureNormalVector;

            textureData.sphereRadius = 10;
            textureData.lightVectorMode = LightVectorMode.MovingLightVector;

            textureData.heightMap = new Bitmap(@"C:\Users\bartosz\Desktop\brick_height_map.jpg");
            textureData.vectorDisorderMode = VectorDisorderMode.HeightMapDisorder;

        }

        public void Init()
        {
            InitCanvasData();
            InitTextureData();

        }

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                canvasData.clickCoordinates = e.Location;
                //canvasData.polygons[0].vertices = GetIntersectedPolygon(canvasData.polygons[0].vertices.ToArray(), canvasData.polygons[1].vertices.ToArray()).ToList();
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
                    if (Algorithms.CheckIfClickedInsideBoundingBox(Polygon.GetBoundingBox(canvasData.polygons[canvasData.activePolygon].vertices), clickCoordinates))
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
            int a = canvas.Height;
            Render.Render.PaintCanvas(sender, e, canvasData, textureData);
        }



        private void PolygonColorPictureBox_Click(object sender, EventArgs e)
        {
            PolygonColorColorDialog.ShowDialog();
            PolygonColorPictureBox.BackColor = PolygonColorColorDialog.Color;
        }

        private void texturePictureBox_Click(object sender, EventArgs e)
        {
            using (var textureFileDialog = new OpenFileDialog())
            {
                textureFileDialog.Filter = "JPG File (.jpg)|*.jpg|PNG File (.png)|*.png";

                if (textureFileDialog.ShowDialog() == DialogResult.OK)
                    textureData.texture = new Bitmap(textureFileDialog.FileName);
            }
        }

        ////////////////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Class: Edge

        /// <summary>
        /// This represents a line segment
        /// </summary>
        private class Edge
        {
            public Edge(Point from, Point to)
            {
                this.From = from;
                this.To = to;
            }

            public readonly Point From;
            public readonly Point To;
        }

        #endregion

        /// <summary>
        /// This clips the subject polygon against the clip polygon (gets the intersection of the two polygons)
        /// </summary>
        /// <remarks>
        /// Based on the psuedocode from:
        /// http://en.wikipedia.org/wiki/Sutherland%E2%80%93Hodgman
        /// </remarks>
        /// <param name="subjectPoly">Can be concave or convex</param>
        /// <param name="clipPoly">Must be convex</param>
        /// <returns>The intersection of the two polygons (or null)</returns>
        public static Point[] GetIntersectedPolygon(Point[] subjectPoly, Point[] clipPoly)
        {
            if (subjectPoly.Length < 3 || clipPoly.Length < 3)
            {
                throw new ArgumentException(string.Format("The polygons passed in must have at least 3 points: subject={0}, clip={1}", subjectPoly.Length.ToString(), clipPoly.Length.ToString()));
            }

            List<Point> outputList = subjectPoly.ToList();

            //	Make sure it's clockwise
            if (!IsClockwise(subjectPoly))
            {
                outputList.Reverse();
            }

            //	Walk around the clip polygon clockwise
            foreach (Edge clipEdge in IterateEdgesClockwise(clipPoly))
            {
                List<Point> inputList = outputList.ToList();		//	clone it
                outputList.Clear();

                if (inputList.Count == 0)
                {
                    //	Sometimes when the polygons don't intersect, this list goes to zero.  Jump out to avoid an index out of range exception
                    break;
                }

                Point S = inputList[inputList.Count - 1];

                foreach (Point E in inputList)
                {
                    if (IsInside(clipEdge, E))
                    {
                        if (!IsInside(clipEdge, S))
                        {
                            Point? point = GetIntersect(S, E, clipEdge.From, clipEdge.To);
                            if (point == null)
                            {
                                throw new ApplicationException("Line segments don't intersect");		//	may be colinear, or may be a bug
                            }
                            else
                            {
                                outputList.Add(point.Value);
                            }
                        }

                        outputList.Add(E);
                    }
                    else if (IsInside(clipEdge, S))
                    {
                        Point? point = GetIntersect(S, E, clipEdge.From, clipEdge.To);
                        if (point == null)
                        {
                            throw new ApplicationException("Line segments don't intersect");		//	may be colinear, or may be a bug
                        }
                        else
                        {
                            outputList.Add(point.Value);
                        }
                    }

                    S = E;
                }
            }

            //	Exit Function
            return outputList.ToArray();
        }

        #region Private Methods

        /// <summary>
        /// This iterates through the edges of the polygon, always clockwise
        /// </summary>
        private static IEnumerable<Edge> IterateEdgesClockwise(Point[] polygon)
        {
            if (IsClockwise(polygon))
            {
                #region Already clockwise

                for (int cntr = 0; cntr < polygon.Length - 1; cntr++)
                {
                    yield return new Edge(polygon[cntr], polygon[cntr + 1]);
                }

                yield return new Edge(polygon[polygon.Length - 1], polygon[0]);

                #endregion
            }
            else
            {
                #region Reverse

                for (int cntr = polygon.Length - 1; cntr > 0; cntr--)
                {
                    yield return new Edge(polygon[cntr], polygon[cntr - 1]);
                }

                yield return new Edge(polygon[0], polygon[polygon.Length - 1]);

                #endregion
            }
        }

        /// <summary>
        /// Returns the intersection of the two lines (line segments are passed in, but they are treated like infinite lines)
        /// </summary>
        /// <remarks>
        /// Got this here:
        /// http://stackoverflow.com/questions/14480124/how-do-i-detect-triangle-and-rectangle-intersection
        /// </remarks>
        private static Point? GetIntersect(Point line1From, Point line1To, Point line2From, Point line2To)
        {
            System.Windows.Vector direction1 = new System.Windows.Vector(line1To.X - line1From.X, line1To.Y - line1From.Y);
            System.Windows.Vector direction2 = new System.Windows.Vector(line2To.X - line2From.X, line2To.Y - line2From.Y);
            double dotPerp = (direction1.X * direction2.Y) - (direction1.Y * direction2.X);

            // If it's 0, it means the lines are parallel so have infinite intersection points
            if (IsNearZero(dotPerp))
            {
                return null;
            }

            System.Windows.Vector c = new System.Windows.Vector(line2From.X - line1From.X, line2From.Y - line1From.Y);
            double t = (c.X * direction2.Y - c.Y * direction2.X) / dotPerp;
            //if (t < 0 || t > 1)
            //{
            //    return null;		//	lies outside the line segment
            //}

            //double u = (c.X * direction1.Y - c.Y * direction1.X) / dotPerp;
            //if (u < 0 || u > 1)
            //{
            //    return null;		//	lies outside the line segment
            //}

            //	Return the intersection point
            return new Point((int)(line1From.X + (t * direction1.X)), (int)(line1From.Y + (t * direction1.Y)));
        }

        private static bool IsInside(Edge edge, Point test)
        {
            bool? isLeft = IsLeftOf(edge, test);
            if (isLeft == null)
            {
                //	Colinear points should be considered inside
                return true;
            }

            return !isLeft.Value;
        }
        private static bool IsClockwise(Point[] polygon)
        {
            for (int cntr = 2; cntr < polygon.Length; cntr++)
            {
                bool? isLeft = IsLeftOf(new Edge(polygon[0], polygon[1]), polygon[cntr]);
                if (isLeft != null)		//	some of the points may be colinear.  That's ok as long as the overall is a polygon
                {
                    return !isLeft.Value;
                }
            }

            throw new ArgumentException("All the points in the polygon are colinear");
        }

        /// <summary>
        /// Tells if the test point lies on the left side of the edge line
        /// </summary>
        private static bool? IsLeftOf(Edge edge, Point test)
        {
            System.Windows.Vector tmp1 = new System.Windows.Vector(edge.To.X - edge.From.X, edge.To.Y - edge.From.Y);
            System.Windows.Vector tmp2 = new System.Windows.Vector(test.X - edge.To.X, test.Y - edge.To.Y);

            double x = (tmp1.X * tmp2.Y) - (tmp1.Y * tmp2.X);       //	dot product of perpendicular?

            if (x < 0)
            {
                return false;
            }
            else if (x > 0)
            {
                return true;
            }
            else
            {
                //	Colinear points;
                return null;
            }
        }

        private static bool IsNearZero(double testValue)
        {
            return Math.Abs(testValue) <= .000000001d;
        }

        #endregion

        //Algorithm for clipping polygon
        private void SutherlandHodgmanAlgorithm()
        {
            //Reimplement to support many polygons, not only 2
            int convexPolygon = 1;
            if (CheckIfPolygonIsConvex(canvasData.polygons[1]))
                convexPolygon = 1;
            else
                if (CheckIfPolygonIsConvex(canvasData.polygons[0]))
                convexPolygon = 0;
            else
                throw new ArgumentException("At least 1 polygon should be convex");



        }

        private bool CheckIfPolygonIsConvex(Polygon polygon)
        {
            if (canvasData.polygons[canvasData.activePolygon].vertices.Count < 3)
                throw new ArgumentException("Too small amount of vertices");

            Point firstPoint = polygon.vertices[0], secondPoint = polygon.vertices[1], thirdPoint = polygon.vertices[2];
            int dx1 = secondPoint.X - firstPoint.X, dy1 = secondPoint.Y - firstPoint.Y, dx2 = thirdPoint.X - secondPoint.X, dy2 = thirdPoint.Y - secondPoint.Y;

            System.Windows.Vector firstVector = new System.Windows.Vector(dx1, dy1), secondVector = new System.Windows.Vector(dx2, dy2);
            double crossProduct = System.Windows.Vector.CrossProduct(firstVector, secondVector);

            for (int i = 0; i < polygon.vertices.Count; i++)
            {
                Point nextFirstPoint = polygon.vertices[i], nextSecondPoint = polygon.vertices[(i + 1) % polygon.vertices.Count],
                    nextThirdPoint = polygon.vertices[(i + 2) % polygon.vertices.Count];
                int nextDx1 = nextSecondPoint.X - nextFirstPoint.X, nextDy1 = nextSecondPoint.Y - nextFirstPoint.Y, nextDx2 = nextThirdPoint.X - nextSecondPoint.X,
                    nextDy2 = nextThirdPoint.Y - nextSecondPoint.Y;

                System.Windows.Vector nextFirstVector = new System.Windows.Vector(nextDx1, nextDy1), nextSecondVector = new System.Windows.Vector(nextDx2, nextDy2);
                if (Math.Sign(crossProduct) != Math.Sign(System.Windows.Vector.CrossProduct(nextFirstVector, nextSecondVector)))
                    return false;
            }


            return true;
        }



    }
}


