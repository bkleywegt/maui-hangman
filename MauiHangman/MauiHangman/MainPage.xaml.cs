using MauiHangman.ViewModels;

namespace MauiHangman;

public partial class MainPage : ContentPage
{
    int _count;
    private string _hangmanWord;
    private string _guessWord;
    private string _guessLetter;
    private bool _startAgain = false;
    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
        ResetGame();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GuessEntry.Focus();
    }
    
    private void OnEntryCompleted(object sender, System.EventArgs e)
    {
        PlayGame();
    }

    private void OnGuessBtnClicked(object sender, EventArgs e)
    {
        PlayGame();
    }

    private void PlayGame()
    {
        if (BindingContext is MainPageViewModel viewModel)
        {
            if (_startAgain)
            {
                ResetGame();
                GuessBtn.Text = "Guess";
                _startAgain = false;
                return;
            }
            
            if (viewModel.GuessMessage.Length != 1)
            {
                viewModel.ErrorMessage = "Please enter a single letter";
                viewModel.IsErrorVisible = true;
                return;
            }
            
            viewModel.ErrorMessage = "";
            viewModel.IsErrorVisible = false;

            _guessLetter = viewModel.GuessMessage.ToLower().Trim();
            
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
                viewModel.GameMessage = $"You got hanged!  The word was '{_hangmanWord}'";
                viewModel.IsGameMsgVisible = true;
                GuessBtn.Text = "Play Again";
                _startAgain = true;
            }
            
            if (!_guessWord.Contains("_"))
            {
                viewModel.GameMessage = "You won!";
                viewModel.IsGameMsgVisible = true;
                GuessBtn.Text = "Play Again";
                _startAgain = true;
            }
        }

        GuessEntry.Focus();
    }

    private void ResetGame()
    {
        _hangmanWord = Helpers.Helper.GetRandomLineFromEmbeddedResource("MauiHangman.Resources.wordlist.10000.txt");
        _guessWord = new string('_', _hangmanWord.Length);
        _count = 0;

        if (BindingContext is MainPageViewModel viewModel)
        {
            CreateGallows(viewModel);
        }
        else
        {
            throw new Exception("Incorrect binding context");
        }
    }
    
    private void CreateGallows(MainPageViewModel viewModel)
    {
        switch (_count)
        {
            case 0:viewModel.GallowsMessage =
                    $@"

   
   
   
   
{_guessWord}";
                break;
            case 1:viewModel.GallowsMessage =
                    $@"

|   
|   
|   
|
   
{_guessWord}";
                break;
            case 2:viewModel.GallowsMessage =
                    $@"
_______
|   
|   
|   
|
   
{_guessWord}";
                break;
            case 3:viewModel.GallowsMessage =
                $@"
_______
| /  
|/   
|   
|
   
{_guessWord}";
                break;
            case 4:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/  
|   
|
   
{_guessWord}";
                break;
            case 5:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   
|
   
{_guessWord}";
                break;
            case 6:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|    |
|
   
{_guessWord}";
                break;
            case 7:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|
|
   
{_guessWord}";
                break;
            case 8:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|-
|
    
{_guessWord}";
                break;
            case 9:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|-
|   / 

{_guessWord}";
                break;
            case 10:viewModel.GallowsMessage =
                    $@"
_______
| /  |
|/   0
|   -|-
|   / \

{_guessWord}";
                break;
        }
    }
}