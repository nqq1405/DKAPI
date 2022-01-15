# Tiêu đề
```markdown 
# Header h1
## Header h2
### Header h3
#### h4 Tiêu đề
##### h5 Tiêu đề
###### h6 Tiêu đề
```
- Khi hiển thị văn bản sẽ như sau:
# Header h1
## Header h2
### Header h3
#### h4 Tiêu đề
##### h5 Tiêu đề
###### h6 Tiêu đề

# Bôi đậm và in nghiêng
```markdown
**In đậm** and *In Nghiêng* and ***cả đậm và nghiêng***.
__In đậm__ and _In Nghiêng_ and ___cả đậm và nghiêng___
```
- Khi hiển thị sẽ như sau:

**In đậm** and *In Nghiêng* and ***cả đậm và nghiêng***.

__In đậm__ and _In Nghiêng_ and ___cả đậm và nghiêng___
# Link
```markdown 
[John_Gruber](https://en.wikipedia.org/wiki/John_Gruber)
```
- Khi hiển thị sẽ như sau:
[John_Gruber](https://en.wikipedia.org/wiki/John_Gruber)

- Ngoài ra bạn có thể thêm tiêu đề cho link bằng cách thêm "title" trong mô tả bên trong ngoặc đơn ().khi rê chuột vào sẽ có gợi ý hiện ra
[John_Gruber](https://en.wikipedia.org/wiki/John_Gruber "Markdown Creator")

```markdown 
<https://doidev.com>
<contact@doidev.com>
[Đời Dev](https://doidev.com)
```
- Khi hiển thị sẽ như sau:

    <https://doidev.com>

    <contact@doidev.com>

    [Đời Dev](https://doidev.com)

# Image
```markdown 
![anh gai xinh](https://th.bing.com/th/id/OIF.RAL9wG3GCIucj9B4cGbhog?w=185&h=247&c=7&r=0&o=5&pid=1.7 "anh gai xinh")
```
- Khi hiển thị sẽ như sau:

![Alt text](https://th.bing.com/th/id/OIF.RAL9wG3GCIucj9B4cGbhog?w=185&h=247&c=7&r=0&o=5&pid=1.7 "anh gai xinh")

# Định dạng danh sách
```markdown 
* Ruby
* PHP
  * Laravel
  * Symfony
  * Phalcon
* Python
  * Flask
     * Jinja2
     * WSGI1.0 
  * Django 
```
- Nếu bạn muốn dùng số để đánh dấu thì viết số và một dấu chấm .
```markdown 
    1. number one
    2. number two
    3. number three
```

- Khi hiển thị sẽ như sau:
1. number one
2. number two
3. number three
- ***Nếu như bạn dùng 1. cho mỗi đầu dòng trong danh sách có thứ tự thì Markdown sẽ tự động đánh số thứ tự cho các dòng. Nếu lỡ bạn có muốn thay đổi thứ tự sẽ rất dễ dàng.***
```markdown
Ví dụ như trên nhưng ta sẽ viết như sau:
1. Một vợ
1. Hai con
1. Ba Lầu
1. Bốn bánh
```
- Khi hiển thị sẽ như sau:

1. Một vợ
1. Hai con
1. Ba Lầu
1. Bốn bánh

# Trích dẫn
- Cách viết một trích dẫn giống hệt khi bạn vẫn trả lời bình luận hay dẫn chứng trong các diễn đàn: sử dụng ký tự >
```markdown 
> Phải chăng khi biết yêu, giấc mơ là nơi bắt đầu. Này mùa đông ơi xin hãy làm tuyết rơi, để chắn lối em anh về. Này mùa đông ơi xin hãy làm tuyết rơi, để anh
```
- Khi hiển thị sẽ như sau:

> Phải chăng khi biết yêu, giấc mơ là nơi bắt đầu. Này mùa đông ơi xin hãy làm tuyết rơi, để chắn lối em anh về. Này mùa đông ơi xin hãy làm tuyết rơi, để anh

# Escape
- Sẽ có những lúc bạn cần dùng đến đúng những ký tự mà Markdown đang sử dụng, ví dụ đơn giản như khi muốn viết *bold* mà không bị in đậm. Khi đó hãy sử dụng ký tự escape \
```markdown 
\*bold\*
``` 
- Khi hiển thị sẽ như sau:

    \*bold\*
- Đoạn trích dẫn cũng có thể được lồng vào nhau như sau:
```markdown
> **Đời Dev** là blog chuyên chém gió về web development & đời dev.
>> Chủ đề chính là chém về frontend và bí kíp coding
```
> **Đời Dev** là blog chuyên chém gió về web development & đời dev.
>> Chủ đề chính là chém về frontend và bí kíp coding

# Gạch ngang giữa chữ (Strikethrough)
```markdown
~~Cú pháp để gạch ngang~~
```
- Khi hiển thị sẽ như sau:

~~Cú pháp để gạch ngang~~

# Danh sách việc cần làm (Task Lists)
- **Bạn có thể tạo danh sách việc cần làm với ô đánh dấu đầu dòng. Cú pháp là bắt đầu dòng với ký tự gạch giữa - theo sau là khoảng trắng và cặp ngoặc vuông []. Để đánh dấu thì gõ ký tự x vào giữa cặp ngoặc vuông [x]**
```markdown
- [x] Tạo blog Đời Dev
- [ ] Chém gió về Web Dev
- [ ] Đọc sách
```
## - ***Khi hiển thị sẽ như sau:***

- [x] Tạo blog Đời Dev
- [ ] Chém gió về Web Dev
- [ ] Đọc sách

# Viết Mã (Code)
## Mã cùng dòng (Inline Code)
- Nhập đoạn mã trong cặp thẻ `.
```markdown
Trong ví dụ này thì , `<section></section>` sẽ được hiển thị là **Mã**.
```
Hiển thị văn bản như sau:

Trong ví dụ này thì , `<section></section>` sẽ được hiển thị là **Mã**.
- Đoạn trích mã (Block Fenced Code)
```markdown
    ```markdown
    Bên trong là đoạn mã ...
    ```
```
Hiển thị văn bản như sau:
```markdown
    Bên trong là đoạn mã ...
```
- Hiển thị màu cho cú pháp (Syntax Highlighting)
```markdown
    ```js
    grunt.initConfig({
    assemble: {
        options: {
        assets: 'docs/assets',
        data: 'src/data/*.{json,yml}',
        helpers: 'src/custom-helpers.js',
        partials: ['src/partials/**/*.{hbs,md}']
        },
        pages: {
        options: {
            layout: 'default.hbs'
        },
        files: {
            './': ['src/templates/pages/index.hbs']
        }
        }
    }
    };
    ``` 
```
Hiển thị như sau:
```js
    grunt.initConfig({
    assemble: {
        options: {
        assets: 'docs/assets',
        data: 'src/data/*.{json,yml}',
        helpers: 'src/custom-helpers.js',
        partials: ['src/partials/**/*.{hbs,md}']
        },
        pages: {
        options: {
            layout: 'default.hbs'
        },
        files: {
            './': ['src/templates/pages/index.hbs']
        }
        }
    }
    };
```
# Bảng (Tables)
#### Bảng được tạo ra bằng cách sắp xếp các ký tự gạch đứng | chia ô và phân cách giữa tiêu đề bảng và nội dung bảng bằng dòng các ký tự gạch giữa - liên tiếp. Lưu ý rằng các gạch đứng không nhất thiết phải thẳng theo hàng dọc mà có thể lệch nhau nhưng khi hiển thị ra giao diện thì vẫn là bảng đều các cạnh

```markdown
| Tiêu đề | Mô tả chi tiết |
| ------ | ----------- |
| Cú pháp Markdown cơ bản | Trình bày cơ bản cách trang trí văn bảng bằng Markdown. |
| Phía sau website | Câu chuyện về mọi thứ phía sau website để cho nó hoạt động. |
| Lập trình mọi thứ    | Chém gió về tương lai lập trình mọi thứ. |
```
- hiển thị như sau:

| Tiêu đề | Mô tả chi tiết |
| ------ | ----------- |
| Cú pháp Markdown cơ bản | Trình bày cơ bản cách trang trí văn bảng bằng Markdown. |
| Phía sau website | Câu chuyện về mọi thứ phía sau website để cho nó hoạt động. |
| Lập trình mọi thứ    | Chém gió về tương lai lập trình mọi thứ. |

- **Canh chỉnh trái, phải, giữa cho bảng**
    - Thêm dấu hai chấm : vào bên trái của dòng gạch giữa thì sẽ canh trái cho cột đó, thêm vào bên phải thì canh phải và thêm cả 2 bên thì canh giữa cho cột đó

```markdown
| Tiêu đề (canh giữa) | Mô tả chi tiết (canh phải)|
| :-------: | --------------: |
| Cú pháp Markdown cơ bản | Trình bày cơ bản cách trang trí văn bảng bằng Markdown. |
| Phía sau website | Câu chuyện về mọi thứ phía sau website để cho nó hoạt động. |
| Lập trình mọi thứ    | Chém gió về tương lai lập trình mọi thứ. |
```
    - Bảng sẽ được hiển thị như sau:

| Tiêu đề (canh giữa) | Mô tả chi tiết (canh phải)|
| :-------: | --------------: |
| Cú pháp Markdown cơ bản | Trình bày cơ bản cách trang trí văn bảng bằng Markdown. |
| Phía sau website | Câu chuyện về mọi thứ phía sau website để cho nó hoạt động. |
| Lập trình mọi thứ    | Chém gió về tương lai lập trình mọi thứ. |

# Neo Liên kết (Named Anchors)
```markdown
## Table of Contents
  * [Chương 1 - Chém gió cấp 1](#chapter-1)
  * [Chương 1 - Chém gió cấp 2](#chapter-2)
  * [Chương 1 - Chém gió cấp 3](#chapter-3)

## Chương 1 - Chém gió cấp 1 <a id="chapter-1"></a>
Chương này mô tả kỹ năng chém gió của lính mới gà mờ.

## Chương 2 - Chém gió cấp 2 <a id="chapter-2"></a>
Kỹ năng chém gió đã lên một cấp bậc mới nhưng vẫn bị chê là nổ.

## Chương 3 - Chém gió cấp 3 <a id="chapter-3"></a>
Kỹ năng chém gió đã có thể tạo bão và gây rúng động thị trường.
```
- hiển thị như sau:
## Table of Contents
  * [Chương 1 - Chém gió cấp 1](#chapter-1)
  * [Chương 1 - Chém gió cấp 2](#chapter-2)
  * [Chương 1 - Chém gió cấp 3](#chapter-3)

## Chương 1 - Chém gió cấp 1 <a id="chapter-1"></a>
Chương này mô tả kỹ năng chém gió của lính mới gà mờ.

## Chương 2 - Chém gió cấp 2 <a id="chapter-2"></a>
Kỹ năng chém gió đã lên một cấp bậc mới nhưng vẫn bị chê là nổ.

## Chương 3 - Chém gió cấp 3 <a id="chapter-3"></a>
Kỹ năng chém gió đã có thể tạo bão và gây rúng động thị trường.

