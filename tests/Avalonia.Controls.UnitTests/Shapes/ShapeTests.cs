using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.UnitTests;
using Xunit;

namespace Avalonia.Controls.UnitTests.Shapes;

public class ShapeTests
{
    [Fact]
    public void Shape_Changing_AffectsGeometry_Property_Causes_Invalidation()
    {
        using var app = UnitTestApplication.Start(TestServices.MockPlatformRenderInterface);

        var target = new TestShape();
        target.Measure(new Size());
        Assert.True(target.IsMeasureValid);

        target.FooAffectsGeometry = true;
        Assert.False(target.IsMeasureValid);
    }
    
    [Fact]
    public void Shape_Changing_NonAffectsGeometry_Property_Does_Not_Cause_Invalidation()
    {
        using var app = UnitTestApplication.Start(TestServices.MockPlatformRenderInterface);

        var target = new TestShape();
        target.Measure(new Size());
        Assert.True(target.IsMeasureValid);

        target.Foo = true;
        Assert.True(target.IsMeasureValid);
    }
    
    [Fact]
    public void Shape_Does_Not_Invalidate_Geometry_If_Bounds_Changed_But_Size_Not_Changed()
    {
        using var app = UnitTestApplication.Start(TestServices.MockPlatformRenderInterface);
        
        var target = new TestShape();
        
        var root = new TestRoot(target);

        Assert.True(SetBounds(target, new Rect(0, 0, 100, 100)));
        Assert.False(target.IsMeasureValid);
        
        target.Measure(new Size());
        Assert.True(target.IsMeasureValid);
            
        // Different Bounds but same Size
        // Expect no invalidation
        Assert.True(SetBounds(target, new Rect(100, 100, 100, 100)));
        Assert.True(target.IsMeasureValid);
        
        root.Child = null;
    }
    
    [Fact]
    public void Shape_Invalidates_Geometry_If_Bounds_Size_Changed()
    {
        using var app = UnitTestApplication.Start(TestServices.MockPlatformRenderInterface);
        
        var target = new TestShape();
        
        var root = new TestRoot(target);

        Assert.True(SetBounds(target, new Rect(0, 0, 100, 100)));
        Assert.False(target.IsMeasureValid);
        
        target.Measure(new Size());
        Assert.True(target.IsMeasureValid);
        
        // Different Bounds with new Size
        // Expect invalidation
        Assert.True(SetBounds(target, new Rect(200, 200, 400, 300)));
        Assert.False(target.IsMeasureValid);

        root.Child = null;
    }
    
    private static bool SetBounds(Shape shape, Rect bounds)
    {
        shape.Width = bounds.Width;
        shape.Height = bounds.Height;
        shape.Arrange(bounds);
        return shape.Bounds == bounds;
    }
    
    private class TestShape : Shape
    {
        public static readonly StyledProperty<bool> FooProperty =
            AvaloniaProperty.Register<TestShape, bool>(nameof(Foo));
        
        public static readonly StyledProperty<bool> FooAffectsGeometryProperty =
            AvaloniaProperty.Register<TestShape, bool>(nameof(Foo));

        static TestShape()
        {
            AffectsGeometry<TestShape>(FooAffectsGeometryProperty, BoundsProperty);
        }

        public bool Foo
        {
            get => GetValue(FooProperty);
            set => SetValue(FooProperty, value);
        }
        
        public bool FooAffectsGeometry
        {
            get => GetValue(FooAffectsGeometryProperty);
            set => SetValue(FooAffectsGeometryProperty, value);
        }

        protected override Geometry CreateDefiningGeometry()
        {
            return new RectangleGeometry(Bounds);
        }
    }
}
