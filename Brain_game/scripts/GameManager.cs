using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
namespace Brain_game;

public class GameManager
{
	private string path = "scripts/quizes.txt";
	public List<string> Questions = new List<string>();
	public List<string> Answers = new List<string>();
	public List<string> Choices = new List<string>();

	public GameManager()
	{
		try
		{
			Questions = ReadQuestions();
			Answers = ReadAnswers();
			Choices = ReadChoices();
		}
		catch (Exception)
		{
			//Don't rename or delete the quizes.txt file
			//If you did it, plz change the `path` variable
			Toast.MakeText("Load Error!").Show();
		}
	}

	public List<string> ReadQuestions()
	{
		List<string> questions = new List<string>();
		string[] linesInQuizes = File.ReadAllLines(path);
		foreach (string line in linesInQuizes)
		{
			string[] quiz = line.Split("||"); //Split the quiz components
			questions.Add(quiz[0]); //Get the question i.e, first element
		}
		return questions;
	}
	public List<string> ReadAnswers()
	{
		List<string> answers = new List<string>();
		string[] linesInQuizes = File.ReadAllLines(path);
		foreach (string line in linesInQuizes)
		{
			string[] quiz = line.Split("||");
			answers.Add(quiz[2]); //Get the third element i.e, Answer
		}
		return answers;
	}
	public List<string> ReadChoices()
	{
		List<string> choices = new List<string>();
		string[] linesInQuizes = File.ReadAllLines(path);
		foreach (string line in linesInQuizes)
		{
			string[] quiz = line.Split("||");
			choices.Add(quiz[1]); //Get the choices i.e, second element
		}
		return choices;
	}
}
