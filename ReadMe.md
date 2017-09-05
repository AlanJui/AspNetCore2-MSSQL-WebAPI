# 操作指引

本 Web API 系統特性：
 - ASP.NET Core 2.0
 - 採用 RESTful Web API Service 標準
 - 搭配 DevExtreme 17.1

為能正常操作以下「步驟」，請依據「事前準備」章節，備妥應安裝之軟體套件及資料庫。

## 編譯及執行程式

### （1）下載專案檔案
```
$ git clone https://github.com/AlanJui/AspNetCore2-MSSQL-WebAPI
```

### （2）進入系統之專案資料夾
```
$ cd AspNetCore2-MSSQL-WebAPI
```

### （3）安裝系統所需之 ASP.NET Core 套件
```
$ dotnet restore
```

### （4）編譯與組建（Build）系統
```
$ dotnet build
```

### （5）啟動系統
```
$ dotnet run
```

### （6）驗證系統已正常啟動
使用 Web 瀏覽器，瀏覽網址： http://127.0.0.1:5000/api/orders


## 事前準備

### 編譯及執行程式

 - Git
 - .NET Core SDK (參考： https://www.microsoft.com/net/core )
 - NodeJS & NPM
 - Bower

### 資料庫

由於範例使用「北風貿易公司」的資料，請於程式執行前，先將 Northwind 資料，Restore 到 MS SQL Server 的資料庫之中。


