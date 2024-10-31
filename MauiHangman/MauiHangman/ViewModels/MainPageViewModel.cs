using System.ComponentModel;

namespace MauiHangman.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private string _gameMessage = "";
    public string GameMessage
    {
        get => _gameMessage;
        set
        {
            _gameMessage = value;
            OnPropertyChanged(nameof(GameMessage));
        }
    }
    
    private bool _isGameMsgVisible;
    public bool IsGameMsgVisible
    {
        get => _isGameMsgVisible;
        set
        {
            _isGameMsgVisible = value;
            OnPropertyChanged(nameof(IsGameMsgVisible));
        }
    }
    
    private string _guessMessage = "";
    public string GuessMessage
    {
        get => _guessMessage;
        set
        {
            _guessMessage = value;
            OnPropertyChanged(nameof(GuessMessage));
        }
    }
    
    private string _gallowsMessage = "";
    public string GallowsMessage
    {
        get => _gallowsMessage;
        set
        {
            _gallowsMessage = value;
            OnPropertyChanged(nameof(GallowsMessage));
        }
    }
    
    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    private bool _isErrorVisible;
    public bool IsErrorVisible
    {
        get => _isErrorVisible;
        set
        {
            _isErrorVisible = value;
            OnPropertyChanged(nameof(IsErrorVisible));
        }
    }
    
    private string _winsMessage = "";
    public string WinsMessage
    {
        get => _winsMessage;
        set
        {
            _winsMessage = value;
            OnPropertyChanged(nameof(WinsMessage));
        }
    }
    
    private string _lossesMessage = "";
    public string LossesMessage
    {
        get => _lossesMessage;
        set
        {
            _lossesMessage = value;
            OnPropertyChanged(nameof(LossesMessage));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}