﻿using ChildScheduler.Models;
using ChildScheduler.Services;
using ChildScheduler.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ChildScheduler.ViewModels
{
    public class SettingsViewModel : ViewModelBase<Models.Contact>
    {
        public ObservableCollection<string> ThemeOptions { get; } = new ObservableCollection<string>();
        public SettingsViewModel()
        {
            ThemeOptions.Add("Light");
            ThemeOptions.Add("Dark");

            if (DesignMode.IsDesignModeEnabled)
                return;


            if (Settings.HasDefaultThemeOption)
                ThemeOptions.Insert(0, "Device Default");


            if (ThemeOptions.Count == 3)
                SelectedTheme = (int)Settings.ThemeOption;
            else
                SelectedTheme = (int)Settings.ThemeOption - 1;
        }

        int selectedTheme;
        public int SelectedTheme
        {
            get => selectedTheme;
            set
            {
                if (SetProperty(ref selectedTheme, value))
                {
                    if (ThemeOptions.Count == 3)
                        Settings.ThemeOption = (Theme)value;
                    else
                        Settings.ThemeOption = (Theme)(value + 1);

                    //ThemeHelper.ChangeTheme(Settings.ThemeOption, true);
                }
            }
        }
    }
}

