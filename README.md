# 💧 WaterLogger

**WaterLogger** is a lightweight ASP.NET Core Razor Pages application for tracking water intake. Users can create, update, and delete entries of water consumption based on date, quantity, and container size (e.g., glass, bottle).

---

## 📁 Project Structure

```
WaterLogger/
├── Models/
│   └── DrinkingWater.cs         # Model class for water logs
├── Pages/
│   ├── Index.cshtml             # Main page displaying logs
│   ├── Create.cshtml            # Create new entry
│   ├── Update.cshtml            # Update an existing entry
│   ├── Delete.cshtml            # Confirm deletion
│   └── Shared/
│       └── _Layout.cshtml       # Shared layout and styles
├── Program.cs                   # ASP.NET Core app entry point
├── appsettings.json             # App configuration
├── WaterLogger.db               # SQLite database file (default)
```

---

## 🚀 Features

- Add daily water intake logs
- Update or delete existing logs
- Track date, quantity (in ml/litres), and size (glass, bottle, etc.)
- Input validation with Razor Pages model binding
- Uses raw SQL with SQLite (via `Microsoft.Data.Sqlite`)

---

## ⚙️ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Rider / Visual Studio / VS Code
- SQLite installed (or bundled support via .NET)

---

## 🛠️ How to Run

1. **Clone**:
    ```bash
    git clone <your-repo-url>
    cd WaterLogger
    ```

2. **Run the app**:
    ```bash
    dotnet run
    ```

3. Open your browser and go to:
    ```
    https://localhost:your-port-number
    ```

---

## 🧩 Setting Up Your Own Database

### Step 1: Create a New SQLite Database

You can use any SQLite client (DB Browser for SQLite, etc.) or run:

```bash
sqlite3 waterLogger.db
```

Inside the SQLite prompt:

```sql
CREATE TABLE drinking_water (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Date TEXT,
    Quantity TEXT,
    Size TEXT
);
```

### Step 2: Update `appsettings.json``                      

Edit the file to point to your DB file:

```json
{
  "ConnectionStrings": {
    "ConnectionString": "Data Source=waterLogger.db"
  }
}
```

### Step 3: Run the App

```bash
dotnet run
```

WaterLogger will now use your database.

---


## ⚠️ Challenges Faced

- Integrating both frontend and backend validation in Razor Pages while ensuring a smooth and consistent user experience.
- Managing consistent formatting and parsing of dates between C# (`DateTime`) and SQLite's text-based date storage.
- Handling model binding in update/delete scenarios where data loss could occur without proper hidden field setup.
- Ensuring clean, smooth UI transitions using JavaScript for input-driven layout adjustments.
- Dealing with form validation messages and preserving input values after a failed submission using `ModelState`.

---

## 💡 Lessons Learned

- Razor Pages can handle full CRUD operations effectively when paired with careful form structure and model binding.
- Validation feedback is most effective when shown instantly, with both client- and server-side checks in place using `asp-validation-for` and `ModelState`.
- Using JavaScript to improve interactivity and form responsiveness without external libraries.
- Attention to UI/UX even in basic form interactions (e.g., resizing or highlighting on invalid input) adds a layer of polish.

---

## 

Built using ASP.NET Core Razor Pages and SQLite.
