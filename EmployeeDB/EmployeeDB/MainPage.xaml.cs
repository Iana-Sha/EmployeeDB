using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmployeeDB
{
    public partial class MainPage : ContentPage
    {

        private string activities = "";
        private string time = "";
        private bool newItem = false;

        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        async void AddButtonClicked(object sender, EventArgs e)
        {
            

            if (string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(salaryEntry.Text))
            {
                lbl.Text = "Please enter name and salary";
            }
            
            else if (dayTime.IsChecked == false && evening.IsChecked == false)
            {
                lbl.Text = "Please select time";
            }
            else if (hockey.IsChecked == false && basketball.IsChecked == false && none.IsChecked == false)
            {
                lbl.Text = "Please select activity";
            }
            
            else
            {
                double abalbeginVal;
                bool parsed = double.TryParse(salaryEntry.Text, out abalbeginVal);

                if (parsed && abalbeginVal >= 0.0) { 
                    time += dayTime.IsChecked ? dayTime.Content.ToString() : "";
                    time += evening.IsChecked ? evening.Content.ToString() : "";
                    activities += hockey.IsChecked ? hk.Text : "";
                    activities += activities == "" ? "" : ", \n";
                    activities += basketball.IsChecked ? (bb.Text) : "";
                    activities += activities == "" ? "" : ", \n";
                    activities = none.IsChecked ? (nn.Text) : activities;

                    await App.Database.SavePersonAsync(new Employee
                    {
                        Name = nameEntry.Text,
                        Salary = Double.Parse(salaryEntry.Text),
                        Time = time,
                        Activity = activities
                    
                    });
                    newItem = true;
                }
                else
                {
                    lbl.Text = "Incorrect salary";
                }
            }
        }

        async void DisplayButtonClicked(object sender, EventArgs e)
        {
            
            if (collectionView.ItemsSource == null) {
                await DisplayAlert("Alert", "Databse is empty", "OK");
            }
            else
            {
                if (newItem)
                {
                    lbl.Text = "Employee List:";
                    nameEntry.Text = salaryEntry.Text = time = activities = string.Empty;
                    collectionView.ItemsSource = await App.Database.GetPeopleAsync();

                    activities = "";
                    time = "";

                    newItem = false;
                }
            }


        }

    }
}
