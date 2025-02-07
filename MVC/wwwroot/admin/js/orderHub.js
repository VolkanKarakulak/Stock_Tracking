
    // SignalR baðlantýsýný baþlat
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7260/orderHub") // OrderHub adresi
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.start().catch(err => console.error(err.toString()));

    // Yeni sipariþ alýndýðýnda güncelleme yap
        connection.on("ReceiveOrder", (order, pendingOrdersCount, todayOrdersCount) => {
        console.log("New Order Received:", order);
        console.log("Updated Pending Orders Count:", pendingOrdersCount);
        console.log("Today's Orders Count:", todayOrdersCount);

            const pendingOrdersElement = document.getElementById("pendingOrdersCount");
            const todayOrdersElement = document.getElementById("todayOrdersCount");
            const orderWidget = document.getElementById("orderWidget");

            // Sayýyý güncelle
            pendingOrdersElement.innerText = pendingOrdersCount;
            todayOrdersElement.innerText = todayOrdersCount;

            // Efekti ekle
            pendingOrdersElement.classList.add("glow");
            todayOrdersElement.classList.add("glow");
            orderWidget.classList.add("highlight");

            // Belirli bir süre sonra efekti kaldýr
            setTimeout(() => {
                pendingOrdersElement.classList.remove("glow");
                todayOrdersElement.classList.remove("glow");
                orderWidget.classList.remove("highlight");
            }, 1200);
        });