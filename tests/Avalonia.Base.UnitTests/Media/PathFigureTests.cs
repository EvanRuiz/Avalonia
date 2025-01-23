using Avalonia.Media;
using Xunit;

namespace Avalonia.Base.UnitTests.Media;

public class PathFigureTests
{
    [Fact]
    public void PathFigure_Triggers_PathGeometry_Invalidation_On_All_Property_Changes()
    {
        var targetFigure = new PathFigure()
        {
            IsClosed = false,
            IsFilled = false,
            Segments = []
        };

        var target = new PathGeometry() { Figures = [targetFigure] };
        
        var changed = false;
        target.Changed += (_, _) => { changed = true; };
        
        // IsClosed
        targetFigure.IsClosed = true;
        Assert.True(changed);

        changed = false;
        
        // IsFilled
        targetFigure.IsFilled = true;
        Assert.True(changed);

        changed = false;
        
        // Segments
        targetFigure.Segments = [new PolyLineSegment() { Points = [new Point(1, 1)] }];
        Assert.True(changed);
    }
}
