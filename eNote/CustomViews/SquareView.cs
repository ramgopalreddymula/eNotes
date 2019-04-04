using System;

using Xamarin.Forms;

namespace eNote
{
    public class SquareView : Layout<View>
{
   public static readonly BindableProperty RowsProperty =
      BindableProperty.Create("Rows",
         typeof(int),
         typeof(SquareView),
         0,
         propertyChanged: (bindable, oldValue, newValue) =>
            ((SquareView)bindable).NativeSizeChanged(),
         validateValue: Validate);

    public static readonly BindableProperty ColumnsProperty =
       BindableProperty.Create("Columns",
          typeof(int),
          typeof(SquareView),
          0,
          propertyChanged: (bindable, oldValue, newValue) =>
             ((SquareView)bindable).NativeSizeChanged(),
             validateValue: Validate);

    public int Rows
    {
        get => (int)GetValue(RowsProperty);
        set => SetValue(RowsProperty, value);
    }

    public int Columns
    {
        get => (int)GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }

    private static bool Validate(BindableObject bindable, object value)
    {
        return (int)value >= 0;
    }
       
        protected override SizeRequest OnMeasure(
   double widthConstraint,
   double heightConstraint)
        {
            var w = double.IsInfinity(widthConstraint) ?
               double.MaxValue : widthConstraint;
            var h = double.IsInfinity(heightConstraint) ?
               double.MaxValue : heightConstraint;

            var square = Math.Min(w, h);
            return new SizeRequest(new Size(square, square));
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            var square = Math.Min(width / Columns, height / Rows);

            var startX = x + (width - square * Columns) / 2;
            var startY = y;

            var rect = new Rectangle(startX, startY, square, square);
            var c = 0;
            foreach (var child in Children)
            {
                LayoutChildIntoBoundingRegion(child, rect);

                if (child.IsVisible)
                {
                    rect.X += square;
                    if (++c >= Columns)
                    {
                        rect.Y += rect.Height;
                        rect.X = startX;
                        c = 0;
                    }
                }
            }
        }
    }
}

