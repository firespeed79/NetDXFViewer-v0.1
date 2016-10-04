﻿/*
 * Created by SharpDevelop.
 * User: michel
 * Date: 29/08/2016
 * Time: 10:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;

namespace NetDXFViewer
{
public sealed class Arc : Shape
{
    public Point Center
    {
        get { return (Point)GetValue(CenterProperty); }
        set { SetValue(CenterProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CenterProperty =
        DependencyProperty.Register("Center", typeof(Point), typeof(Arc)
        , new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsRender));


    public double StartAngle
    {
        get { return (double)GetValue(StartAngleProperty); }
        set { SetValue(StartAngleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for StartAngle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StartAngleProperty =
        DependencyProperty.Register("StartAngle", typeof(double), typeof(Arc)
        , new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public double EndAngle
    {
        get { return (double)GetValue(EndAngleProperty); }
        set { SetValue(EndAngleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for EndAngle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EndAngleProperty =
        DependencyProperty.Register("EndAngle", typeof(double), typeof(Arc)
        , new FrameworkPropertyMetadata(Math.PI/2.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public double Radius
    {
        get { return (double)GetValue(RadiusProperty); }
        set { SetValue(RadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty RadiusProperty =
        DependencyProperty.Register("Radius", typeof(double), typeof(Arc)
        , new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));



    public bool SmallAngle
    {
        get { return (bool)GetValue(SmallAngleProperty); }
        set { SetValue(SmallAngleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for SmallAngle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SmallAngleProperty =
        DependencyProperty.Register("SmallAngle", typeof(bool), typeof(Arc)
        , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));


    static Arc()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(typeof(Arc)));
    }

    protected override Geometry DefiningGeometry
    {
        get
        {
			

        	
            /*var a0 = StartAngle < 0 ? StartAngle + 2 * Math.PI : StartAngle;
            var a1 = EndAngle < 0 ? EndAngle + 2 * Math.PI : EndAngle;*/
			
            var a0 = StartAngle;
            var a1 = EndAngle;

            /*if (a1<a0)
            {
                a1 += Math.PI * 2;
                a0 += Math.PI * 2;
            }*/
            
            a0 = a0*(Math.PI / 180);
        	a1 = a1*(Math.PI / 180);

            SweepDirection d = SweepDirection.Counterclockwise;
            bool large;

            if (SmallAngle)
            {
                large = false;
                double t = a1;
                if ((a1-a0)>Math.PI)
                {
                    d = SweepDirection.Counterclockwise;
                }
                else
                {
                    d = SweepDirection.Clockwise;
                }


            }else{
                large = (Math.Abs(a1 - a0) < Math.PI);
            }

            Point p0 = Center + new Vector(Math.Cos(a0), Math.Sin(a0)) * Radius;
            Point p1 = Center + new Vector(Math.Cos(a1), Math.Sin(a1)) * Radius;
            /*Debug.WriteLine("ARC a0:"+a0.ToString());
            Debug.WriteLine("ARC a1:"+a1.ToString());*/

            List<PathSegment> segments = new List<PathSegment>(1);
            segments.Add(new ArcSegment(p1, new Size(Radius, Radius), 0.0, large, d, true));

            List<PathFigure> figures = new List<PathFigure>(1);
            PathFigure pf = new PathFigure(p0, segments, true);
            pf.IsClosed = false;
            figures.Add(pf);

            Geometry g = new PathGeometry(figures, FillRule.EvenOdd, null);
            return g;
        }
    }
}
}
