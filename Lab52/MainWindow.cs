using System;
using Gtk;
using Cairo;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnDrawGraphicClicked (object sender, EventArgs e)
	{
		Area.GdkWindow.Clear ();

		DrawGrid ();

		var canvas = Gdk.CairoHelper.Create(Area.GdkWindow);

		canvas.SetSourceRGB (0.7, 0.2, 0.0);

		canvas.MoveTo (0, 50);

		for (double i = 0; i < Convert.ToDouble (Area.WidthRequest); i++)
			canvas.LineTo (i, 20*Math.Log (i+1)+50); 

		canvas.Stroke ();
	}

	protected void DrawGrid ()
	{
		var grid = Gdk.CairoHelper.Create(Area.GdkWindow);

		grid.SetSourceRGBA (0.2, 0.23, 0.9, 0.3); // Red Green Blue Alpha, выставляем цвет и прозрачность

		for (int i = 20; i < Area.WidthRequest; i += 20)
		{
			grid.MoveTo ((double) i, 0); // Переходим на позицию (i, 0), не рисуя линию
			grid.LineTo ((double) i, (double) Area.HeightRequest); // Рисуем вертикальную линию от 0 до Area.HeightRequest
		}

		grid.Stroke (); // Само рисование линий

		for (int j = 30; j < Area.HeightRequest; j += 20)
		{
			grid.MoveTo (0, (double) j);
			grid.LineTo ((double) Area.WidthRequest, (double) j);
		}

		grid.Stroke ();
	}

	protected void OnAreaExposeEvent (object o, ExposeEventArgs args)
	{
		DrawGrid ();
	}
}

