# ***1.CÀI ĐẶT MONGO DB TRÊN SERVER UBUNTU***
Link: [xem thêm](https://cloudviet.com.vn/cai-dat-mongodb-tren-ubuntu-20-04/)
- Cài đặt kho lưu trữ qua HTTPS:
    ```bash
	# sudo apt install dirmngr gnupg apt-transport-https ca-certificates software-properties-common
    ```
- Nhập khóa GPG: 
    ```bash
    # wget -qO - https://www.mongodb.org/static/pgp/server-4.4.asc | sudo apt-key add -
    ```
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb2.png)
- Thêm kho lưu trữ cho MongoDB:
    ```bash
    # sudo add-apt-repository 'deb [arch=amd64] https://repo.mongodb.org/apt/ubuntu focal/mongodb-org/4.4 multiverse'
    ```
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb3.png)
- Cài đặt Mongodb-org:
    ```bash
    # sudo apt install mongodb-org
    ```
- Nhấn `y` để tiếp tục
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb5.png)
- Khởi động MongoDB:
    ```bash
    # sudo systemctl enable --now mongod
    ```

- Kiểm tra xem MongoDB cài đặt và chạy thành công hay chưa:	
    ```bash
    # mongo --eval 'db.runCommand({ connectionStatus: 1 })'
    ```    
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb7.png)
# ***2.Cấu Hình MongoDB***
- File cấu hình MongoDB được đặt tên mongod.conf và nằm trong /etc thư mục Chỉnh sửa File mongod.conf:
    ```apacheconf
    security:
        authorization: enabled
    ```
    - Cấu hình file mongod.conf
    ```bash
    # sudo nano /etc/mongod.conf
    ``` 
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb8.png)
- Restart dịch vụ của MongoDB:
    ```bash
    # sudo systemctl restart mongod
    ``` 
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb9.png)
# ***3.Tạo người dùng MongoDB***
- Truy cập vào MongoDB
    ```bash
    # mongo
    ```
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb10.png)
    - Kiểm tra xem đã có tiến trình dịch vụ mongod đang chạy chưa.
    ```bash
    # ps axu | grep mongod
    mongod 6962 1.1 0.6 1079432 53256 ? Sl 13:59 0:00 /usr/bin/mongod -f /etc/mongod.conf
    ```
    - Kiểm tra xem dịch vụ MongoDB đã lắng nghe port 27017 mặc định của mongod chưa nhé.
    ```bash
    # netstat -alnpt | grep "27017"
    tcp 0 0 127.0.0.1:27017 0.0.0.0:* LISTEN 6962/mongod
    ```
- Kết nối với admin cơ sơ dữ liệu:
    ```bash
    # use admin
    ```
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb11.png)
- Tạo người dùng :
    ```js
    db.createUser(
    {
    user: "mongoadmin",
    pwd: "Pass$#word23",
    roles: [ { role: "userAdminAnyDatabase", db: "admin" } ]
    })
    ```
    - Sau khi hoàn tất
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb12.png)
- Hãy thoát khỏi trình bao mongo bằng:
    ```bash
    # quit
    ```
-  Truy cập bằng user Admin mới vừa tạo:
    ```bash
    # mongo -u mongoadmin -p --authenticationDatabase admin
    ```
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb14.png)

    ```bash
    # use admin
    ```
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb15.png)
    
    -> Hiện thị thông tin người dùng vừa tạo:
    ```bash
    # show users
    ```
    ![alt](https://cloudviet.com.vn/wp-content/uploads/2021/01/mdb16.png)


 # Uninstall MongoDb
    ```bash
    # apt-get remove mongodb* --purge
    # rm -rf /var/lib/mongodb/
    ```