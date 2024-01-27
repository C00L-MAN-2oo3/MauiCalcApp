using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Expression = NCalc.Expression;

namespace MauiCalcApp
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _mathematicalExpression = String.Empty;
        private double _result;

        [RelayCommand]
        private void Print(string buttonValue)
        {
            MathematicalExpression += buttonValue;

            if (MathematicalExpression.StartsWith(".") || MathematicalExpression.StartsWith("+")
                || MathematicalExpression.StartsWith("÷") || MathematicalExpression.StartsWith("×"))
                MathematicalExpression = String.Empty;

            RemoveDuplicateCharacters();
        }

        [RelayCommand]
        private void CalculatePercent()
            => MathematicalExpression = (Convert.ToDouble(MathematicalExpression) / 100).ToString();

        [RelayCommand]
        private void ClearText()
        {
            if (MathematicalExpression.Length == 0)
                return;

            MathematicalExpression = String.Empty;
        }

        [RelayCommand]
        private void DeleteItem()
        {
            if (MathematicalExpression.Length == 0)
                return;

            MathematicalExpression = MathematicalExpression.Remove(MathematicalExpression.Length - 1);
        }

        [RelayCommand]
        private void GetResult()
        {
            string modifiedMathematicalExpression = MathematicalExpression
                .Replace("÷", "/")
                .Replace("×", "*")
                .Replace("–", "-");

            if (modifiedMathematicalExpression.Length == 0)
                MathematicalExpression = _result.ToString();
            else
            {
                try
                {
                    _result = Convert.ToDouble(new Expression(modifiedMathematicalExpression).Evaluate());
                    MathematicalExpression = _result.ToString();
                }
                catch (Exception)
                {
                    MathematicalExpression = "Error!";
                }
            }
        }

        private void RemoveDuplicateCharacters()
        {
            string[] duplicateCharacters = { "––", "–+", "–÷", "–×", "–.", "++", "+–", "+÷",
                                             "+×", "+.", "×–", "×+", "××", "×÷", "×.", "÷×",
                                             "÷÷", "÷–", "÷+", "÷." };

            foreach (string character in duplicateCharacters)
                MathematicalExpression = MathematicalExpression.Replace(character, character.Remove(character.Length - 1));
        }
    }
}
