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



summarizeBtn.addEventListener("click", () => {

    showLoading();

    requestAnimationFrame(() => {

        window.chrome.webview.postMessage("Summarize");

    });

});


function showLoading() {

    homePage.classList.add("hidden");

    summaryPage.classList.add("hidden");

    loadingPage.classList.remove("hidden");
}



function updateLoadingState(state) {

    switch (state) {

        case "Capturing":

            loadingText.innerText = "📷 Capturing Screenshot...";
            break;

        case "Uploading":

            loadingText.innerText = "☁ Uploading Screenshot to Gemini...";
            break;

        case "Processing":

            loadingText.innerText = "🧠 Gemini is generating summary...";
            break;

        case "Completed":

            loadingText.innerText = "✅ Summary Generated";
            break;

        default:

            loadingText.innerText = state;
            break;
    }
}


function showSummary(summary) {

    currentSummary = summary;

    updateLoadingState("Completed");

    loadingPage.classList.add("hidden");

    summaryPage.classList.remove("hidden");

    summaryText.innerText = summary;

    window.chrome.webview.postMessage("summaryRendered");
}



function showError(message) {

    loadingPage.classList.add("hidden");

    homePage.classList.remove("hidden");

    alert(message);

}


copyBtn.addEventListener("click", async () => {

    await navigator.clipboard.writeText(currentSummary);

    copyBtn.innerText = "✅ Copied!";

    setTimeout(() => {

        copyBtn.innerText = "📋 Copy Summary";

    });

});


saveBtn.addEventListener("click", () => {

    const blob = new Blob([currentSummary], { type: "text/plain" });

    const a = document.createElement("a");

    a.href = URL.createObjectURL(blob);

    a.download = "Summary.txt";

    a.click();

});


backBtn.addEventListener("click", () => {

    summaryPage.classList.add("hidden");

    loadingPage.classList.add("hidden");

    homePage.classList.remove("hidden");

});