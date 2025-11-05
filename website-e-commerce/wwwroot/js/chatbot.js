document.addEventListener("DOMContentLoaded", function () {
    const btn = document.getElementById("chatbotBtn");
    const box = document.getElementById("chatbotBox");
    const input = document.getElementById("chatInput");
    const sendBtn = document.getElementById("sendBtn");
    const messages = document.getElementById("chatMessages");

    btn.addEventListener("click", () => {
        box.style.display = box.style.display === "flex" ? "none" : "flex";
    });

    sendBtn.addEventListener("click", sendMessage);
    input.addEventListener("keypress", (e) => {
        if (e.key === "Enter") sendMessage();
    });

    function appendMessage(sender, text) {
        const div = document.createElement("div");
        div.className = sender;
        div.innerHTML = `<b>${sender === "user" ? "Bạn" : "🤖 Bot"}:</b> ${text}`;
        messages.appendChild(div);
        messages.scrollTop = messages.scrollHeight;
    }

    function sendMessage() {
        const text = input.value.trim();
        if (!text) return;

        appendMessage("user", text);
        input.value = "";

        const formData = new FormData();
        formData.append("message", text);

        // Hiệu ứng bot đang gõ
        const typing = document.createElement("div");
        typing.className = "bot";
        typing.textContent = "🤖 Bot đang gõ...";
        messages.appendChild(typing);

        fetch("/chat/send", { method: "POST", body: formData })
            .then(res => res.json())
            .then(data => {
                typing.remove();
                appendMessage("bot", data.reply);
            })
            .catch(() => {
                typing.remove();
                appendMessage("bot", "⚠️ Lỗi kết nối server!");
            });
    }
});
