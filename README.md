# Base36 Razor MVC

C# ASP.NET Core MVC 範例：使用 Razor 與 HtmlHelper 進行十進制和三十六進制雙向轉換。

## 執行

需安裝 .NET 10 SDK。

```powershell
dotnet run --project Base36Mvc --urls http://localhost:5080
```

開啟 http://localhost:5080 。

## 功能

- 兩個輸入欄位，可擇一或同時轉換。
- Model 使用 Data Annotations 進行長度及字元驗證。
- Controller 驗證後交由 Model 運算，再回傳 Razor View。
- 支援 1～100 字元的非負整數，使用 BigInteger 避免溢位。
- 三十六進制接受大小寫，輸出統一大寫。
- 範例：`123456` ↔ `2N9C`。

## 檢查

```powershell
dotnet build Base36Mvc
dotnet run --project Base36Mvc.Checks
```

詳細設計見 [專案說明](Base36Mvc/README.md)。
