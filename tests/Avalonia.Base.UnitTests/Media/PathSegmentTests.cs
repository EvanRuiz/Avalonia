using Avalonia.Media;
using Xunit;

namespace Avalonia.Base.UnitTests.Media
{
    public class PathSegmentTests
    {
        [Fact]
        public void PathSegment_Triggers_Invalidation_On_Property_Change()
        {
            var targetSegment = new ArcSegment()
            {
                Size = new Size(10, 10),
                Point = new Point(5, 5)
            };

            var target = new PathGeometry
            {
                Figures = new PathFigures
                {
                    new PathFigure { IsClosed = false, Segments = new PathSegments { targetSegment } }
                }
            };
            
            var changed = false;

            target.Changed += (s, e) => changed = true;

            targetSegment.Size = new Size(20, 20);

            Assert.True(changed);
        }
        
        [Fact]
        public void ArcSegment_Triggers_PathGeometry_Invalidation_On_All_Property_Changes()
        {
            var targetSegment = new ArcSegment();
            
            var target = new PathGeometry()
            {
                Figures = [new PathFigure { IsClosed = false, Segments = [targetSegment] }]
            };
            var changed = false;
            target.Changed += (_, _) => { changed = true; };
         
            // Point
            targetSegment.Point = new Point(1, 1);
            Assert.True(changed);

            changed = false;
            
            // Size
            targetSegment.Size = new Size(20, 20);
            Assert.True(changed);

            changed = false;

            // IsLargeArc
            targetSegment.IsLargeArc = true;
            Assert.True(changed);
            
            changed = false;

            // SweepDirection
            targetSegment.SweepDirection = SweepDirection.CounterClockwise;
            Assert.True(changed);

            changed = false;

            // Rotation Angle
            targetSegment.RotationAngle = 90;
            Assert.True(changed);
        }

        [Fact]
        public void BezierSegment_Triggers_PathGeometry_Invalidation_On_All_Property_Changes()
        {
            var targetSegment = new BezierSegment()
            {
                Point1 = new Point(1, 1),
                Point2 = new Point(2, 2),
                Point3 = new Point(3, 3)
            };
            
            var target = new PathGeometry()
            {
                Figures = [new PathFigure { IsClosed = false, Segments = [targetSegment] }]
            };
            var changed = false;
            target.Changed += (_, _) => { changed = true; };
         
            // Point1
            targetSegment.Point1 = new Point();
            Assert.True(changed);
            
            changed = false;
            
            // Point2
            targetSegment.Point2 = new Point();
            Assert.True(changed);
            
            changed = false;
            
            // Point3
            targetSegment.Point3 = new Point();
            Assert.True(changed);
        }
        
        [Fact]
        public void LineSegment_Triggers_PathGeometry_Invalidation_On_All_Property_Changes()
        {
            var targetSegment = new LineSegment();
            
            var target = new PathGeometry()
            {
                Figures = [new PathFigure { IsClosed = false, Segments = [targetSegment] }]
            };
            var changed = false;
            target.Changed += (_, _) => { changed = true; };
            
            targetSegment.Point = new Point(1, 1);
            Assert.True(changed);
        }
        
        [Fact]
        public void PolyBezierSegment_Triggers_PathGeometry_Invalidation_On_All_Property_Changes()
        {
            var targetSegment = new PolyBezierSegment() { Points = [new Point(0, 0)] };
            var target = new PathGeometry()
            {
                Figures = [new PathFigure { IsClosed = false, Segments = [targetSegment] }]
            };
            var changed = false;
            target.Changed += (_, _) => { changed = true; };
            
            targetSegment.Points = [];
            Assert.True(changed);
        }
        
        [Fact]
        public void PolyLineSegment_Triggers_PathGeometry_Invalidation_On_All_Property_Change()
        {
            var targetSegment = new PolyLineSegment() { Points = [new Point(0, 0)] };
            
            var target = new PathGeometry()
            {
                Figures = [new PathFigure { IsClosed = false, Segments = [targetSegment] }]
            };
            var changed = false;
            target.Changed += (_, _) => { changed = true; };
            
            targetSegment.Points = [];
            Assert.True(changed);
        }
        
        [Fact]
        public void QuadraticBezierSegment_Triggers_PathGeometry_Invalidation_On_All_Property_Change()
        {
            var targetSegment = new QuadraticBezierSegment();
            var target = new PathGeometry()
            {
                Figures = [new PathFigure { IsClosed = false, Segments = [targetSegment] }]
            };
            var changed = false;
            target.Changed += (_, _) => { changed = true; };
            
            // Point1
            targetSegment.Point1 = new Point(1, 1);
            Assert.True(changed);

            changed = false;
            
            // Point2
            targetSegment.Point2 = new Point(1, 1);
            Assert.True(changed);
        }
    }
}
