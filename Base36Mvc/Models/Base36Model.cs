using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Base36Mvc.Models;

public class Base36Model : IValidatableObject
{
    private const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [Display(Name = "十進制數字")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} 的長度必須介於 {2}～{1} 個字元。")]
    [RegularExpression(@"[0-9]+", ErrorMessage = "十進制僅能輸入 0～9 的非負整數。")]
    public string? DecimalInput { get; set; }

    [Display(Name = "三十六進制英數")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} 的長度必須介於 {2}～{1} 個字元。")]
    [RegularExpression(@"[0-9A-Za-z]+", ErrorMessage = "三十六進制僅能輸入 0～9、A～Z 或 a～z。")]
    public string? Base36Input { get; set; }

    public string? Base36Result { get; private set; }
    public string? DecimalResult { get; private set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(DecimalInput) && string.IsNullOrWhiteSpace(Base36Input))
            yield return new ValidationResult("請至少填寫一個欄位。");
    }

    public void Calculate()
    {
        // 即使由 Controller 以外的程式呼叫，也先驗證資料。
        Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        Base36Result = null;
        DecimalResult = null;

        if (!string.IsNullOrEmpty(DecimalInput))
        {
            var number = BigInteger.Parse(DecimalInput, CultureInfo.InvariantCulture);
            var result = new StringBuilder();
            do
            {
                number = BigInteger.DivRem(number, 36, out var remainder);
                result.Append(Digits[(int)remainder]);
            } while (number > 0);
            Base36Result = new string(result.ToString().Reverse().ToArray());
        }

        if (!string.IsNullOrEmpty(Base36Input))
        {
            BigInteger number = 0;
            foreach (var character in Base36Input.ToUpperInvariant())
                number = number * 36 + Digits.IndexOf(character);
            DecimalResult = number.ToString(CultureInfo.InvariantCulture);
        }
    }
}
