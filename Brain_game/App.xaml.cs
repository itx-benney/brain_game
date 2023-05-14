using System;
using System.IO;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Brain_game
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new MainPage();
		}
	}
}