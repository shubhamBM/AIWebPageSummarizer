const summarizeBtn = document.getElementById("summarizeBtn");

const homePage = document.getElementById("homePage");
const loadingPage = document.getElementById("loadingPage");
const summaryPage = document.getElementById("summaryPage");

const loadingText = document.getElementById("loadingText");
const summaryText = document.getElementById("summaryText");

const copyBtn = document.getElementById("copyBtn");
const saveBtn = document.getElementById("saveBtn");
const backBtn = document.getElementById("backBtn");

let currentSummary = "";

// ==========================
// Summarize Button
// ==========================

summarizeBtn.addEventListener("click", () => {

    showLoading();

    window.chrome.webview.postMessage("summarize");

});

// ==========================
// Loading Screen
// ==========================

function showLoading() {

    homePage.classList.add("hidden");

    summaryPage.classList.add("hidden");

    loadingPage.classList.remove("hidden");

    loadingText.innerText = "📷 Capturing Screenshot...";

    setTimeout(() => {

        loadingText.innerText = "☁ Uploading Screenshot to Gemini...";

    }, 1200);

    setTimeout(() => {

        loadingText.innerText = "🧠 Gemini is generating summary...";

    }, 2500);

}

// ==========================
// Summary Screen
// ==========================

function showSummary(summary) {

    console.log("showSummary called");

    currentSummary = summary;

    loadingPage.classList.add("hidden");

    summaryPage.classList.remove("hidden");

    summaryText.innerText = summary;

    console.log("Summary inserted into page");

    alert("JS reached end of showSummary");

    window.chrome.webview.postMessage("summaryRendered");
}

// ==========================
// Error Screen
// ==========================

function showError(message) {

    loadingPage.classList.add("hidden");

    homePage.classList.remove("hidden");

    alert(message);

}

// ==========================
// Copy Summary
// ==========================

copyBtn.addEventListener("click", async () => {

    await navigator.clipboard.writeText(currentSummary);

    copyBtn.innerText = "✅ Copied!";

    setTimeout(() => {

        copyBtn.innerText = "📋 Copy Summary";

    }, 2000);

});

// ==========================
// Save Summary
// ==========================

saveBtn.addEventListener("click", () => {

    const blob = new Blob([currentSummary], { type: "text/plain" });

    const a = document.createElement("a");

    a.href = URL.createObjectURL(blob);

    a.download = "Summary.txt";

    a.click();

});

// ==========================
// Summarize Again
// ==========================

backBtn.addEventListener("click", () => {

    summaryPage.classList.add("hidden");

    loadingPage.classList.add("hidden");

    homePage.classList.remove("hidden");

});