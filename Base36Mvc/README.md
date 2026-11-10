# Razor + HtmlHelper 36 進制轉換

使用 .NET 10 SDK，在此目錄執行：

```powershell
dotnet run --urls http://localhost:5080
```

開啟 http://localhost:5080 。

流程：View 的 HtmlHelper 表單 → Controller 模型繫結與 ModelState 驗證 → Model.Calculate() 運算 → Controller 將 Model 傳回 Razor View。

- `Models/Base36Model.cs`：Class 屬性以 Display、StringLength、RegularExpression 驗證；IValidatableObject 檢查至少輸入一欄。BigInteger 避免 100 位輸入溢位。
- `Controllers/Base36Controller.cs`：GET 顯示表單；POST 驗證後委派 Model 計算。包含防偽權杖驗證。
- `Views/Base36/Index.cshtml`：使用 BeginForm、LabelFor、TextBoxFor、ValidationMessageFor、ValidationSummary 等 HtmlHelper，顯示伺服器端驗證訊息與兩種結果。

10 → 36：反覆除以 36，將餘數映射至 0–9、A–Z，再反轉順序。
36 → 10：由左至右累積 `結果 = 結果 * 36 + 字元數值`。

例：123456 ↔ 2N9C、35 ↔ Z、36 ↔ 10、0 ↔ 0。接受小寫與前導零，輸出為標準大寫且移除多餘前導零。不接受負數、小數或符號。

題目的 MinimumLength = 6 視為屬性語法範例；本例用 1，讓單一數字也能轉換。若作業強制至少六碼，可將兩個 StringLength 的 MinimumLength 改為 6。
