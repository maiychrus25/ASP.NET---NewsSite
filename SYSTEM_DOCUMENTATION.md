# NewsSite System Documentation

Trạng thái: Bài thực hành ASP.NET Core MVC chạy local.
Target framework: `net10.0`.

## Phạm vi bài học

- NewsController có Index, Latest và List; các View nằm trong Views/News.
- Latest truyền NewsTitle qua ViewBag; List truyền Titles qua ViewData.
- Ba View nhúng Views/Shared/_NewsHeader.cshtml.
- Giữ các trang Home, Privacy và Error của template. Dữ liệu bài học là chuỗi/danh sách mẫu, không có database.

## Git và file sinh tự động

Ngày 2026-11-05: bổ sung .gitignore chuẩn từ SDK .NET và quy tắc cho tài liệu local. Bỏ bin/obj khỏi Git index, giữ nguyên file trên máy và mã nguồn. Commit đầu chưa được chia sẻ được sửa lại để file build không nằm trong lịch sử sẽ đẩy lên. Có thể tìm commit trước đó trong reflog; không dọn reflog để giữ khả năng khôi phục.

Hướng dẫn làm bài giữ nguyên nội dung ban đầu trong [NewsSite-Guide.md](NewsSite-Guide.md). Kiến thức nền tảng, câu hỏi ôn tập và cách dọn build/Git nằm riêng trong [ASP.NET-Core-MVC-Notebook.md](ASP.NET-Core-MVC-Notebook.md).
