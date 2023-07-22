using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyBoard.Model
{
    class KeyModel
    {
        public List<KeyButton> Buttons { get; set; }
        List<Color> colors;
        string symbols = "tgbyhnrfvujmedcik,wsxol.qazp;/345678`1290-=[]\'\\";
        string Upsymbols = "TGBYHNRFVUJMEDCIK<WSXOL>QAZP:?#$%^&*~!@()_+{}\"|";
        public KeyModel() {
            colors = new List<Color>() {
                Color.FromRgb(98, 124, 189),
                Color.FromRgb(111, 199, 193),
                Color.FromRgb(98, 189, 151),
                Color.FromRgb(127, 189, 98),
                Color.FromRgb(187, 189, 98),
                Color.FromRgb(189, 152, 98),
                Color.FromRgb(189, 109, 98),
                Color.FromRgb(189, 98, 180),
                Color.FromRgb(143, 98, 189)
            };
            FillButtons();
        }
        void FillButtons() {
            Buttons = new List<KeyButton>() {
                new KeyButton(Key.OemTilde, 1,colors[0],"~","`"),
                new KeyButton(Key.D1, 1,colors[0],"!","1"),
                new KeyButton(Key.D2, 1,colors[1],"@","2"),
                new KeyButton(Key.D3, 1,colors[2],"#","3"),
                new KeyButton(Key.D4, 1,colors[3],"$","4"),
                new KeyButton(Key.D5, 1,colors[3],"%","5"),
                new KeyButton(Key.D6, 1,colors[4],"^","6"),
                new KeyButton(Key.D7, 1,colors[4],"&","7"),
                new KeyButton(Key.D8, 1,colors[5],"*","8"),
                new KeyButton(Key.D9, 1,colors[6],"(","9"),
                new KeyButton(Key.D0, 1,colors[7],")","0"),
                new KeyButton(Key.OemMinus, 1,colors[7],"_","-"),
                new KeyButton(Key.OemPlus, 1,colors[7],"+","="),
                new KeyButton(Key.Back, 2,colors[7],"BackSpace"),
                new KeyButton(Key.Tab, 1.5,colors[0],"Tab"),
                new KeyButton(Key.Q, 1,colors[0],"Q"),
                new KeyButton(Key.W, 1,colors[1],"W"),
                new KeyButton(Key.E, 1,colors[2],"E"),
                new KeyButton(Key.R, 1,colors[3],"R"),
                new KeyButton(Key.T, 1,colors[3],"T"),
                new KeyButton(Key.Y, 1,colors[4],"Y"),
                new KeyButton(Key.U, 1,colors[4],"U"),
                new KeyButton(Key.I, 1,colors[5],"I"),
                new KeyButton(Key.O, 1,colors[6],"O"),
                new KeyButton(Key.P, 1,colors[7],"P"),
                new KeyButton(Key.OemOpenBrackets, 1,colors[7],"{","["),
                new KeyButton(Key.OemCloseBrackets, 1,colors[7],"}","]"),
                new KeyButton(Key.OemPipe, 1.5,colors[7],"|","\\"),
                new KeyButton(Key.CapsLock, 2,colors[0],"Caps Lock"),
                new KeyButton(Key.A, 1,colors[0],"A"),
                new KeyButton(Key.S, 1,colors[1],"S"),
                new KeyButton(Key.D, 1,colors[2],"D"),
                new KeyButton(Key.F, 1,colors[3],"F"),
                new KeyButton(Key.G, 1,colors[3],"G"),
                new KeyButton(Key.H, 1,colors[4],"H"),
                new KeyButton(Key.J, 1,colors[4],"J"),
                new KeyButton(Key.K, 1,colors[5],"K"),
                new KeyButton(Key.L, 1,colors[6],"L"),
                new KeyButton(Key.OemSemicolon, 1,colors[7],":",";"),
                new KeyButton(Key.OemQuotes, 1,colors[7],"\"","\'"),
                new KeyButton(Key.Enter, 2,colors[7],"Enter"),
                new KeyButton(Key.LeftShift, 2.5,colors[0],"Shift"),
                new KeyButton(Key.Z, 1,colors[0],"Z"),
                new KeyButton(Key.X, 1,colors[1],"X"),
                new KeyButton(Key.C, 1,colors[2],"C"),
                new KeyButton(Key.V, 1,colors[3],"V"),
                new KeyButton(Key.B, 1,colors[3],"B"),
                new KeyButton(Key.N, 1,colors[4],"N"),
                new KeyButton(Key.M, 1,colors[4],"M"),
                new KeyButton(Key.OemComma, 1,colors[5],"<",","),
                new KeyButton(Key.OemPeriod, 1,colors[6],">","."),
                new KeyButton(Key.OemQuestion, 1,colors[7],"?","/"),
                new KeyButton(Key.RightShift, 2.5,colors[7],"Shift"),
                new KeyButton(Key.LeftCtrl, 1.6,colors[0],"Ctrl"),
                new KeyButton(Key.LWin, 1.5,colors[0],"Win"),
                new KeyButton(Key.LeftAlt, 1.5,colors[0],"Alt"),
                new KeyButton(Key.Space, 6,colors[8],"Space"),
                new KeyButton(Key.RightAlt, 1.5,colors[7],"Alt"),
                new KeyButton(Key.RWin, 1.5,colors[7],"Win"),
                new KeyButton(Key.RightCtrl, 1.6,colors[7],"Ctrl")
            };
        }
        public string GenerateString(int difficulty, bool isUpper) {
            string used = symbols.Substring(0, difficulty);
            if (isUpper)
                used += Upsymbols.Substring(0, difficulty);
            string result = "";
            Random rand = new Random();
            int space = rand.Next(10);
            for (int i = 0; i < 30; i++) {
                for (int j = 0; j < space; j++) {
                    result += used[rand.Next(used.Length - 1)];
                }
                result += " ";
                space = rand.Next(10);
            }
            result = result.Remove(result.Length - 1);
            return result;
        }
    }
}
