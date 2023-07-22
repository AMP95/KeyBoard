using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Input;

namespace KeyBoard.Model
{
    class KeyButton : INotifyPropertyChanged
    {
        public Key Code { get; }
        public string Symbol { get; private set; }
        public double WidthCoef { get; }
        public SolidColorBrush Background { get; }

        string _upper;
        string _lower;
        bool _isPressed;
        bool _isUpper;
        Color _backgroundColor;

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsPressed
        {
            get {
                return _isPressed;
            }
            set {
                _isPressed = value;
                if (_isPressed)
                    Background.Color = _backgroundColor;
                else
                    Background.Color = Color.FromRgb(200,200,200);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Background"));
            }
        }
        public bool IsUpper
        {
            get
            {
                return _isUpper ;
            }
            set
            {
                _isUpper = value;
                if (_isUpper)
                    Symbol = _upper;
                else
                    Symbol = _lower;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Symbol"));
            }
        }
        public KeyButton(Key code, double widthCoef, Color color, string upper, string lower = "") {
            Code = code;
            _upper = upper;
            if (_upper.Length > 1)
                _lower = _upper;
            else {
                _lower = _upper.ToLower();
                if (_upper == _lower)
                    _lower = lower;
            }
            WidthCoef = widthCoef;
            _backgroundColor = color;
            Background = new SolidColorBrush();
            IsPressed = false;
            IsUpper = false;
        }
    }
}
