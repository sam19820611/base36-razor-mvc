using Base36Mvc.Models;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

void Check(bool passed, string name)
{
    if (!passed) throw new Exception(name);
    Console.WriteLine($"PASS: {name}");
}

foreach (var (dec, b36) in new[] { ("0", "0"), ("35", "Z"), ("36", "10"), ("123456", "2N9C") })
{
    var model = new Base36Model { DecimalInput = dec, Base36Input = b36.ToLowerInvariant() };
    model.Calculate();
    Check(model.Base36Result == b36 && model.DecimalResult == dec, $"{dec} <-> {b36}");
}
var leading = new Base36Model { DecimalInput = "00036", Base36Input = "000z" };
leading.Calculate();
Check(leading.Base36Result == "10" && leading.DecimalResult == "35", "Leading zeros");
var large = new Base36Model { DecimalInput = new string('9', 100) };
large.Calculate();
var reverse = new Base36Model { Base36Input = large.Base36Result };
reverse.Calculate();
Check(reverse.DecimalResult == large.DecimalInput, "100-digit round trip");
var max36 = new Base36Model { Base36Input = new string('Z', 100) };
max36.Calculate();
Check(max36.DecimalResult == (BigInteger.Pow(36, 100) - 1).ToString(), "100-digit base36");
foreach (var model in new[] {
    new Base36Model(),
    new Base36Model { DecimalInput = " " },
    new Base36Model { DecimalInput = "-1" },
    new Base36Model { DecimalInput = "1.2" },
    new Base36Model { DecimalInput = "１２" },
    new Base36Model { Base36Input = "A!" },
    new Base36Model { Base36Input = "A\n" },
    new Base36Model { DecimalInput = new string('1', 101) },
    new Base36Model { Base36Input = new string('Z', 101) }
})
{
    Check(!Validator.TryValidateObject(model, new ValidationContext(model), new List<ValidationResult>(), true), "Reject invalid input");
}
