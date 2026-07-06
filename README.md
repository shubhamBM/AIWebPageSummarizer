# 🤖 AI Web Page Summarizer

An AI-powered desktop application built using **C#, WPF, WebView2, HTML/CSS/JavaScript, and Google Gemini Vision API**.

---

# 📌 Features

- 📸 Capture the current screen
- 🤖 AI-powered screenshot analysis using Gemini
- 📄 Generate intelligent summaries
- ⏳ Loading animation while AI processes
- 📋 Copy summary
- 💾 Save summary as TXT
- 🔄 Summarize again
- 🎨 Modern responsive UI

---

# 🛠️ Technologies Used

- C#
- WPF
- WebView2
- HTML
- CSS
- JavaScript
- Google Gemini API
- .NET 8

---

# 📂 Project Structure

```
AIWebPageSummarizer
│
├── Assets
│   ├── index.html
│   ├── style.css
│   └── script.js
│
├── Services
│   ├── AIService.cs
│   └── ScreenshotService.cs
│
├── Models
│
├── MainWindow.xaml
├── MainWindow.xaml.cs
└── appsettings.json
```

---

# 🚀 Getting Started

## Clone Repository

```bash
git clone https://github.com/shubhamBM/AIWebPageSummarizer.git
```

Open the project in **Visual Studio 2022**.

Install NuGet packages.

Add your Gemini API Key in

```
appsettings.json
```

```json
{
  "Gemini": {
    "ApiKey": "YOUR_API_KEY"
  }
}
```

Run the application.

---

# 📈 Future Improvements

- Export Summary as PDF
- Dark Mode
- History of Summaries
- Capture only the WebView instead of the whole screen
- Multiple AI Model Support

---

# 👨‍💻 Author

**Shubham Batrakhaye**

GitHub:
https://github.com/shubhamBM
