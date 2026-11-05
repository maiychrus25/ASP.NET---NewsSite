# Sổ tay ASP.NET Core MVC và bài thực hành NewsSite

Tài liệu dành cho anh mới bắt đầu, nhằm giúp anh **hiểu để tự giải thích và tự làm bài kiểm tra**, thay vì chỉ chép code. Các ví dụ dùng ASP.NET Core MVC trên .NET 10, tương ứng project NewsSite của anh.

Cách học:

1. Đọc phần A để hiểu từ vựng và luồng chạy.
2. Làm phần B, mỗi lần gõ một file rồi đối chiếu vai trò của file đó.
3. Dùng phần C để phân tích đề trong ảnh và diễn đạt câu trả lời.
4. Đọc phần D để biết build, chạy, dừng, dọn và dùng Git đúng cách.
5. Tự trả lời phần E trước khi mở đáp án; cuối cùng làm phần F mà không nhìn code mẫu.

Trong VS Code trên Mac, mở file này rồi nhấn `Cmd+Shift+V` để xem bản Markdown đã định dạng.

## Phần A. Notebook kiến thức nền tảng

### A1. C#/.NET/ASP.NET Core/VS Code khác nhau thế nào?

| Thuật ngữ | Anh hiểu ngắn gọn | Gặp ở đâu trong bài |
|---|---|---|
| C# | Ngôn ngữ anh dùng để viết xử lý | `NewsController.cs`, các đoạn C# trong Razor |
| .NET | Nền tảng và bộ thư viện để chương trình C# hoạt động | Project nhắm đến `net10.0` |
| .NET SDK | Bộ công cụ để tạo, biên dịch và chạy project | Lệnh `dotnet new`, `dotnet build`, `dotnet run` |
| ASP.NET Core | Framework xây dựng ứng dụng web trên .NET | Nhận yêu cầu HTTP và tạo phản hồi |
| MVC | Cách tổ chức ứng dụng thành Model, View, Controller | Các thư mục `Models`, `Views`, `Controllers` |
| Razor | Cú pháp kết hợp HTML với C# để sinh giao diện | File `.cshtml`, ký hiệu `@` |
| VS Code | Trình soạn thảo để anh viết và quản lý file | Explorer, Terminal và trình xem Markdown |
| Trình duyệt | Gửi yêu cầu và hiển thị HTML nhận về | Chrome/Safari mở `/News/Latest` |
| Terminal | Nơi nhập lệnh | Anh chạy `dotnet build` tại đây |

`code .` mở thư mục hiện tại bằng VS Code. Nó không khởi động website. Muốn website hoạt động, anh phải chạy ứng dụng bằng `dotnet run` hoặc cách chạy tương đương.

### A2. MVC là gì? Vai trò từng phần

| Thành phần | Nhiệm vụ | Ví dụ trong bài |
|---|---|---|
| Model | Biểu diễn dữ liệu và các quy tắc liên quan | Bài này dùng chuỗi và danh sách chuỗi, chưa cần tạo class News riêng |
| View | Quyết định nội dung HTML hiển thị | `Views/News/List.cshtml` vẽ danh sách `<li>` |
| Controller | Nhận yêu cầu, chuẩn bị dữ liệu và chọn kết quả trả về | `NewsController.List()` chuẩn bị các tiêu đề |

Có thư mục `Models` không có nghĩa bài nào cũng cần tạo bảng database. Bài trong ảnh yêu cầu truyền dữ liệu bằng ViewBag và ViewData, nên chưa cần Entity Framework, migration hay database để hoàn thành.

Khi tự làm, anh hỏi: **“Phần này là chuẩn bị dữ liệu, hay là hiển thị dữ liệu?”** Trong bài này, danh sách tiêu đề được chuẩn bị ở Controller; vòng lặp tạo HTML nằm trong View.

### A3. Một lần mở trang thực sự diễn ra thế nào?

Ví dụ anh mở `http://localhost:5080/News/Latest`:

```text
Trình duyệt gửi yêu cầu HTTP GET
        ↓
ASP.NET Core đọc đường dẫn /News/Latest
        ↓
Routing chọn NewsController và action Latest
        ↓
Latest gán ViewBag.NewsTitle rồi return View()
        ↓
Razor dựng Views/News/Latest.cshtml, nhúng _NewsHeader
        ↓
Máy chủ trả HTML cho trình duyệt
        ↓
Trình duyệt hiển thị tiêu đề và header
```

`localhost` là chính máy anh. `5080` là cổng ứng dụng đang lắng nghe. `/News/Latest` là đường dẫn để tìm chức năng.

C# và Razor được xử lý phía máy chủ. Trình duyệt nhận HTML đã dựng, không trực tiếp thực thi `NewsController.cs`.

### A4. Anh cần biết những thư mục/file nào?

```text
NewsSite/
├── NewsSite.csproj
├── Program.cs
├── Controllers/
│   └── NewsController.cs
├── Views/
│   ├── News/
│   │   ├── Index.cshtml
│   │   ├── Latest.cshtml
│   │   └── List.cshtml
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   └── _NewsHeader.cshtml
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
├── wwwroot/
├── Properties/launchSettings.json
├── .gitignore
├── bin/                         ← đầu ra build, không đưa vào Git
└── obj/                         ← file trung gian, không đưa vào Git
```

| File/thư mục | Vai trò cần nhớ |
|---|---|
| `.csproj` | Cấu hình project, framework và các tham chiếu |
| `Program.cs` | Khởi động ứng dụng, đăng ký MVC và cấu hình route |
| `Controllers` | Chứa các Controller viết bằng C# |
| `Views/News` | Các View dùng cho NewsController theo quy ước |
| `Views/Shared` | Giao diện có thể dùng chung giữa các View |
| `_Layout.cshtml` | Khung trang chung: thanh điều hướng, vùng nội dung, footer |
| `_ViewStart.cshtml` | Trong template này, chọn layout cho các View đầy đủ |
| `_ViewImports.cshtml` | Dùng chung namespace và đăng ký Tag Helpers cho View |
| `wwwroot` | Tài nguyên tĩnh của website: CSS, JavaScript, hình ảnh |
| `launchSettings.json` | Profile chạy local, ví dụ `http`, `https` và cổng mặc định |
| `appsettings.json` | Cấu hình ứng dụng; file mẫu hiện tại không phải rác build |

Đường dẫn file và URL khác nhau: file `Views/News/Latest.cshtml` được dựng thông qua URL `/News/Latest`. Anh không dùng đường dẫn file đó làm URL của trang MVC.

### A5. Đọc được từng dòng trong một Action

```csharp
public IActionResult Latest()
{
    ViewBag.NewsTitle = "Sinh viên bắt đầu học ASP.NET Core MVC";
    return View();
}
```

| Thành phần | Ý nghĩa |
|---|---|
| `public` | Phương thức có thể được truy cập; đây là một Action công khai trong bài |
| `IActionResult` | Kiểu kết quả mà Action trả về cho framework xử lý |
| `Latest` | Tên Action |
| `()` | Danh sách tham số; ở đây không nhận tham số |
| `{ ... }` | Phần thân phương thức |
| `ViewBag.NewsTitle = ...` | Chuẩn bị giá trị tiêu đề để View đọc |
| `return` | Kết thúc phương thức và trả kết quả |
| `View()` | Tạo kết quả hiển thị View; không phải lời gọi mở file bằng trình duyệt |

Với `public class NewsController : Controller`:

- `class` khai báo một lớp C#.
- `NewsController` là tên lớp; hậu tố `Controller` giúp thể hiện đây là Controller.
- `: Controller` là kế thừa lớp Controller của ASP.NET Core để dùng các tiện ích như `View()` và `ViewBag`.
- `using Microsoft.AspNetCore.Mvc;` cho phép sử dụng các tên trong namespace MVC.
- `namespace NewsSite.Controllers;` tổ chức tên các lớp trong project.

Trong bài, ba phương thức Index, Latest và List là ba Action. Anh không cần tạo ba Controller riêng cho chúng.

### A6. Routing: vì sao /News mở được Index?

Template MVC có cấu hình tương tự đoạn sau trong `Program.cs`:

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

`controller` chọn Controller, `action` chọn Action; `Home` và `Index` là giá trị mặc định. `id?` là tham số tùy chọn, bài này chưa dùng.

| URL | Controller | Action |
|---|---|---|
| `/` | HomeController | Index |
| `/News` | NewsController | Index |
| `/News/Index` | NewsController | Index |
| `/News/Latest` | NewsController | Latest |
| `/News/List` | NewsController | List |

Vì vậy, thấy trang Welcome ở `/` chưa chứng minh bài News bị sai. Anh kiểm tra đúng `/News`. Với quy ước này, tạo thêm Action không đòi hỏi phải đăng ký một URL thủ công cho từng Action. [Tham khảo routing của Microsoft](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-10.0).

### A7. return View() chọn file nào?

Trong `NewsController.Latest()`, `return View()` chọn tên View theo Action hiện tại là Latest. Framework tìm `Views/News/Latest.cshtml`, rồi đến `Views/Shared/Latest.cshtml` nếu cần.

`return View("Index")` chọn View mang tên Index một cách tường minh. Chọn View không tự đổi URL trình duyệt thành URL của Action khác. [Tham khảo quy ước tìm View](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-10.0#view-discovery).

Mẹo khi làm bài: gạch chân tên Controller và tên Action, rồi viết đường dẫn View tương ứng ra giấy trước khi tạo file.

### A8. HTML và Razor: ký hiệu @ để làm gì?

| Cú pháp | Vai trò |
|---|---|
| `<h1>...</h1>` | Tiêu đề chính trong nội dung trang |
| `<ul>` và `<li>` | Danh sách và từng phần tử của danh sách |
| `@ViewBag.NewsTitle` | Đánh giá biểu thức C# và đưa giá trị ra HTML |
| `@{ ... }` | Khối lệnh C#; khai báo biến không tự làm biến đó xuất hiện trên trang |
| `@foreach (...) { ... }` | Lặp và sinh HTML cho từng phần tử |
| `<partial name="_NewsHeader" />` | Nhúng partial View bằng Tag Helper |

Ví dụ:

```cshtml
@{
    var message = "Xin chào";
}

<p>@message</p>
```

Trình duyệt hiển thị “Xin chào”. Nếu viết `<p>message</p>`, anh sẽ thấy chữ “message”, vì chưa yêu cầu Razor đọc biến.

Razor mặc định mã hóa HTML đối với giá trị chuỗi khi hiển thị qua biểu thức. Bài này dùng các cách hiển thị thông thường; không cần dùng `Html.Raw` để đưa tiêu đề ra trang. [Tham khảo cú pháp và mã hóa của Razor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-10.0).

### A9. ViewBag và ViewData: hiểu để không học nhầm

| Nội dung | ViewBag | ViewData |
|---|---|---|
| Cách gán ở Controller | `ViewBag.NewsTitle = "Tin mới";` | `ViewData["NewsTitle"] = "Tin mới";` |
| Cách đọc ở View | `@ViewBag.NewsTitle` | `@ViewData["NewsTitle"]` |
| Cách truy cập | Dạng thuộc tính động | Dạng từ điển với khóa chuỗi |
| Khi duyệt một collection | Cần biết kiểu dữ liệu đang lấy để dùng phù hợp | Thường lấy ra và ép về kiểu collection trước khi duyệt |

Cả hai cùng truy cập bộ dữ liệu ViewData của View. Chúng đều có thể chứa chuỗi hoặc danh sách; cách chọn trong bài là do **đề yêu cầu**, không phải giới hạn “ViewBag chỉ chứa một tiêu đề, ViewData chỉ chứa danh sách”.

Hai cách sau cùng đọc một giá trị:

```csharp
ViewBag.NewsTitle = "Tin mới";
```

```cshtml
<p>@ViewData["NewsTitle"]</p>
```

Giữ tên nhất quán, tránh nhầm `Title`, `Titles` và `NewsTitle`. Hai cơ chế này phục vụ việc dựng View trong yêu cầu hiện tại; chúng không phải nơi lưu bài viết lâu dài hay giữ dữ liệu tự động qua một yêu cầu mới. [Tham khảo ViewBag và ViewData](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-10.0#weakly-typed-data-viewdata-viewdata-attribute-and-viewbag).

Trong template, `ViewData["Title"]` thường được layout dùng làm tên tab trình duyệt. Bài mẫu dùng `NewsTitle` cho tiêu đề bài viết để hai mục đích không giẫm lên nhau.

### A10. Đọc hiểu danh sách, as và ??

```csharp
new List<string>
{
    "Tin thứ nhất",
    "Tin thứ hai"
}
```

`string` là một chuỗi; `List<string>` là danh sách các chuỗi. Dấu phẩy phân cách các phần tử, dấu ngoặc kép bao quanh nội dung một chuỗi.

Trong View:

```cshtml
@{
    var titles = ViewData["Titles"] as List<string> ?? new List<string>();
}
```

Đọc từ trái sang phải:

1. Lấy giá trị ở khóa Titles.
2. `as List<string>` thử lấy nó dưới kiểu danh sách chuỗi; nếu không phù hợp hoặc không có giá trị thì kết quả là null.
3. `??` chọn danh sách rỗng nếu kết quả bên trái null.
4. `titles` là biến dùng ở vòng lặp phía dưới.

Danh sách rỗng giúp tránh lỗi khi duyệt, nhưng cũng có thể khiến anh chỉ thấy trang trống nếu gõ sai khóa. Khi thiếu tiêu đề, quay lại kiểm tra Action đã gán đúng `ViewData["Titles"]` chưa.

### A11. Partial View và Layout

| Nội dung | `_Layout.cshtml` | `_NewsHeader.cshtml` trong bài |
|---|---|---|
| Mục đích | Khung chung của trang | Mảnh nội dung dùng lại trong các trang tin |
| Nội dung điển hình | Thanh điều hướng, vùng nội dung, footer | “Bản tin hôm nay” |
| Cách dùng trong bài | Template đã cấu hình qua `_ViewStart.cshtml` | Mỗi View News gọi thẻ `<partial>` |

Đặt header trong Shared để các View dễ dùng lại. Dấu gạch dưới là quy ước đặt tên thường gặp; nó không phải từ khóa C# tạo quyền private. Trong bài này, không cần Action riêng cho header.

```cshtml
<partial name="_NewsHeader" />
```

Dòng này cần có trong cả Index, Latest và List. Template MVC đã đăng ký Tag Helpers trong `_ViewImports.cshtml`. Partial không tự chạy `_ViewStart.cshtml` như một View đầy đủ. [Tham khảo partial View](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/partial?view=aspnetcore-10.0).

### A12. Sáu câu anh cần giải thích được trước khi gõ bài

- Trình duyệt gửi yêu cầu đến đâu?
- URL đang chọn Controller và Action nào?
- Action chuẩn bị dữ liệu gì?
- Dữ liệu đi qua ViewBag hay ViewData theo đề?
- View nào đọc dữ liệu và sinh HTML?
- Đoạn nào lặp lại nên đặt vào partial View?

Nếu trả lời được sáu câu này với Latest và List, anh đã hiểu luồng chính của bài.

## Phần B. Thực hành đầy đủ từ tạo project đến chạy

Anh đã tạo project NewsSite trong `ASP.NET/week4/NewsSite`. Giữ phần này để làm lại từ đầu khi ôn; không chạy lệnh tạo mới đè lên project đang làm.

### B1. Tạo project MVC

Nếu đã có thư mục `NewsSite` chứa `NewsSite.csproj`, anh mở project hiện có và chuyển sang B2. Chỉ chạy các lệnh tạo mới dưới đây khi làm lại bài trong một thư mục chưa có project.

Mở Terminal trong VS Code, chạy:

```bash
cd ~/Documents/ASP.NET/week4
dotnet new mvc -n NewsSite
cd NewsSite
code .
```

`dotnet new mvc` tạo sẵn cấu trúc MVC. Trong bài này, anh sẽ thêm Controller và các View.

### B2. Tạo NewsController

Trong thư mục `Controllers`, tạo file **`NewsController.cs`**:

```csharp
using Microsoft.AspNetCore.Mvc;

namespace NewsSite.Controllers;

public class NewsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Latest()
    {
        ViewBag.NewsTitle = "Sinh viên bắt đầu học ASP.NET Core MVC";
        return View();
    }

    public IActionResult List()
    {
        ViewData["Titles"] = new List<string>
        {
            "ASP.NET Core MVC là gì?",
            "Tìm hiểu Controller và View",
            "Sử dụng ViewBag và ViewData"
        };

        return View();
    }
}
```

Anh hiểu ba Action như sau:

- `Index`: trả về trang Index.
- `Latest`: đặt một tiêu đề vào `ViewBag.NewsTitle`, rồi trả về View.
- `List`: đặt danh sách tiêu đề vào `ViewData["Titles"]`, rồi trả về View.

Với `return View()`, ASP.NET tự tìm View trùng tên Action. Ví dụ, `Latest()` tìm **`Views/News/Latest.cshtml`**.

### B3. Tạo partial View dùng chung

Trong `Views/Shared`, tạo **`_NewsHeader.cshtml`**:

```cshtml
<header class="mb-3">
    <strong>Bản tin hôm nay</strong>
</header>
```

Đây là phần giao diện dùng lại trên ba trang. Khi sửa nội dung file này, cả ba trang đều nhận thay đổi.

### B4. Tạo View Index

Tạo thư mục **`News`** bên trong `Views`.

Trong đó, tạo **`Index.cshtml`**:

```cshtml
@{
    ViewData["Title"] = "Tin tức";
}

<partial name="_NewsHeader" />

<h1>Tin tức mới nhất</h1>
```

Dòng `<partial ... />` nhúng header chung. Thẻ `<h1>` hiển thị nội dung đúng yêu cầu đề bài.

### B5. Tạo View Latest

Trong `Views/News`, tạo **`Latest.cshtml`**:

```cshtml
@{
    ViewData["Title"] = "Bài viết mới nhất";
}

<partial name="_NewsHeader" />

<h1>@ViewBag.NewsTitle</h1>
```

`@ViewBag.NewsTitle` lấy đúng giá trị mà Action `Latest()` đã gán.

**Tên `NewsTitle` phải giống nhau ở Controller và View.**

### B6. Tạo View List

Trong `Views/News`, tạo **`List.cshtml`**:

```cshtml
@{
    ViewData["Title"] = "Danh sách tin tức";
    var titles = ViewData["Titles"] as List<string> ?? new List<string>();
}

<partial name="_NewsHeader" />

<h1>Danh sách tiêu đề</h1>

<ul>
    @foreach (var title in titles)
    {
        <li>@title</li>
    }
</ul>
```

Phần cần hiểu:

- `ViewData["Titles"]` lấy dữ liệu từ Controller.
- `as List<string>` ép dữ liệu về danh sách chuỗi.
- `?? new List<string>()` dùng danh sách rỗng nếu không có dữ liệu.
- `foreach` tạo một thẻ `<li>` cho mỗi tiêu đề.

`ViewData["Title"]` trong các View đặt tên tab trình duyệt; `ViewData["Titles"]` chứa danh sách bài viết. Đây là hai khóa khác nhau.

### B7. Build và chạy

Trong Terminal của thư mục `NewsSite`, chạy:

```bash
dotnet build
dotnet run --launch-profile http --urls http://localhost:5080
```

Sau khi thấy `Now listening on`, mở các địa chỉ:

| Địa chỉ | Kết quả cần thấy |
|---|---|
| `http://localhost:5080/News` | “Tin tức mới nhất” |
| `http://localhost:5080/News/Latest` | Tiêu đề được truyền qua ViewBag |
| `http://localhost:5080/News/List` | Ba tiêu đề được truyền qua ViewData |

**Cả ba trang đều phải có “Bản tin hôm nay”.** Nhấn `Ctrl+C` trong Terminal để dừng ứng dụng.

## Phần C. Đọc đề và chuyển thành việc phải làm

### C1. Bảng đối chiếu đúng đề trong ảnh

| Đề nói gì? | Anh cần tạo/viết gì? | Cách tự chứng minh |
|---|---|---|
| Tạo project NewsSite | Dùng template MVC, đặt tên NewsSite | Có NewsSite.csproj và các thư mục MVC |
| Tạo NewsController | Tạo Controllers/NewsController.cs | Lớp NewsController kế thừa Controller |
| Index trả về View Index | Action Index có `return View();` | Mở `/News/Index` |
| Latest truyền tiêu đề qua ViewBag | Gán `ViewBag.NewsTitle` trước khi trả View | Latest.cshtml đọc `@ViewBag.NewsTitle` |
| List truyền danh sách qua ViewData | Gán `List<string>` vào `ViewData["Titles"]` | List.cshtml lấy đúng khóa và dùng foreach |
| Index hiện “Tin tức mới nhất” | Thêm nội dung vào Index.cshtml | Nhìn thấy đúng câu trên `/News` |
| Tạo partial _NewsHeader | Tạo Views/Shared/_NewsHeader.cshtml | File chứa “Bản tin hôm nay” |
| Nhúng header vào các View | Gọi partial trong ba file News | Cả ba trang đều hiện header |

### C2. Cách làm khi nhận một đề tương tự

1. Khoanh tên project để xác định namespace và nơi làm bài.
2. Khoanh tên Controller; bỏ hậu tố Controller để suy ra phần URL.
3. Liệt kê tất cả Action, mỗi Action một dòng.
4. Ghi cạnh mỗi Action: dữ liệu cần chuẩn bị, cách truyền theo đề, tên View nhận dữ liệu.
5. Tạo đúng thư mục View và đủ file, sau đó xử lý nội dung từng View.
6. Tách phần được đề yêu cầu dùng chung thành partial; kiểm tra từng nơi nhúng.
7. Build, chạy và mở từng URL. Build thành công chưa thay thế việc mở các trang để kiểm tra.

Ví dụ khi đề đổi sang `SportsController` với Action `Featured`, anh suy ra đường dẫn `/Sports/Featured` và View `Views/Sports/Featured.cshtml`. Tên mới không làm thay đổi nguyên tắc xử lý.

### C3. Cách diễn đạt câu trả lời khi bị hỏi về code của mình

Anh có thể trả lời theo ba ý: **nhận yêu cầu → chuẩn bị dữ liệu → hiển thị kết quả**.

> Khi mở /News/Latest, routing gọi Action Latest của NewsController. Action gán tiêu đề vào ViewBag.NewsTitle rồi trả View. Latest.cshtml đọc giá trị đó bằng Razor để hiển thị tiêu đề; đồng thời nhúng _NewsHeader dùng chung.

Với List, anh thay phần dữ liệu thành `List<string>` qua `ViewData["Titles"]` và nói rõ `foreach` tạo một thẻ li cho từng tiêu đề.

## Phần D. Build, chạy, dừng, dọn và giữ Git sạch

### D1. Biết mình đang đứng ở đâu trước khi chạy lệnh

```bash
cd ~/Documents/ASP.NET/week4/NewsSite
pwd
ls
```

Anh cần thấy `NewsSite.csproj`. Terminal ở thư mục khác có thể khiến lệnh báo không tìm thấy project, hoặc thao tác nhầm dự án.

### D2. Các lệnh khác nhau thế nào?

| Lệnh | Công việc | Có khởi động website không? |
|---|---|---|
| `dotnet --version` | Xem phiên bản SDK được chọn | Không |
| `dotnet new mvc -n NewsSite` | Tạo project mới từ template | Không |
| `dotnet restore` | Chuẩn bị các phụ thuộc cần cho build | Không |
| `dotnet build` | Biên dịch project, tạo đầu ra | Không |
| `dotnet run` | Chạy ứng dụng; thông thường build trước nếu cần | Có |
| `dotnet run --no-build` | Chạy đầu ra đã có, bỏ bước build | Có, khi đã có bản build phù hợp |
| `dotnet watch` | Theo dõi thay đổi để hỗ trợ cập nhật/chạy lại trong lúc phát triển | Có với cách dùng mặc định của bài |
| `dotnet clean` | Dọn đầu ra của cấu hình build được chọn | Không |

Build và run thông thường đã thực hiện restore khi cần. Không phải sau mỗi sửa chữ đều phải gõ restore riêng. `--no-build` không phù hợp để kỳ vọng code vừa sửa được biên dịch lại. [Microsoft: build](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build), [Microsoft: run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run).

`dotnet test` hữu ích khi có project kiểm thử. NewsSite hiện chưa có project kiểm thử riêng; chỉ chạy lệnh đó không chứng minh ba trang đã hiển thị đúng.

### D3. Quy trình gọn trong một buổi học

Khi bắt đầu:

```bash
git status --short
dotnet build
dotnet run --launch-profile http --urls http://localhost:5080
```

Sau đó mở ba URL của phần B. Đọc dòng `Now listening on` để biết địa chỉ thực sự của phiên chạy. Khi muốn dừng, nhấn `Ctrl+C` ở Terminal đang chạy NewsSite; chỉ đóng tab trình duyệt không dừng máy chủ.

Nếu muốn theo dõi thay đổi trong lúc học:

```bash
dotnet watch --launch-profile http --urls http://localhost:5080
```

Dừng phiên chạy trước khi khởi động phiên khác cùng cổng. Nếu chưa thấy thay đổi, xem log build/hot reload và tải lại trang; đừng vội tạo một project khác chỉ vì trang cũ chưa cập nhật.

### D4. bin và obj có phải lúc nào cũng là rác?

| Thứ gì? | Vì sao có? | Có đưa lên Git không? |
|---|---|---|
| `bin/` | Kết quả build: DLL, file chạy, thông tin debug và tài nguyên liên quan | Không trong project học này |
| `obj/` | File trung gian, thông tin restore, mã sinh tự động và cache phục vụ build | Không |
| `Controllers`, `Models`, `Views` | Mã nguồn do anh viết | Có |
| `.csproj`, `Program.cs` | Định nghĩa project và khởi động ứng dụng | Có |
| `wwwroot`, kể cả thư viện template đang dùng | CSS, JS, ảnh cần để website hiển thị | Có khi là tài nguyên của project |
| `.git/` | Lịch sử và cấu hình repository local | Git tự quản lý; không xóa như thư mục rác |
| `.DS_Store`, file tạm của editor | Metadata của hệ điều hành/công cụ | Không |

Có bin/obj khi đang làm bài là bình thường. Xóa liên tục rồi build lại sẽ phải sinh lại file và có thể làm mất lợi ích build tăng dần. Mục tiêu là **không đưa đầu ra build vào lịch sử Git**, không phải giữ project không bao giờ có đầu ra.

### D5. Dọn an toàn khi kết thúc hoặc cần build lại sạch

1. Lưu các file đang sửa.
2. Dừng đúng ứng dụng NewsSite bằng Ctrl+C.
3. Kiểm tra đang ở thư mục có NewsSite.csproj.
4. Chạy:

```bash
dotnet clean
```

Nếu từng build cấu hình Release, dọn cấu hình đó khi cần:

```bash
dotnet clean --configuration Release
```

Clean dọn các đầu ra MSBuild biết của cấu hình được chọn; không cam kết xóa toàn bộ thư mục hay mọi metadata restore. Có vài file còn lại trong obj không có nghĩa phải xóa cả project. [Microsoft: dotnet clean](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-clean).

Khi thật sự cần làm lại cache local, sau khi đã dừng ứng dụng anh có thể xóa riêng **bin và obj** bằng Explorer trong VS Code, rồi chạy `dotnet restore` và `dotnet build` lại. Không chọn nhầm Controllers, Views, wwwroot hoặc .git.

Không cần xóa SDK trong `.dotnet` hoặc toàn bộ cache NuGet chỉ để dọn một bài MVC. Chúng phục vụ nhiều project và có thể phải tải lại nếu bị xóa.

### D6. Tạo .gitignore trước khi add

Ở thư mục project, chạy một lần nếu chưa có file:

```bash
dotnet new gitignore
```

Lệnh tạo bộ quy tắc .NET chuẩn. Nếu đã có file, mở file để bổ sung; không cần ép ghi đè. Những quy tắc cốt lõi thường gặp:

```gitignore
[Bb]in/
[Oo]bj/
.vs/
.DS_Store
*.user
*.suo
*.log
.env
.env.*
!.env.example
```

Trong project của anh, AGENTS.md và DESIGN.md được giữ làm tài liệu local theo cách dùng trước đó. Guide học tập và SYSTEM_DOCUMENTATION.md vẫn có thể đưa vào Git. Không bỏ qua toàn bộ wwwroot hoặc mọi file JSON: nhiều file trong đó là phần cần của ứng dụng.

`.gitignore` không xóa file khỏi ổ đĩa và không tự loại file đã được Git theo dõi. [Tài liệu gitignore](https://git-scm.com/docs/gitignore).

### D7. Phân biệt add, commit và push

```text
File trên máy → git add → staging/index → git commit → lịch sử local → git push → GitHub
```

| Thao tác/trạng thái | Ý nghĩa |
|---|---|
| Sửa file | Thay đổi ở thư mục làm việc, chưa chắc đã nằm trong staging |
| `git add` | Chọn phiên bản file để đưa vào commit kế tiếp |
| `git commit` | Ghi một phiên bản vào lịch sử local |
| `git push` | Gửi các commit lên remote |
| `git status --short` | Xem file mới, thay đổi và staging |
| `git diff --cached --stat` | Xem phạm vi file đang được chọn cho commit |

Sau khi tạo ignore, trước khi commit anh kiểm tra:

```bash
git status --short
git add .
git diff --cached --stat
```

Chỉ tiếp tục commit sau khi đã xem danh sách: không có bin/obj, file bí mật hoặc file ngoài bài học.

### D8. Đã lỡ add hoặc theo dõi bin/obj thì làm sao?

Thêm .gitignore trước, rồi bỏ chúng khỏi index:

```bash
git rm -r --cached --ignore-unmatch bin obj
git add .gitignore
git status --short
```

`--cached` chỉ bỏ theo dõi trong index; file vẫn ở trên máy. `-r` áp dụng cho nội dung thư mục. `--ignore-unmatch` cho phép lệnh không báo lỗi khi không có đường dẫn phù hợp. [Tài liệu git rm](https://git-scm.com/docs/git-rm).

Nếu bin/obj đã nằm trong commit trước, một commit dọn bình thường chỉ loại chúng khỏi phiên bản mới; commit cũ vẫn chứa chúng. Vì thế, thêm .gitignore sau khi commit chưa tự xóa file khỏi lịch sử.

**Trường hợp NewsSite lúc anh yêu cầu xử lý:** có một commit đầu local, remote còn trống, 108 file build bị theo dõi. Cách sửa được áp dụng là thêm ignore, gỡ file sinh tự động khỏi index và sửa commit đầu chưa chia sẻ. File trên máy và mã nguồn được giữ nguyên.

Chỉ với commit chưa được chia sẻ, sau khi xem kỹ staging, mới cân nhắc:

```bash
git commit --amend --no-edit
```

Amend đổi mã commit. Nếu commit đã đẩy lên hoặc người khác đang dùng, không tự dùng quy trình này hoặc force push; tạo commit dọn mới và trao đổi trước khi thay đổi lịch sử chung. Commit cũ có thể còn trong reflog local để khôi phục, nhưng không còn là lịch sử của main sau khi sửa commit đầu. Không cần dọn reflog để học bài này.

### D9. Thói quen giúp ít rác và ít mất công

- Một bài dùng một project rõ tên; sửa project hiện có thay vì tạo nhiều bản thử trong cùng thư mục.
- Tạo ignore sớm, xem staging trước mỗi commit.
- Giữ file thử, ảnh chụp và ghi chú ở vị trí rõ ràng; không coi ảnh đề bài là rác build.
- Không xóa file code hoặc thư viện giao diện đang dùng để giảm dung lượng.
- Dừng ứng dụng trước khi clean sâu; không dừng tất cả tiến trình dotnet của các project khác.
- Giữ token và mật khẩu ngoài mã nguồn/Git. Ignore không thể làm một bí mật đã commit trở nên an toàn; khi lộ bí mật cần xử lý và thay credential phù hợp.

## Phần E. Tự kiểm tra kiến thức, có đáp án

Anh tự nói hoặc viết câu trả lời trước, rồi mới mở “Đáp án gợi ý”. Mục tiêu là giải thích được bằng lời của anh.

### E1. MVC là gì?

<details>
<summary>Đáp án gợi ý</summary>

MVC gồm Model để biểu diễn dữ liệu/quy tắc, View để hiển thị và Controller để nhận yêu cầu, chuẩn bị dữ liệu và chọn kết quả. Trong bài News, Controller truyền chuỗi/danh sách cho View.

</details>

### E2. Vì sao /News gọi Index, còn /News/Latest gọi Latest?

<details>
<summary>Đáp án gợi ý</summary>

Route của template dùng controller/action và mặc định action là Index. /News chọn NewsController rồi dùng Index; /News/Latest chỉ rõ Action Latest.

</details>

### E3. return View() trong Latest tìm View ở đâu?

<details>
<summary>Đáp án gợi ý</summary>

Theo quy ước, tên View là Latest và thư mục Controller là News: tìm Views/News/Latest.cshtml trước, sau đó có thể tìm Views/Shared/Latest.cshtml.

</details>

### E4. Vì sao thêm Controller rồi vẫn có thể gặp lỗi không tìm thấy View?

<details>
<summary>Đáp án gợi ý</summary>

Controller và View là các file khác nhau. Cần tạo đủ file .cshtml và đặt đúng thư mục/tên. Build thành công chưa đảm bảo View được tìm thấy lúc có request.

</details>

### E5. ViewBag chỉ chứa một chuỗi, ViewData chỉ chứa danh sách có đúng không?

<details>
<summary>Đáp án gợi ý</summary>

Không. Cả hai có thể dùng với nhiều loại dữ liệu. Đề yêu cầu dùng ViewBag cho tiêu đề và ViewData cho danh sách để luyện hai cách truy cập.

</details>

### E6. Hai cách ViewBag.NewsTitle và ViewData["NewsTitle"] có liên hệ gì?

<details>
<summary>Đáp án gợi ý</summary>

Chúng truy cập cùng dữ liệu dưới khóa NewsTitle. Đây là lý do nên dùng tên nhất quán và tách tên tiêu đề bài khỏi khóa Title mà layout dùng.

</details>

### E7. Vì sao trước foreach lại lấy dữ liệu bằng `as List<string>`?

<details>
<summary>Đáp án gợi ý</summary>

ViewData cung cấp giá trị dạng object. Ở ví dụ này, View cần lấy về đúng danh sách chuỗi để duyệt; as trả null nếu không phù hợp, ?? cung cấp danh sách rỗng.

</details>

### E8. Partial khác View đầy đủ như thế nào trong bài?

<details>
<summary>Đáp án gợi ý</summary>

Index/Latest/List là các View nội dung. _NewsHeader là một mảnh giao diện được nhúng vào chúng để dùng chung. Bài không cần Action riêng cho header.

</details>

### E9. Có thể viết header ba lần thay vì partial không?

<details>
<summary>Đáp án gợi ý</summary>

Kết quả nhìn có thể giống nhau nhưng chưa làm đúng yêu cầu tạo và nhúng partial. Dùng chung một file cũng giúp sửa header tại một chỗ.

</details>

### E10. Dòng ViewData["Title"] trong View có phải danh sách bài viết không?

<details>
<summary>Đáp án gợi ý</summary>

Không. Trong mẫu này Title phục vụ tên tab trình duyệt, còn Titles chứa danh sách; NewsTitle chứa tiêu đề một bài. Cần phân biệt mục đích của từng khóa.

</details>

### E11. Đổi tên Controller thành SportsController thì thư mục View theo quy ước là gì?

<details>
<summary>Đáp án gợi ý</summary>

Views/Sports. Ví dụ Action Featured thường dùng Views/Sports/Featured.cshtml, URL tương ứng là /Sports/Featured khi giữ route quy ước.

</details>

### E12. Build succeeded có chứng minh trang List hiển thị đủ ba tiêu đề không?

<details>
<summary>Đáp án gợi ý</summary>

Không. Build kiểm tra biên dịch; anh còn phải chạy app và mở /News/List để kiểm tra dữ liệu, vòng lặp và partial.

</details>

### E13. Xóa bin/obj có mất NewsController không?

<details>
<summary>Đáp án gợi ý</summary>

Không nếu chỉ xóa đúng đầu ra build. Mã nguồn ở Controllers/Views vẫn còn và có thể build lại. Tuy nhiên phải dừng app và kiểm tra đúng thư mục trước khi dọn.

</details>

### E14. Vì sao thêm .gitignore mà Git vẫn theo dõi bin/obj?

<details>
<summary>Đáp án gợi ý</summary>

Vì các file đã được Git theo dõi từ trước. Cần gỡ khỏi index bằng git rm --cached; việc đó không tự xóa chúng khỏi commit cũ.

</details>

### E15. Bài này đã cần database và class News chưa?

<details>
<summary>Đáp án gợi ý</summary>

Chưa cần để hoàn thành các yêu cầu trong ảnh. Dữ liệu mẫu đi qua ViewBag/ViewData. Model nghiệp vụ và lưu trữ lâu dài là phần học tiếp theo.

</details>

## Phần F. Tự luyện và tự sửa lỗi

### F1. Ba bài luyện nhỏ

**Bài 1:** Thêm Action About vào NewsController. Tạo View tương ứng hiển thị “Giới thiệu bản tin” và nhúng header chung. Trước khi viết, tự ghi URL và đường dẫn View ra giấy. Kết quả cần kiểm tra: `/News/About`.

**Bài 2:** Thêm tiêu đề thứ tư vào danh sách của Action List. Khi chạy trang List phải có bốn mục, vẫn chỉ có một header. Giải thích vì sao anh chỉ thêm dữ liệu ở Action mà không cần viết thêm một thẻ li cố định.

**Bài 3:** Tự đổi tên khóa tiêu đề bài từ NewsTitle sang Headline ở đúng những nơi liên quan. Mở Latest để kiểm tra rồi giải thích: đổi khóa dữ liệu có làm URL /News/Latest đổi không? Gợi ý: URL lấy tên Controller/Action; khóa dữ liệu là chuyện khác.

Các bài luyện là việc anh tự làm khi ôn. Notebook này không tự sửa các Action của project.

### F2. Đọc triệu chứng trước khi sửa

| Triệu chứng | Kiểm tra theo thứ tự |
|---|---|
| Không tìm thấy project khi chạy lệnh | Dùng pwd/ls; thư mục hiện tại có NewsSite.csproj không? |
| Trình duyệt không kết nối được | App đã chạy chưa, đã thấy Now listening on chưa, cổng có đúng không? |
| Báo cổng đang được dùng | Dừng đúng phiên chạy cũ hoặc chọn cổng khác và dùng URL mới; đừng dừng app không liên quan |
| Vào / thấy Welcome | Mở đúng /News để tới NewsController theo route hiện tại |
| HTTP 404 ở /News/Latest | Kiểm tra URL, lớp NewsController và Action Latest; không dùng URL là đường dẫn file View |
| Báo View Latest không tìm thấy | Kiểm tra Views/News/Latest.cshtml; giữ tên và cách viết hoa/thường nhất quán |
| Trang Latest không có tiêu đề | Kiểm tra Action đã gán dữ liệu, View đọc đúng khóa và có ký hiệu @ |
| List không có mục nào | Kiểm tra khóa Titles, kiểu danh sách và dữ liệu trước foreach; danh sách rỗng dự phòng có thể che một lỗi gõ khóa |
| Header chỉ xuất hiện ở một trang | Kiểm tra lời gọi partial trong đủ ba View |
| Sửa code nhưng vẫn thấy kết quả cũ | Đọc log build/run, kiểm tra dùng đúng project/cổng, tải lại trang; không dùng --no-build khi cần biên dịch code mới |
| Git hiện rất nhiều bin/obj | Tạo ignore và bỏ chúng khỏi index, không xóa mã nguồn để làm status ngắn lại |

### F3. Checklist trước khi tự nhận đã hoàn thành bài

- [ ] Giải thích được luồng /News/Latest từ URL đến HTML.
- [ ] Phân biệt được Controller, Action, View và partial.
- [ ] Viết đúng ba Action và suy ra đúng ba đường dẫn View.
- [ ] Phân biệt NewsTitle, Title và Titles trong mẫu.
- [ ] Mở đủ Index, Latest, List và thấy nội dung đúng.
- [ ] Cả ba trang đều nhúng header chung.
- [ ] Giải thích được `foreach`, `List<string>`, `as` và `??`.
- [ ] Biết dừng đúng app trước khi dọn output.
- [ ] Git không theo dõi bin/obj, nhưng vẫn giữ đủ mã nguồn/tài nguyên cần chạy.
- [ ] Làm được ít nhất một bài đổi tên Controller/Action mà không chép máy móc.

## Ghi chú kiểm chứng và phạm vi

Các đoạn code ở phần B được giữ từ mẫu đã build thành công và kiểm tra 10/10 đường dẫn cùng hai vòng desktop/mobile. Bổ sung notebook không thay đổi mã Controller/View của anh.

Project hiện có dữ liệu mẫu để học truyền dữ liệu. Chưa có database hoặc thao tác lưu bài viết lâu dài. File guide này nằm trong project để anh đọc cùng code; các lệnh tạo project chỉ dùng khi làm mới từ đầu.
