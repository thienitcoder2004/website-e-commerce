//<script>
//    document.addEventListener("DOMContentLoaded", function () {
//    const btn = document.getElementById("chatbotBtn");
//    const box = document.getElementById("chatbotBox");
//    const input = document.getElementById("chatInput");
//    const sendBtn = document.getElementById("sendBtn");
//    const messages = document.getElementById("chatMessages");

//    // 🧠 1️⃣ KHÔI PHỤC TỪ localStorage
//    const saved = localStorage.getItem("chatHistory");
//    if (saved && saved.trim() !== "") {
//        messages.innerHTML = saved;
//    console.log("✅ Đã khôi phục lịch sử chat từ localStorage");
//    } else {
//        const hello = `<div class="bot"><b>🤖 Bot:</b> Xin chào! Mình là trợ lý HTM Shop, bạn cần hỗ trợ gì không?</div>`;
//    messages.innerHTML = hello;
//    localStorage.setItem("chatHistory", hello);
//    console.log("🚀 Tạo lời chào đầu tiên");
//    }

//    messages.scrollTop = messages.scrollHeight;

//    // 💬 2️⃣ NÚT MỞ / ẨN
//    btn.addEventListener("click", () => {
//        box.style.display = box.style.display === "flex" ? "none" : "flex";
//    });

//    // 📩 3️⃣ GỬI TIN NHẮN
//    sendBtn.addEventListener("click", sendMessage);
//    input.addEventListener("keypress", e => {
//        if (e.key === "Enter") sendMessage();
//    });

//    function appendMessage(sender, text) {
//        const div = document.createElement("div");
//    div.className = sender;
//    div.innerHTML = `<b>${sender === "user" ? "Bạn" : "🤖 Bot"}:</b> ${text}`;
//    messages.appendChild(div);
//    messages.scrollTop = messages.scrollHeight;

//    // 💾 Lưu lại
//    localStorage.setItem("chatHistory", messages.innerHTML);
//    }

//    function sendMessage() {
//        const text = input.value.trim();
//    if (!text) return;
//    appendMessage("user", text);
//    input.value = "";

//    const typing = document.createElement("div");
//    typing.className = "bot";
//    typing.textContent = "🤖 Bot đang gõ...";
//    messages.appendChild(typing);
//    messages.scrollTop = messages.scrollHeight;

//    const formData = new FormData();
//    formData.append("message", text);

//    fetch("/chat/send", {method: "POST", body: formData })
//            .then(res => res.json())
//            .then(data => {
//        typing.remove();
//    appendMessage("bot", data.reply);
//            })
//            .catch(() => {
//        typing.remove();
//    appendMessage("bot", "⚠️ Không thể kết nối đến server!");
//            });
//    }

//    // 🔁 4️⃣ ĐỒNG BỘ GIỮA CÁC TAB
//    window.addEventListener("storage", e => {
//        if (e.key === "chatHistory") {
//        messages.innerHTML = e.newValue;
//    messages.scrollTop = messages.scrollHeight;
//        }
//    });

//    // 🧷 5️⃣ LƯU LẠI KHI ĐÓNG / REFRESH
//    window.addEventListener("beforeunload", () => {
//        localStorage.setItem("chatHistory", messages.innerHTML);
//    });
//});
//</script>
