# Backend Interview Mid Project

## 專案結構
專案遵循標準的 ASP.NET Core Web API 結構，並增加了一些額外的層次以更好地組織：

*   **`backend-interview-mid/`**: 專案主目錄。
    *   **`Controllers/`**: 包含負責處理 HTTP 請求和回應的 API 控制器。它們與服務層互動。
        *   `MyofficeAcpdController.cs`: 實現 `MyOffice_ACPD` 資料的 RESTful CRUD 操作。
    *   **`Services/`**: 包含服務層，負責抽象業務邏輯和資料存取。
        *   `IMyofficeAcpdService.cs`: 定義 `MyofficeAcpdService` 契約的介面。
        *   `MyofficeAcpdService.cs`: `MyOffice_ACPD` 資料服務的實現，與 `BackendExamHubDbContext` 互動。
    *   **`Contexts/`**: 包含 Entity Framework Core 的 DbContext。
        *   `BackendExamHubDbContext.cs`: 管理資料庫會話和查詢的 DbContext，包括 `MyOffice_ACPD` 實體映射。
    *   **`Entities/`**: 包含直接映射到資料庫表的 Entity Framework Core 實體類別。
        *   `MyOfficeAcpd.cs`: 代表 `MyOffice_ACPD` 表的模型。
        *   `MyOfficeExcuteionLog.cs`: 另一個實體模型。
    *   **`Properties/`**: 包含專案級屬性，包括用於啟動配置的 `launchSettings.json`。
    *   **`appsettings.json`, `appsettings.Development.json`**: 應用程式的配置檔案，包括資料庫連接字串。
    *   **`Program.cs`**: 應用程式的入口點，負責配置服務、HTTP 請求管道，並設置 Entity Framework Core 和 Swagger。
    *   **`.csproj`**: 專案檔案，列出依賴項和專案設定。

## 設定說明

### 資料庫設定
1.  **連接字串 (Connection String)**：請確保您的資料庫連接字串在 `appsettings.json` 中配置正確。

### NuGet 套件
所有必要的 NuGet 套件在 IDE 中建置專案或運行 `dotnet restore` 時，通常會自動恢復。主要套件包括：
*   `Microsoft.EntityFrameworkCore.SqlServer`
*   `Microsoft.EntityFrameworkCore.Design`
*   `Swashbuckle.AspNetCore`

## 運行應用程式

1.  **打開專案**：在您偏好的 IDE 中打開 `backend-interview-mid.slnx 解決方案檔案，或在 VS Code 中打開 `backend-interview-mid` 資料夾。
2.  **建置專案 (Build the project)**：
    ```bash
    dotnet build
    ```
    (您的 IDE 通常會自動執行此操作)。
3.  **運行應用程式 (Run the application)**：
    ```bash
    dotnet run
    ```
    或直接從您的 IDE 運行 (例如，在 Visual Studio 中按 F5)。

應用程式通常會在 `https://localhost:7XXX` (其中 7XXX 是端口號) 啟動。您可以在 `Properties/launchSettings.json` 中找到確切的 URL。

## API 端點 (Swagger UI)

應用程式運行後，您可以訪問 Swagger UI 來查看和測試 API 端點：

*   **Swagger UI URL**：`https://localhost:7XXX/swagger` (將 7XXX 替換為您應用程式的端口號)。

### 測試 `MyOffice_ACPD` 的 CRUD 操作

`MyofficeAcpdController` 提供了以下 RESTful 端點：

*   **`GET /api/myofficeacpd`**：檢索所有 `MyOffice_ACPD` 記錄。
*   **`GET /api/myofficeacpd/{id}`**：根據 `AcpdSid` 檢索單一 `MyOffice_ACPD` 記錄。
*   **`POST /api/myofficeacpd`**：創建新的 `MyOffice_ACPD` 記錄。
    *   **注意**：`AcpdSid` 將由服務在創建時自動生成。
    *   範例請求主體：Swagger UI 將提供架構。您應為 `AcpdCname`、`AcpdEname`、`AcpdEmail` 等提供值。
*   **`PUT /api/myofficeacpd/{id}`**：根據 `AcpdSid` 更新現有的 `MyOffice_ACPD` 記錄。
    *   在 URL 中提供 `AcpdSid`，並在請求主體中提供更新後的 `MyOffice_ACPD` 物件。確保主體中的 `AcpdSid` 與 URL 中的匹配。
*   **`DELETE /api/myofficeacpd/{id}`**：根據 `AcpdSid` 刪除 `MyOffice_ACPD` 記錄。

Swagger UI 中的每個 API 端點都附有預期的請求格式和可能的響應代碼說明 (200 OK, 201 Created, 204 No Content, 400 Bad Request, 404 Not Found, 500 Internal Server Error)。