# Hướng dẫn xây dựng NewsSite bằng ASP.NET Core MVC

Anh làm lần lượt các bước dưới đây. Bài này có luồng:

**Trình duyệt → Action trong Controller → View hiển thị dữ liệu.**

Mẫu đã được kiểm chứng trên .NET 10. Tài liệu này hướng dẫn anh tự tạo project và gõ code.

## 1. Tạo project MVC

Mở Terminal trong VS Code, chạy:

```bash
cd ~/Documents/ASP.NET/week4
dotnet new mvc -n NewsSite
cd NewsSite
code .
```

`dotnet new mvc` tạo sẵn cấu trúc MVC. Trong bài này, anh sẽ thêm Controller và các View.

## 2. Tạo NewsController

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

## 3. Tạo partial View dùng chung

Trong `Views/Shared`, tạo **`_NewsHeader.cshtml`**:

```cshtml
<header class="mb-3">
    <strong>Bản tin hôm nay</strong>
</header>
```

Đây là phần giao diện dùng lại trên ba trang. Khi sửa nội dung file này, cả ba trang đều nhận thay đổi.

## 4. Tạo View Index

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

## 5. Tạo View Latest

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

## 6. Tạo View List

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

## 7. Build và chạy

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

## Kết quả kiểm chứng mẫu

Mẫu đã build thành công, đạt 10/10 kiểm tra đường dẫn và hai vòng desktop/mobile. Thư mục mẫu tạm đã được dọn.

Việc lưu tài liệu này chưa tạo project NewsSite trong thư mục của anh; anh thực hiện các bước bên trên để tạo dự án.
