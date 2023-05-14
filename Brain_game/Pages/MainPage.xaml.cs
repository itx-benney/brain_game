/*
   Author : @itx-benney
   ProjectName : Brain Game
   PJ Version : 1.0 (beta)
   Updated on : 14-May-2023 2:21 PM
   Description : This is a simple Maui application project in which you can 
                 answer quizes and check how many questions you passed after completed.
				 I'm a beginner and if you have any suggestions, feel free to say.
				 I'm learning and practising day afer day.I hope you will enjoy my project.
				 ThankQ  Ü
   BTW : You can edit quizes in `scripts/quizes.txt` file, Or add new ones if you have some quizes 
*/

using System;
using System.IO;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Timers;

namespace Brain_game
{
	public partial class MainPage : ContentPage
	{
		private GameManager gm; //GameManager is used to extract Quizes from file
		private Label lbl; // Used for showing question number e.g, 1/20
		private int count = 0; // question count
		private int correct = 0; // Correct answer count

		public MainPage()
		{
			InitializeComponent();
			gm = new GameManager();
			start_game.Clicked += HideWelcome;
		}
		public void HideWelcome(object o, EventArgs args)
		{
			board.Remove(welcome);
			lbl = new Label()
			{
				Text = $"{count + 1}/{gm.Questions.Count}",
				FontFamily = "fonts/FuturaPTBook.otf",
				HorizontalOptions = LayoutOptions.End,
				Margin = 5,
				FontSize = 18,
				TextColor = Colors.Red
			};
			board.Add(lbl);
			StartGame();
		}

		//Game will start here
		private void StartGame()
		{
			if (!(count >= gm.Questions.Count))
			{
				Label questionText = new Label()
				{
					Text = gm.Questions[count],
					FontFamily = "fonts/FuturaPTBook.otf",
					HorizontalOptions = LayoutOptions.Start,
					FontSize = 16,
					Margin = 5,
					TextColor = Colors.White
				};
				board.Add(questionText); // Showing question

				string[] choices = (gm.Choices[count].Split(","));
				StackLayout layout = new StackLayout()
				{
					Orientation = StackOrientation.Horizontal,
					IsClippedToBounds = true,
					HorizontalOptions = LayoutOptions.Center
				};

				Button choice1 = new Button()
				{
					Text = choices[0],
					TextColor = Colors.Black,
					FontFamily = "fonts/FuturaPTBook.otf",
					WidthRequest = 100,
					FontSize = 10,
					HeightRequest = 35,
					BackgroundColor = Colors.Cyan
				};
				layout.Add(choice1);
				Button choice2 = new Button()
				{
					Text = choices[1],
					TextColor = Colors.Black,
					FontFamily = "fonts/FuturaPTBook.otf",
					WidthRequest = 100,
					FontSize = 10,
					HeightRequest = 35,
					Margin = 3,
					BackgroundColor = Colors.Cyan
				};
				layout.Add(choice2);
				Button choice3 = new Button()
				{
					Text = choices[2],
					TextColor = Colors.Black,
					FontFamily = "fonts/FuturaPTBook.otf",
					WidthRequest = 100,
					FontSize = 10,
					HeightRequest = 35,
					BackgroundColor = Colors.Cyan
				};
				layout.Add(choice3);

				board.Add(layout);

				choice1.Clicked += delegate
				{
					choice2.IsEnabled = false;
					choice3.IsEnabled = false;
					CheckAnswer(choice1.Text, count);
				};
				choice2.Clicked += delegate
				{
					choice1.IsEnabled = false;
					choice3.IsEnabled = false;
					CheckAnswer(choice2.Text, count);
				};
				choice3.Clicked += delegate
				{
					choice1.IsEnabled = false;
					choice2.IsEnabled = false;
					CheckAnswer(choice3.Text, count);
				};
			}
			else
			{
				//Game End and thus, show results
				board.RemoveAt(0);
				ShowResults();
			}
		}

		private void CheckAnswer(string choice, int quizNum)
		{
			if (choice == gm.Answers[quizNum])
			{
				count++;
				correct++;
				Toast.MakeText("Correct!").Show(); //If Error happened, comment this line
				lbl.Text = $"{count + 1}/{gm.Questions.Count}";
				board.RemoveAt(1);
				board.RemoveAt(1);
				StartGame(); // Ask new question
			}
			else
			{
				count++;
				Toast.MakeText("Wrong!").Show(); // If Error happened, comment this line
				lbl.Text = $"{count + 1}/{gm.Questions.Count}";
				board.RemoveAt(1);
				board.RemoveAt(1);
				StartGame(); // Ask new question
			}
		}

		// Showing final results such as Correct answers and passed percentage
		private void ShowResults()
		{
			Stream image = Ui.GetAsset("assets/smile.png");
			Image img = new Image()
			{
				Source = ImageSource.FromStream(() => image),
				WidthRequest = 120,
				HeightRequest = 120
			};
			board.Add(img);
			Label txt = new Label()
			{
				Text = "Congrats! You completed answering all questions.",
				FontFamily = "fonts/FuturaPTBook.otf",
				TextColor = Colors.Yellow,
				HorizontalOptions = LayoutOptions.Center,
				Margin = 5
			};
			board.Add(txt);
			Label dtl = new Label()
			{
				Text = $"Passed : {((correct) * 100.0F) / count} %\nTotal : {count}\nCorrect Answers : {correct}",
				FontFamily = "fonts/FuturaPTBook.otf",
				HorizontalOptions = LayoutOptions.Center
			};
			board.Add(dtl);
		}
	}
}