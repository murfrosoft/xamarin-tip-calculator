using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TipCalculator
{
	public class HomePage : ContentPage
	{

        Slider tipSlider;
        Label tipCost;
        Label totalCost;

        Label roundTipCost;
        Label roundTotalCost;

        Entry mealCost;

        public HomePage()
        {
            /*
            Text
            Enter Meal Cost
            [  Entry   ]

            Select Base Tip Percentage
            0 ---------X--------- 100

            Tip Amount: $ X.XX
            Total Amount: $ X.XX

            */

            Label title = new Label
            {
                Text = "Tip Calculator",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 1.5 * Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            Label enterCost = new Label
            {
                Text = "Enter Meal Cost:",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            mealCost = new Entry
            {
                Placeholder = "Enter Meal Cost",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Entry)),
                WidthRequest = 350,
                Keyboard = Keyboard.Numeric
            };
            mealCost.TextChanged += MealCost_TextChanged;

            BoxView box1 = new BoxView
            {
                HeightRequest = 40
            };
            BoxView box2 = new BoxView
            {
                HeightRequest = 40
            };
            BoxView box3 = new BoxView
            {
                HeightRequest = 10
            };

            #region Slider Code

            tipSlider = new Slider
            {
                Maximum = 50,
                Minimum = 1,
                Value = 15,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            tipSlider.ValueChanged += OnSliderValueChanged;

            Label percentageLabel1 = new Label
            {
                Text = "Percentage: ",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label percentageLabel2 = new Label
            {
                Text = "%",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label lengthLabel = new Label
            {
                // Text is bound to an slider object
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.DarkSeaGreen,
                HorizontalOptions = LayoutOptions.Start,
                BindingContext = tipSlider,
            };
            lengthLabel.SetBinding(Label.TextProperty, "Value");

            StackLayout labelRow = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 20,
                Children = { percentageLabel1, lengthLabel, percentageLabel2 }
            };
            #endregion

            tipCost = new Label
            {
                Text = "Tip Cost: $0.00",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            totalCost = new Label
            {
                Text = "Meal Cost: $0.00",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            roundTipCost = new Label
            {
                Text = "Rounding the tip: Tip ($0.00) Total ($0.00)",
                TextColor = Color.DarkGray,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };

            roundTotalCost = new Label
            {
                Text = "Rounding the total: Tip ($0.00) Total ($0.00)",
                TextColor = Color.DarkGray,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };




            this.Padding = 30;

            Content = new StackLayout {
				Children = {
					title, enterCost, mealCost, box1, labelRow, tipSlider, box2, tipCost, totalCost, box3, roundTipCost, roundTotalCost
                }
			};
		}

        private void MealCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            double cost;
            if( double.TryParse(mealCost.Text, out cost) )
            {
                double tip = cost * (tipSlider.Value / 100.0);
                double total = cost + tip;

                double rtip = Math.Round(tip);
                double rtotal = cost + rtip;

                double rTotal = Math.Round(total);
                double rTip = rTotal - cost;

                tipCost.Text = $"Tip Cost: {tip:C}";
                totalCost.Text = $"Total Cost: {total:C}";
                roundTipCost.Text = $"Rounding the tip: Tip ({rtip:C}) Total ({rtotal:C})";
                roundTotalCost.Text = $"Rounding the total: Tip ({rTip:C}) Total ({rTotal:C})";
            }
            
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var StepValue = 1.0;
            var newStep = Math.Round(e.NewValue / StepValue);

            tipSlider.Value = newStep * StepValue;

            double cost;
            if (double.TryParse(mealCost.Text, out cost))
            {
                double tip = cost * (tipSlider.Value / 100.0);
                double total = cost + tip;

                double rtip = Math.Round(tip);
                double rtotal = cost + rtip;

                double rTotal = Math.Round(total);
                double rTip = rTotal - cost;

                tipCost.Text = $"Tip Cost: {tip:C}";
                totalCost.Text = $"Total Cost: {total:C}";
                roundTipCost.Text = $"Rounding the tip: Tip ({rtip:C}) Total ({rtotal:C})";
                roundTotalCost.Text = $"Rounding the total: Tip ({rTip:C}) Total ({rTotal:C})";
            }
        }
    }
}