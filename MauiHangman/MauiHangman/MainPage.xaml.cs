using MauiHangman.ViewModels;
using Microsoft.Maui.Storage;

namespace MauiHangman;

public partial class MainPage : ContentPage
{
    int _count;
    private string _hangmanWord;
    private string _guessWord;
    private string _guessLetter;
    private int _sessionWins = 0;
    private int _sessionLosses = 0;
    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
        SetWinsLossesMsgs();
    }

    private void OnPlayBtnClicked(object sender, EventArgs e)
    {
        PlayBtn.IsVisible = false;
        ResetGame();
    }
    
    private void OnGuessBtnClicked(object sender, EventArgs e)
    {
        if (PlayBtn.IsVisible)
        {
            return;
        }
        
        var btn = sender as Button;

        if (btn == null) return;
        PlayGame(btn.Text);
        DisableButton(btn);
    }

    private static void DisableButton(Button btn)
    {
        btn.BackgroundColor = Color.FromArgb("#D3D3D3");
        btn.TextColor = Color.FromArgb("#A9A9A9");
        btn.Opacity = 0.5;
        btn.IsEnabled = false;
    }

    private string SpaceOut(string word)
    {
        string spacedWord = "";
        foreach (char c in word)
        {
            spacedWord += c + " ";
        }

        return spacedWord;
    }
    
    private void PlayGame(string letter)
    {
        if (BindingContext is MainPageViewModel viewModel)
        {
            viewModel.ErrorMessage = "";
            viewModel.IsErrorVisible = false;

            _guessLetter = letter.ToLower().Trim();
            
            if (_hangmanWord.ToLower().Contains(_guessLetter))
            {
                for (int i = 0; i < _hangmanWord.Length; i++)
                {
                    if (_hangmanWord[i] == _guessLetter[0])
                    {
                        _guessWord = _guessWord.Remove(i, 1).Insert(i, _guessLetter);
                    }
                }
                
                viewModel.GallowsMessage = _guessWord;
            }
            else
            {
                _count++; 
            }
            
            CreateGallows(viewModel);
            viewModel.GuessMessage = "";
            
            if (_count == 10)
            {
                Preferences.Set("Losses", Preferences.Get("Losses", 0) + 1);
                _sessionLosses++;
                SetWinsLossesMsgs();
                viewModel.GameMessage = $"You got hanged!  The word was '{_hangmanWord}'";
                viewModel.IsGameMsgVisible = true;
                PlayBtn.IsVisible = true;
            }
            
            if (!_guessWord.Contains("_"))
            {
                Preferences.Set("Wins", Preferences.Get("Wins", 0) + 1);
                _sessionWins++;
                SetWinsLossesMsgs();
                viewModel.GameMessage = "You won!";
                viewModel.IsGameMsgVisible = true;
                PlayBtn.IsVisible = true;
            }
        }
    }

    private void SetWinsLossesMsgs()
    {
        if (BindingContext is MainPageViewModel viewModel)
        {
            viewModel.WinsMessage = $"Wins (AllTime) {Preferences.Get("Wins", 0)} (Session) {_sessionWins}";
            viewModel.LossesMessage = $"Losses (AllTime) {Preferences.Get("Losses", 0)} (Session) {_sessionLosses}";
        }
        else
        {
            throw new Exception("Incorrect binding context");
        }
    }
    
    private void ResetGame()
    {
        _hangmanWord = Helpers.Helper.GetRandomLineFromEmbeddedResource("MauiHangman.Resources.wordlist.10000.txt");
        _guessWord = new string('_', _hangmanWord.Length);
        _count = 0;

        if (BindingContext is MainPageViewModel viewModel)
        {
            viewModel.GameMessage = "";
            CreateGallows(viewModel);
        }
        else
        {
            throw new Exception("Incorrect binding context");
        }

        foreach (var childView in GuessesLayout.Children)
        {
            var btn = childView as Button;
            
            if (btn == null) continue;

            btn.BackgroundColor = Colors.LightGray;
            btn.TextColor = Colors.Black;
            btn.Opacity = 1;
            btn.IsEnabled = true;
        }
    }
    
    private void CreateGallows(MainPageViewModel viewModel)
    {
        switch (_count)
        {
            case 0:viewModel.GallowsMessage =
                    $@"

   
   
   
   
{SpaceOut(_guessWord)}";
                break;
            case 1:viewModel.GallowsMessage =
                    $@"

|   
|   
|   
|
   
{SpaceOut(_guessWord)}";
                break;
            case 2:viewModel.GallowsMessage =
                    $@"
_______
|   
|   
|   
|
   
{SpaceOut(_guessWord)}";
                break;
            case 3:viewModel.GallowsMessage =
                $@"
_______
| /  
|/   
|   
|
   
{SpaceOut(_guessWord)}";
                break;
            case 4:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/  
|   
|
   
{SpaceOut(_guessWord)}";
                break;
            case 5:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   
|
   
{SpaceOut(_guessWord)}";
                break;
            case 6:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|    |
|
   
{SpaceOut(_guessWord)}";
                break;
            case 7:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|
|
   
{SpaceOut(_guessWord)}";
                break;
            case 8:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|-
|
    
{SpaceOut(_guessWord)}";
                break;
            case 9:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|-
|   / 

{SpaceOut(_guessWord)}";
                break;
            case 10:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|-
|   / \

{SpaceOut(_guessWord)}";
                break;
        }
    }
}