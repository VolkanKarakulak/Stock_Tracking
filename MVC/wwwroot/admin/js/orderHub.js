
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

        // HTML içinde bekleyen sipariþ sayýsýný güncelle
        document.getElementById("pendingOrdersCount").innerText = pendingOrdersCount;
        document.getElementById("todayOrdersCount").innerText = todayOrdersCount;
    });
