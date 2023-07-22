using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using KeyBoard.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace KeyBoard.ViewModel 
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        KeyModel _model;
        int _speed;
        int _fails;
        int _difficulty;
        int _selectedLength;
        bool _isCaseSensitive;
        bool _isCaps = false;
        bool _isShift = false;
        bool _isStop = true;
        string _example;
        string _inputText;
        DateTime _start;
        void Notify(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public bool IsStop {
            get => _isStop;
            set {
                _isStop = value;
                Notify("IsStop");
            }
        }
        public int Speed
        {
            get { return _speed; }
            set {
                _speed = value;
                Notify("Speed");
            }
        }
        public int Fails
        {
            get { return _fails; }
            set
            {
                _fails = value;
                Notify("Fails");
            }
        }
        public int Difficulty
        {
            get { return _difficulty; }
            set
            {
                _difficulty = value;
                Notify("Difficulty");
            }
        }
        public int SelectedLength
        {
            get { return _selectedLength; }
            set
            {
                _selectedLength = value;
                Notify("SelectedLength");
            }
        }
        public bool IsCaseSensitive
        {
            get { return _isCaseSensitive; }
            set
            {
                _isCaseSensitive = value;
                Notify("IsCaseSensitive");
            }
        }
        public string Example
        {
            get { return _example; }
            set {
                _example = value;
                Notify("Example");
            }
        }
        public string InputText
        {
            get { return _inputText; }
            set
            {
                _inputText = value;
                Notify("InputText");
            }
        }
        public ObservableCollection<KeyButton> Buttons { get; set; }
        public ViewModel() {
            _model = new KeyModel();
            Difficulty = 6;
            Buttons = new ObservableCollection<KeyButton>(_model.Buttons);
            IsStop = true;
        }
        void AddInput(KeyButton button) {
            InputText = InputText.Remove(InputText.Length - 1);
            if (button.Symbol == "BackSpace")
            {
                if (InputText.Length != 0)
                {
                    InputText = InputText.Remove(InputText.Length - 1);
                    SelectedLength--;
                }
            }
            else if (button.Symbol == "Space" ||
                button.Symbol == "Enter" ||
                button.Symbol.Length == 1)
            {
                if (button.Symbol == "Space")
                    InputText += " ";
                else if (button.Symbol == "Enter")
                    InputText += "\n";
                else
                    InputText += button.Symbol;
                if (Example[SelectedLength] != InputText[SelectedLength])
                    Fails++;
                SelectedLength++;
                double sec = (DateTime.Now - _start).TotalSeconds;
                Speed = (int)(SelectedLength / sec * 60);
            }
            InputText += "|";
            if (SelectedLength == _example.Length)
            {
                IsStop = true;
            }
        }
        void KeyDown(KeyEventArgs e) {
            foreach (KeyButton button in Buttons) {
                if (e.Key == button.Code || e.SystemKey == button.Code)
                {
                    button.IsPressed = true;
                    if (e.Key == Key.CapsLock)
                        _isCaps = !_isCaps;
                    if (e.Key == Key.LeftShift ||
                        e.Key == Key.RightShift)
                        _isShift = true;
                    if(!_isStop)
                        AddInput(button);
                }
            }
            if (_isShift || _isCaps) {
                foreach (KeyButton button in Buttons) {
                    button.IsUpper = true;
                }
            }
        }
        void KeyUp(KeyEventArgs e) {
            foreach (KeyButton button in Buttons)
            {
                if (e.Key == button.Code || e.SystemKey == button.Code)
                {
                    if (e.Key == Key.CapsLock)
                    {
                        if (!_isCaps)
                            button.IsPressed = false;
                    }
                    else
                    {
                        button.IsPressed = false;
                        if (e.Key == Key.LeftShift ||
                            e.Key == Key.RightShift)
                            _isShift = false;
                    }
                }
            }
            if (!_isShift && !_isCaps)
            {
                foreach (KeyButton button in Buttons)
                {
                    button.IsUpper = false;
                }
            }
            
        }
        public ButtonCommand KeyDownCommand
        {
            get {
                return new ButtonCommand(
                     (param) => 
                     {
                         if (param is KeyEventArgs key) {
                             KeyDown(key);

                         }
                     }
                    ) ;
            }
        }
        public ButtonCommand KeyUpCommand
        {
            get
            {
                return new ButtonCommand(
                     (param) =>
                     {
                         if (param is KeyEventArgs key)
                         {
                             KeyUp(key);

                         }
                     }
                    );
            }
        }
        public ButtonCommand PrewKeyDown
        {
            get
            {
                return new ButtonCommand(
                     (param) =>
                     {
                         if (param is KeyEventArgs key)
                         {
                             if (key.Key == Key.Space ||
                             key.Key == Key.Enter ||
                             key.Key == Key.Space ||
                             key.Key == Key.Back ||
                             key.SystemKey == Key.LeftAlt||
                             key.SystemKey == Key.RightAlt)
                             {
                                 key.Handled = true;
                                 KeyDown(key);
                             }
                         }
                     }
                    );
            }
        }
        public ButtonCommand StartCommand
        {
            get {
                return new ButtonCommand(
                    (param) => {
                        Example = _model.GenerateString(Difficulty, IsCaseSensitive);
                        SelectedLength = 0;
                        InputText = "|";
                        Fails = 0;
                        Speed = 0;
                        _start = DateTime.Now;
                        IsStop = false;
                    }
                    );
            }
        }
        public ButtonCommand StopCommand
        {
            get
            {
                return new ButtonCommand(
                    (param) => {
                        IsStop = true;
                    }
                    );
            }
        }
    }
}
